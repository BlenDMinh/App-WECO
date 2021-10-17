using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CommunityCore : MonoBehaviour {

    [SerializeField]
    private PostHandler postHandler;

    public static CommunityUserData user;

    void Start() {
        user = CommunityUserDataHandler.Instance.user;
        Debug.Log(user.name + " " + user.avatar);
        foreach(string scope in user.post_scope)
            DownloadPosts(scope);
    }

    private async void DownloadPosts(string path) {
        bool outOfPost = false;
        int id = 0;
        while(!outOfPost) {
            byte[] output = await FirebaseHelper.DownloadBytes($"{path}{id}.json");
            string json = Encoding.ASCII.GetString(output);
            await postHandler.AddPost(json);
            if (output == null)
                outOfPost = true;
            id++;
        }
    }
}