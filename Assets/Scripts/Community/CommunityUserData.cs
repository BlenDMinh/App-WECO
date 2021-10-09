using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CommunityUserData {
    public string uid, name, avatar;
    public List<Group> groups; // Groups which user have joined
    public List<string> categories;
    // default category: Joined Groups, Your Groups

    public CommunityUserData() {
        groups = new List<Group>();
        categories = new List<string>(2){"Joined Groups", "Your Groups"};
    }

    public void WriteToCache() {
        string json = JsonConvert.SerializeObject(this), path;
        if (Application.platform == RuntimePlatform.Android)
            path = Application.persistentDataPath + "//Data//CommunityUserData.json";
        else
            path = Application.dataPath + "//Data//CommunityUserData.json";

        File.WriteAllText(path, json);
    }

    public static CommunityUserData LoadFromCache() {
        string path;
        if (Application.platform == RuntimePlatform.Android)
            path = Application.persistentDataPath + "//Data//CommunityUserData.json";
        else
            path = Application.dataPath + "//Data//CommunityUserData.json";

        if (!File.Exists(path)) {
            Debug.Log("Error: Couldn't find Local Community User Data");
            return null;
        }

        FileStream JsonFile = new FileStream(path, FileMode.Open);

        StreamReader reader = new StreamReader(JsonFile);
        string json = reader.ReadToEnd();
        CommunityUserData user = JsonConvert.DeserializeObject<CommunityUserData>(json);
        reader.Close();
        return user;
    }

    public static void WriteToCache(string json) {
        string path;
        if (Application.platform == RuntimePlatform.Android)
            path = Application.persistentDataPath + "//Data//CommunityUserData.json";
        else
            path = Application.dataPath + "//Data//CommunityUserData.json";

        File.WriteAllText(path, json);
    }
}

public class Group {
    public string id; // group index
    public int catagory_id; // index of user's category where this group in (default = 0)
    public Group() {
        id = "-1";
        catagory_id = -1;
    }

    public Group(string id, int cid) {
        this.id = id;
        catagory_id = cid;
    }
}
