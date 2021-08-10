using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;

public class PostHandler : MonoBehaviour {

    private PostData PostData = new PostData();

    // Image Handler
    public void AddImage() { // Open Gallery and add image

        if (NativeGallery.CanSelectMultipleFilesFromGallery()) {
            NativeGallery.MediaPickMultipleCallback multi_callback = new NativeGallery.MediaPickMultipleCallback(LoadImages);
            NativeGallery.GetImagesFromGallery(multi_callback);
            return;
        }

        NativeGallery.MediaPickCallback callback = new NativeGallery.MediaPickCallback(LoadImage);
        NativeGallery.GetImageFromGallery(callback);
    }

    [SerializeField]
    private Transform imgBoard; // Image Board in Posting Panel

    private void LoadImage(string path) {
        if (path == null)
            return;

        if (PostData.imageNames == null)
            PostData.imageNames = new List<string>();

        PostData.imageNames.Add(path);
        GameObject image = UIHelper.CreateImageObject(path);
        image.transform.SetParent(imgBoard);
        image.transform.localScale = new Vector3(1, 1, 1);
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(imgBoard.gameObject.GetComponent<RectTransform>().sizeDelta.y, imgBoard.gameObject.GetComponent<RectTransform>().sizeDelta.y);
    }
    private void LoadImages(string[] paths) {
        if (paths.Length < 1)
            return;
        foreach (string path in paths)
            LoadImage(path);
    }

    [SerializeField]
    private TMP_Text postText;

    [SerializeField]
    private GameObject post, content;

    private GameObject clone;

    public void Post() {
        PostData.postText = postText.text;

        // Add post to main scene
        clone = UIHelper.PushAndGetPrefabToParent(post, content.transform, 0);
        PostElement pe = clone.GetComponent<PostElement>();
        pe.postText = postText.text;

        if (imgBoard.transform.childCount > 0) {
            foreach (Transform child in imgBoard.transform)
                pe.ImageQueue.Add(child.gameObject);
        }


        //Process images here

        for(int i = 0; i < PostData.imageNames.Count; i++) {
            string image = PostData.imageNames[i];

            string imgType = System.IO.Path.GetExtension(image);
            string datePatt = @"yyyyMMddHHmmssfff";
            string imgName = DateTime.UtcNow.ToString(datePatt, CultureInfo.InvariantCulture) + imgType;

            FirebaseHelper.UploadFile(image, imgName, "images");

            PostData.imageNames[i] = imgName;
        }
        //Process images here

        string json = JsonConvert.SerializeObject(PostData);

        // PostData

        byte[] postTextbytes = Encoding.ASCII.GetBytes(json);
        const string nextPostID = "0";
        FirebaseHelper.UploadBytes(postTextbytes, "data.json", "posts/user/uid/" + nextPostID);

        // Clear
        PostData.imageNames = new List<string>();

        StartCoroutine(SiblingUpdate());
        pe.updateAll();
    }

    public void CancelPost() {
        PostData.imageNames = new List<string>();
    }

    //Update posts on main scene
    private IEnumerator SiblingUpdate() {
        yield return 0;
        clone.transform.SetSiblingIndex(2);
        transform.parent.gameObject.SetActive(false);
    }

    public async System.Threading.Tasks.Task AddPost(string json) {
        PostData data = JsonConvert.DeserializeObject<PostData>(json);

        GameObject postClone = UIHelper.PushAndGetPrefabToParent(post, content.transform, 0);
        PostElement pe = postClone.GetComponent<PostElement>();
        pe.postText = data.postText;

        foreach(string image in data.imageNames) {
            string path = Application.persistentDataPath + "cache/images/";
            Directory.CreateDirectory(path);

            if (!File.Exists($"{path}{image}")) {
                Debug.Log($"Can't find image in {path}{image}, start downloading from images/{image}");
                await FirebaseHelper.DownloadFile($"images/{image}", $"{path}{image}");
            }

            pe.ImageQueue.Add(UIHelper.CreateImageObject($"{path}{image}"));
        }

        pe.updateAll();
        StartCoroutine(UpdatePostPostion(postClone));
    }

    private IEnumerator UpdatePostPostion(GameObject post) {
        yield return new WaitForSeconds(0.5f);
        post.transform.SetSiblingIndex(2);
    }

}

class PostData {
    public string username;
    public string postText;
    public List<string> imageNames;
}
