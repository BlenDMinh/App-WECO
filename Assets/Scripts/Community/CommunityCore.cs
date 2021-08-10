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
        DownloadImage();
    }

    private async void DownloadPosts() {
        bool outOfPost = false;
        int id = 0;
        while(!outOfPost) {
            byte[] output = await FirebaseHelper.DownloadBytes($"/posts/test/{id}.json");

            string json = Encoding.ASCII.GetString(output);
            Debug.Log(json);

            await postHandler.AddPost(json);

            if (output == null)
                outOfPost = true;
            id++;
        }
    }

    private async System.Threading.Tasks.Task DownloadPost() {
        Task<byte[]> task = FirebaseHelper.DownloadBytes("/posts/username/nextID/post.json");
        byte[] output = await task;
        string json = Encoding.ASCII.GetString(output);
        Debug.Log(json);
    }

    [SerializeField]
    private Image testSprite;

    private async System.Threading.Tasks.Task DownloadImage() {
        System.Threading.Tasks.Task task = FirebaseHelper.DownloadFile("/images/20210731055838368.png", "D:/test.png");
        await task;
    }
}