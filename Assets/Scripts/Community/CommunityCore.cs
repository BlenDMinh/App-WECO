using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CommunityCore : MonoBehaviour {
    void Start() {
        //DownloadPost();
        DownloadImage();
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
        Debug.Log("set image");
        Sprite sprite = IMG2Sprite.LoadNewSprite("D:/test.png");
        testSprite.sprite = sprite;
    }
}