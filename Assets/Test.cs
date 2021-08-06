using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

/*
 * 
 * 
 * 
 * 
 * 
 * TODO:    get đúng cái link nữa là ok :)
 * wtf :)
 * Hình như, chưa load xong path
 * ddoi
 * de anh test may anh @@
 *  
 * 
 * 
 * 
 */
public class Test : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        GetImage();
    }

    string path;
    void GetImage() {
        Debug.Log(1);
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference pathRef = storage.GetReference("images/20210731055838368.png");
        pathRef.GetDownloadUrlAsync().ContinueWith(task => {
            if (!task.IsFaulted && !task.IsCanceled) {
                path = task.Result.ToString();
                Debug.Log(path);
                //aaaa();
                StartCoroutine(WaitRequest());
            }
        });
        
             
    }
    UnityWebRequest getImage;

    IEnumerator WaitRequest() {
        if(!String.IsNullOrEmpty(path)) {
            Debug.Log("Getting texture");
            getImage = UnityWebRequestTexture.GetTexture(path);
            getImage.SendWebRequest();
            while (!getImage.isDone)
                yield return null;

            lone();
        }
    }

    void aaaa() {
        if (!String.IsNullOrEmpty(path)) {
            Debug.Log(1);
            getImage = UnityWebRequestTexture.GetTexture(path);
            getImage.SendWebRequest();

            //my code

        }
    }

    void lone() {
        Debug.Log("call");
        if (getImage.isHttpError || getImage.isNetworkError) {
            Debug.Log(getImage.error);
        }
        else {
            Debug.Log(1);
        }

        Texture2D a = ((DownloadHandlerTexture)getImage.downloadHandler).texture;
        image.sprite = Sprite.Create(a, new Rect(0.0f, 0.0f, a.width, a.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
    IEnumerator anothet() {
        Debug.Log(2);
        if (String.IsNullOrEmpty(path)) {
            //UnityWebRequest getImage = UnityWebRequestTexture.GetTexture("https://firebasestorage.googleapis.com/v0/b/wecopia-c8f12.appspot.com/o/images%2F20210731055838368.png");
            UnityWebRequest getImage = UnityWebRequestTexture.GetTexture(path);
            Debug.Log(3);
            yield return getImage.SendWebRequest();
            if (getImage.isHttpError || getImage.isNetworkError) {
                Debug.Log(getImage.error);
            }
            else {
                Debug.Log("wtf");
                Texture2D a = ((DownloadHandlerTexture)getImage.downloadHandler).texture;
                image.sprite = Sprite.Create(a, new Rect(0.0f, 0.0f, a.width, a.height), new Vector2(0.5f, 0.5f), 100.0f);
            }
        }
        else {
            Debug.Log(4);
            yield break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
