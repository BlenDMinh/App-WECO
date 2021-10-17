using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;

public class CommunityUserData {

    public int num_posts;
    public List<string> post_scope; // where you get posts from. Ex: from your fiend: "posts/user/{friend_ID}"

    public string uid, name, avatar;
    public List<Group> groups; // Groups which user have joined
    public List<string> categories;
    // default category: Joined Groups, Your Groups

    public CommunityUserData() {
        num_posts = 0;
        groups = new List<Group>();
        post_scope = new List<string>() {
            $"posts/weco/",
        };
        categories = new List<string>(2){"Joined Groups", "Your Groups"};
    }

    public CommunityUserData(string uid, string name, string avatar) {
        this.avatar = avatar;
        this.uid = uid;
        this.name = name;
        num_posts = 0;
        groups = new List<Group>();
        post_scope = new List<string>() {
            $"posts/weco/",
            $"posts/user/{uid}/"
        };
        categories = new List<string>(2) { "Joined Groups", "Your Groups" };
    }

    public void WriteToServer() {
        string json = JsonConvert.SerializeObject(this);
        FirebaseHelper.UploadString(json, $"{uid}.json", "CommunityUserData/");
        Debug.Log("Successfully saved CUserData to server");
    }

    public void WriteToCache() {
        string json = JsonConvert.SerializeObject(this), path;
        path = Application.persistentDataPath + "//Data//CommunityUserData.json";

        Debug.Log(path);

        Directory.CreateDirectory(Application.persistentDataPath + "//Data//");
        File.WriteAllText(path, json);
        Debug.Log("Succesfully saved CUserData to local");
    }

    public static CommunityUserData LoadFromCache() {
        string path;
        path = Application.persistentDataPath + "//Data//CommunityUserData.json";

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

    public async static System.Threading.Tasks.Task<CommunityUserData> LoadFromServer(string uid) {
        string json = await FirebaseHelper.DownloadString($"CommunityUserData/{uid}.json");
        CommunityUserData user = JsonConvert.DeserializeObject<CommunityUserData>(json);
        return user;
    }

    public static void WriteToCache(string json) {
        string path;
        path = Application.persistentDataPath + "//Data//CommunityUserData.json";
        Debug.Log(path);
        Directory.CreateDirectory(Application.persistentDataPath + "//Data//");
        File.WriteAllText(path, json);
        Debug.Log("Succesfully saved CUserData to local");
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
