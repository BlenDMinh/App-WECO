using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupManagerCore : MonoBehaviour {

    [SerializeField]
    private GameObject category_prefab, group_prefab, Categories_Board;

    private List<GameObject> categories = new List<GameObject>();

    void Start() {
        /* todo: Download and Load CommunityUserData when start
        ...
        */

        // delete this later
        CommunityUserData user = new CommunityUserData();
        user.groups.Add(new Group("0", 0));
        user.groups.Add(new Group("1", 0));
        user.groups.Add(new Group("2", 0));
        // delete this later

        // Create User's Category GameObject
        foreach (string category_name in user.categories)
            CreateCategory(category_name);

        // Create User's Groups GameObject
        foreach (Group group in user.groups)
            CreateGroup(group.id, group.at_catagory);
    }

    private void CreateCategory(string category_name) {
        GameObject category = Instantiate(category_prefab, Categories_Board.transform, false);
        category.transform.GetChild(0).GetComponent<Text>().text = category_name;
        categories.Add(category);
    }

    private void CreateGroup(string group_id, int cid) {
        // todo: Get group name from id
        //

        string group_name = "Well this is just a decoy";
        GameObject category = categories[cid];

        GameObject group = Instantiate(group_prefab, category.transform.GetChild(1), false);

        // todo: Set image to GameObject GroupImage
        //

        group.transform.GetChild(1).GetComponent<Text>().text = group_name;
    }
}
