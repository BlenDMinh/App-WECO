using System.Collections;
using UnityEngine;
using TMPro;
using Firebase.Storage;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.UI;

public class PostButton : MonoBehaviour {
    [SerializeField]
    private TMP_Text postText;
    
    [SerializeField]
    private GameObject post, content, imgBoard, uploadQueue;

    private GameObject clone;
    
    public void Post() {
        clone = UIHelper.PushAndGetPrefabToParent(post, content.transform, 0);
        PostElement pe = clone.GetComponent<PostElement>();
        pe.postText = postText.text;

        // Upload Text
        FirebaseStorage storage = FirebaseStorage.DefaultInstance;
        StorageReference postTextRef = storage.RootReference.Child("posts/" + "username" + "/" + "nextID" + "/" + "postText");

        byte[] postTextbytes = Encoding.ASCII.GetBytes(pe.postText);

        postTextRef.PutBytesAsync(postTextbytes).ContinueWith((Task<StorageMetadata> task) => {
            if (task.IsFaulted || task.IsCanceled) {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                StorageMetadata metadata = task.Result;
                string md5Hash = metadata.Md5Hash;
                Debug.Log("Finished uploading...");
                Debug.Log("md5 hash = " + md5Hash);
            }
        });

        if (imgBoard.transform.childCount > 0) { //there're images
            foreach(Transform child in imgBoard.transform)
                pe.ImageQueue.Add(child.gameObject.GetComponent<Image>());
        }
        
        StartCoroutine(SiblingUpdate());
        pe.updateAll();
    }

    private IEnumerator SiblingUpdate() {
        yield return 0;
        clone.transform.SetSiblingIndex(2);
        transform.parent.gameObject.SetActive(false);
        content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}
