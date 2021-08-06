using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostHandler : MonoBehaviour {

    private Upload upload = new Upload();

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

        if (upload.imageNames == null)
            upload.imageNames = new List<string>();

        upload.imageNames.Add(path);

        Sprite sprite = IMG2Sprite.LoadNewSprite(path);
        GameObject image = new GameObject("Image");
        image.transform.SetParent(imgBoard);
        image.AddComponent<Image>().sprite = sprite;
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
        upload.postText = postText.text;

        // Add uploading post to main scene
        clone = UIHelper.PushAndGetPrefabToParent(post, content.transform, 0);
        PostElement pe = clone.GetComponent<PostElement>();
        pe.postText = postText.text;

        if (imgBoard.transform.childCount > 0) {
            foreach (Transform child in imgBoard.transform)
                pe.ImageQueue.Add(child.gameObject);
        }


        //Process images here

        for(int i = 0; i < upload.imageNames.Count; i++) {
            string image = upload.imageNames[i];

            string imgType = System.IO.Path.GetExtension(image);
            string datePatt = @"yyyyMMddHHmmssfff";
            string imgName = DateTime.UtcNow.ToString(datePatt, CultureInfo.InvariantCulture) + imgType;

            FirebaseHelper.UploadFile(image, imgName, "images");

            upload.imageNames[i] = imgName;
        }
        //Process images here

        string json = JsonConvert.SerializeObject(upload);

        // Upload

        byte[] postTextbytes = Encoding.ASCII.GetBytes(json);
        const string nextPostID = "0";
        FirebaseHelper.UploadBytes(postTextbytes, "data.json", "posts/user/uid/" + nextPostID);

        // Clear
        upload.imageNames = new List<string>();

        StartCoroutine(SiblingUpdate());
        pe.updateAll();
    }

    public void CancelPost() {
        upload.imageNames = new List<string>();
    }

    //Update posts on main scene
    private IEnumerator SiblingUpdate() {
        yield return 0;
        clone.transform.SetSiblingIndex(2);
        transform.parent.gameObject.SetActive(false);
    }
}

class Upload {
    public string username;
    public string postText;
    public List<string> imageNames;
}
