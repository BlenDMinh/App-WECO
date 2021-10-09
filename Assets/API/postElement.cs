using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PostElement : MonoBehaviour {
    public Image userAvatar;
    public Image postImage;
    public string userName;
    public string postText;
    public string Address;
    public List<Image> ImageQueue;
    public List<string> imagePaths;
    public Text text;
    public PostElement loadAddress(string address){
        Address = address;
	    StreamReader r = new StreamReader(Address);
		string json = r.ReadToEnd();
		PostElement res = JsonConvert.DeserializeObject<PostElement>(json);
		r.Close();
		return res;
    }

    public PostElement() {
        postText = "";
        ImageQueue = new List<Image>();
    }

    public async Task<PostElement> LoadFromData(PostData postData) {
        this.postText = postData.postText;
        await FirebaseHelper.DownloadCacheImages(postData.imageNames);
        foreach (string image in postData.imageNames)
            this.ImageQueue.Add(UIHelper.LoadImage($"{FirebaseHelper.LocalCacheImagePath}{image}"));
        return this;
    }

    public async Task<PostElement> LoadFromData(PostData postData, bool fromLocal) {
        if (!fromLocal)
            return await LoadFromData(postData);
        this.postText = postData.postText;
        foreach (string image in postData.imageNames)
            this.ImageQueue.Add(UIHelper.LoadImage($"{image}"));
        return this;
    }

    [SerializeField]
    private List<GameObject> cig;

    public void updateAll() {
        CropImagesGroup cropImageGroup = new CropImagesGroup();
        if(ImageQueue.Count < 4) {
            GameObject cig_o = Instantiate(cig[ImageQueue.Count - 1], transform.GetChild(2), false);
            cropImageGroup = cig_o.GetComponent<CropImagesGroup>();
        }

        for (int i = 0; i < Mathf.Min(ImageQueue.Count, 3); i++) {
            cropImageGroup.cropImages[i].image.sprite = ImageQueue[i].GetComponent<Image>().sprite;
            cropImageGroup.cropImages[i].FitCanvas();
        }

        text.text = postText;
    }
}
