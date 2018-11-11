using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
public class SendRequest : MonoBehaviour {

    public Image Image;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    public void CallRequest()
    {
        StartCoroutine(SendHttpRequest());

    }
    IEnumerator SendHttpRequest()
    {
        WWWForm form = new WWWForm();
        if (File.Exists("newcat.jpg"))
        {
            print(true);

            //byte [] bytes = System.Text.Encoding.UTF8.GetBytes("input.jpg");
            byte[] bytes =System.IO.File.ReadAllBytes("newcat.jpg");

            print(bytes.Length);
            form.AddBinaryData("photo", bytes, "input.jpg", "image/jpg");
            form.AddField("style","dora-marr-network");
            UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/upload",form);

            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                // Show results as text
                Debug.Log(www.downloadHandler.data);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                MemoryStream ms = new MemoryStream(results);

                Texture2D texture = new Texture2D(100, 100);
                texture.LoadImage(results);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));

                Image.sprite = sprite;



            }
        }
        else
        {
            print(false);
        }

    
    }

    /*private void VaryQualityLevel()
    {
        // Get a bitmap. The using statement ensures objects  
        // are automatically disposed from memory after use.  
        using (Bitmap bmp1 = new Bitmap(@"C:\TestPhoto.jpg"))
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID  
            // for the Quality parameter category.  
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.  
            // An EncoderParameters object has an array of EncoderParameter  
            // objects. In this case, there is only one  
            // EncoderParameter object in the array.  
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"c:\TestPhotoQualityFifty.jpg", jpgEncoder, myEncoderParameters);

            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"C:\TestPhotoQualityHundred.jpg", jpgEncoder, myEncoderParameters);

            // Save the bitmap as a JPG file with zero quality level compression.  
            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(@"C:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);
        }
    }*/
}
