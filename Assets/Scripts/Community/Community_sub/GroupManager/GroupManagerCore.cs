using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroupManagerCore : MonoBehaviour {

    [SerializeField]
    private GameObject category_prefab, group_prefab, Categories_Board;

    private List<GameObject> categories = new List<GameObject>();

    async void Start() {
        /* todo: Download and Load CommunityUserData when start
        ...
        */

        // delete this later
        CommunityUserData user = new CommunityUserData();
        user.groups.Add(new Group("0000001", 0));
        // delete this later

        // Create User's Category GameObject
        foreach (string category_name in user.categories)
            CreateCategory(category_name);

        // Create User's Groups GameObject
        foreach (Group group in user.groups)
            await CreateGroup(group.id, group.catagory_id);
    }

    private void CreateCategory(string category_name) {
        GameObject category = Instantiate(category_prefab, Categories_Board.transform, false);
        category.transform.GetChild(0).GetComponent<Text>().text = category_name;
        categories.Add(category);
    }

    private async System.Threading.Tasks.Task CreateGroup(string group_id, int cid) {

        string json = await FirebaseHelper.DownloadString($"groups/{group_id}.json");
        GroupInfo groupInfo = JsonConvert.DeserializeObject<GroupInfo>(json);

        string group_name = groupInfo.name;
        GameObject category = categories[cid];

        GameObject group = Instantiate(group_prefab, category.transform.GetChild(1), false);

        // todo: Set image to GameObject GroupImage
        //

        group.transform.GetChild(1).GetComponent<Text>().text = group_name;
        group.GetComponent<Button>().onClick.AddListener(delegate { EnterGroup(groupInfo); });
    }

    void EnterGroup(GroupInfo groupInfo) {
        PlayerPrefs.SetString("GroupInfo", JsonConvert.SerializeObject(groupInfo));
        SceneManager.LoadSceneAsync("GroupView");
    }
}
