using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
	public class postElement {
        public Image userAvatar { get; set; } 
        public Image postImage { get; set; }
        public string userName { get; set; } 
        public string postText { get; set; } 
        public string Address { get; set; } 

        public postElement loadAddress(){
	        StreamReader r = new StreamReader(Address);
			string json = r.ReadToEnd();
			postElement res = JsonConvert.DeserializeObject<postElement>(json);
			r.Close();
			return res;
        }
    }
}
