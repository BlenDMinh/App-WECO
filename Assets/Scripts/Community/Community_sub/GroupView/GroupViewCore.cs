using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GroupViewCore : MonoBehaviour {
    [SerializeField]
    private Text GroupName;

    [SerializeField]
    PostHandler postHandler;

    private GroupInfo groupInfo;
    // Start is called before the first frame update
    async void Start() {
        // Debug
        //FirebaseHelper.TestingVoid();
        // Debug

        groupInfo = JsonConvert.DeserializeObject<GroupInfo>(PlayerPrefs.GetString("GroupInfo"));
        GroupName.text = groupInfo.name;

        List<string> postJsons = new List<string>();
        // Download Post

        FirebaseHelper.DownloadBytes("groups/0000001.json");

        // Download Post

        foreach (string json in postJsons)
            await postHandler.AddPost(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
