using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CommunityCore : MonoBehaviour {

    [SerializeField]
    private PostHandler postHandler;

    void Start() {
        DownloadPosts();
    }

    private async void DownloadPosts() {
        bool outOfPost = false;
        int id = 0;
        while(!outOfPost) {
            byte[] output = await FirebaseHelper.DownloadBytes($"/posts/test/{id}.json");
            string json = Encoding.ASCII.GetString(output);
            await postHandler.AddPost(json);
            if (output == null)
                outOfPost = true;
            id++;
        }
    }
}