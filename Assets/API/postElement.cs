using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PostElement : MonoBehaviour {
    public Image userAvatar { get; set; } 
    public Image postImage { get; set; }
    public string userName { get; set; } 
    public string postText { get; set; } 
    public string Address { get; set; } 

    public PostElement loadAddress(string address){
        Address = address;
	    StreamReader r = new StreamReader(Address);
		string json = r.ReadToEnd();
		PostElement res = JsonConvert.DeserializeObject<PostElement>(json);
		r.Close();
		return res;
    }
}
