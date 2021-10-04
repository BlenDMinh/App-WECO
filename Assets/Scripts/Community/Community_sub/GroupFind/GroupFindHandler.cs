using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupFindHandler : MonoBehaviour {

    [SerializeField]
    private Text groupID, groupInfo_Name;
    [SerializeField]
    private GameObject FoundGroupPanel;

    public async void Find() {
        string json = await FirebaseHelper.DownloadString($"groups/{groupID.text}.json");
        if(!string.IsNullOrWhiteSpace(json)) {
            FoundGroupPanel.SetActive(true);
            GroupInfo info = JsonConvert.DeserializeObject<GroupInfo>(json);
            groupInfo_Name.text = info.name;
        } else {
            FoundGroupPanel.SetActive(false);
        }
    }
}
