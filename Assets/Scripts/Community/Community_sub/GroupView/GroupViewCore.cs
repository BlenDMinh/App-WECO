using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GroupViewCore : MonoBehaviour {
    [SerializeField]
    private Text GroupName;

    private GroupInfo groupInfo;
    // Start is called before the first frame update
    void Start() {
        groupInfo = JsonConvert.DeserializeObject<GroupInfo>(PlayerPrefs.GetString("GroupInfo"));
        GroupName.text = groupInfo.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
