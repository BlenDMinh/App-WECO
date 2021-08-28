using Newtonsoft.Json;
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
    public List<GameObject> ImageQueue;
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
        ImageQueue = new List<GameObject>();
    }

    public async Task<PostElement> LoadFromData(PostData postData) {
        this.postText = postData.postText;
        await FirebaseHelper.DownloadCacheImages(postData.imageNames);
        foreach (string image in postData.imageNames)
            this.ImageQueue.Add(UIHelper.CreateImageObject($"{FirebaseHelper.LocalCacheImagePath}{image}"));
        return this;
    }

    public async Task<PostElement> LoadFromData(PostData postData, bool fromLocal) {
        if (!fromLocal)
            return await LoadFromData(postData);
        this.postText = postData.postText;
        foreach (string image in postData.imageNames)
            this.ImageQueue.Add(UIHelper.CreateImageObject($"{image}"));
        return this;
    }

    public void updateAll() {
        if(ImageQueue.Count > 0) {
            transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            foreach (GameObject image in ImageQueue) {
                image.transform.SetParent(transform.GetChild(2).GetChild(0));
                image.transform.localScale = new Vector3(1, 1, 1);
            }
        } else
            transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        text.text = postText;
    }
}
