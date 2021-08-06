using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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

    public PostElement loadAddress(string address){
        Address = address;
	    StreamReader r = new StreamReader(Address);
		string json = r.ReadToEnd();
		PostElement res = JsonConvert.DeserializeObject<PostElement>(json);
		r.Close();
		return res;
    }

    public void updateAll() {
        Text post = transform.GetChild(3).GetComponent<Text>();
        if(ImageQueue.Count > 0) {
            transform.GetChild(4).gameObject.SetActive(true);
            foreach (GameObject image in ImageQueue) {
                image.transform.SetParent(transform.GetChild(4));
                image.transform.localScale = new Vector3(1, 1, 1);
            }
        } else
            transform.GetChild(4).gameObject.SetActive(false);
        post.text = postText;
    }
}
