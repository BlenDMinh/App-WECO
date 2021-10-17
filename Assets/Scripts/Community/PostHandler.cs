using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PostHandler : MonoBehaviour {

    private PostData postData = new PostData();

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

    [SerializeField]
    private GameObject cropImage_prefab;

    private void LoadImage(string path) {
        if (path == null)
            return;

        if (postData.imageNames == null)
            postData.imageNames = new List<string>();

        postData.imageNames.Add(path);

        GameObject cropImage_obj = Instantiate(cropImage_prefab, imgBoard, false);
        cropImage_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(imgBoard.gameObject.GetComponent<RectTransform>().sizeDelta.y, imgBoard.gameObject.GetComponent<RectTransform>().sizeDelta.y);

        CropImage cropImage = cropImage_obj.GetComponent<CropImage>();
        cropImage.image.sprite = IMG2Sprite.LoadNewSprite(path);

        cropImage.GetComponent<CropImage>().FitCanvas();
    }

    private void LoadImages(string[] paths) {
        if (paths.Length < 1)
            return;
        foreach (string path in paths)
            LoadImage(path);
    }

    [SerializeField]
    private Text postText;

    [SerializeField]
    private GameObject postElementPrefab, postContent;

    private GameObject clone;

    public void Post() {
        postData.uid = CommunityUserDataHandler.Instance.user.uid;
        postData.postText = postText.text;

        // Add postElementPrefab to main scene
        clone = UIHelper.PushAndGetPrefabToParent(postElementPrefab, postContent.transform, 0);
        PostElement pe = clone.GetComponent<PostElement>();
        pe.LoadFromData(postData, true);

        PostData.UploadPostData(postData);

        // Clear
        postData.imageNames = new List<string>();

        StartCoroutine(SiblingUpdate());
        pe.updateAll();
    }

    private IEnumerator SiblingUpdate() {
        yield return new WaitForSeconds(1 / 60);
        clone.transform.SetParent(empty, false);
        yield return new WaitForSeconds(1 / 60);
        clone.transform.SetParent(postContent.transform, false);
        yield return new WaitForSeconds(1 / 60);
        clone.transform.SetSiblingIndex(0);

        PostingPanel.SetActive(false);
    }

    public void CancelPost() {
        postData.imageNames = new List<string>();
    }

    [SerializeField]
    GameObject PostingPanel;

    //Update posts on main scene

    public Transform empty; 
    public async System.Threading.Tasks.Task AddPost(string json) {
        PostData data = JsonConvert.DeserializeObject<PostData>(json);
        GameObject postClone = UIHelper.PushAndGetPrefabToParent(postElementPrefab, postContent.transform, 0);
        PostElement pe = postClone.GetComponent<PostElement>();
        pe = await pe.LoadFromData(data);
        pe.updateAll();
        StartCoroutine(UpdatePostPostion(postClone));
    }

    private IEnumerator UpdatePostPostion(GameObject postElementPrefab) {
        yield return new WaitForSeconds(1 / 60);
        postElementPrefab.transform.SetParent(empty, false);
        yield return new WaitForSeconds(1 / 60);
        postElementPrefab.transform.SetParent(postContent.transform, false);
    }
}

public class PostData {
    public string uid;
    public string postText;
    public List<string> imageNames;

    public static void UploadPostData(PostData data) {
        if (data.imageNames != null)
            for (int i = 0; i < data.imageNames.Count; i++) {
                string image = data.imageNames[i];

                string imgType = Path.GetExtension(image);
                string datePatt = @"yyyyMMddHHmmssfff";
                string imgName = DateTime.UtcNow.ToString(datePatt, CultureInfo.InvariantCulture) + imgType;

                FirebaseHelper.UploadFile(image, imgName, "images");

                data.imageNames[i] = imgName;
            }

        string json = JsonConvert.SerializeObject(data);
        byte[] postTextbytes = Encoding.ASCII.GetBytes(json);
        CommunityUserData user = CommunityUserDataHandler.Instance.user;

        FirebaseHelper.UploadBytes(postTextbytes, $"{user.num_posts}.json", $"posts/user/{user.uid}/");
        user.num_posts++;
    }
}
