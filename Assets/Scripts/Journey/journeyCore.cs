using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class journeyCore : MonoBehaviour {

    [SerializeField]
    private Button button;
    public Image i;
    public ScrollRect scrollback;
    public Sprite completed, forbidden;

    private UserData user;
    private Challenge challenge;
    private Sprite bgSprite;
    private void Start() {
        user = UserData.LoadUserData();
        challenge = Challenge.LoadCurrentChallenge();
        bgSprite = Resources.Load<Sprite>("challengeTexture\\" + challenge.challengeName + "_BG");
        i.gameObject.SetActive(false);
        i.sprite = bgSprite;
        i.GetComponent<RectTransform>().sizeDelta = new Vector2(challenge.bgW, challenge.bgH);

        RectTransform content = scrollback.content;
        //Set Size of Content to default size
        //height = number of task * (20% of Screen.height)
        content.sizeDelta = new Vector2(Screen.width, challenge.tasks.Count * Screen.height * 20 / 100);

        int id = 0, skip = 1;
        List<bool> progress = UserData.GetChallengeProgress(user, challenge);
            
        foreach (Task t in challenge.tasks) {
            t.id = id;
            CreateButton(t, bgSprite);
            if (progress[id]) {
                CreateHover(t, bgSprite, completed);
            } else if (skip == 0) {
                CreateHover(t, bgSprite, forbidden);
            } else {
                skip--;
            }
            id++;
        }


    }

    private void CreateHover(Task t, Sprite bgSprite, Sprite sprite) {
        GameObject newImage = new GameObject("hover", typeof(Image));
        newImage.transform.SetParent(scrollback.content);
        newImage.GetComponent<Image>().sprite = sprite;
        RectTransform r = newImage.GetComponent<RectTransform>();

        r.localScale = new Vector2(1, 1);
        //r.pivot = new Vector2(0, 1);
        r.sizeDelta = new Vector2(Screen.width * 25 / 100, Screen.width * 25 / 100);
        //r.anchoredPosition = new Vector2((t.x / bgSprite.rect.width) * challenge.bgW - challenge.btW * 10 / 100, -(t.y / bgSprite.rect.height) * challenge.bgH + challenge.btH * 10 / 100);
        r.anchoredPosition = new Vector2(Screen.width / 2, -Screen.height * t.id * 20 / 100);
        r.anchorMin = r.anchorMax = new Vector2(0, 1);
    }

    private void CreateButton(Task t, Sprite bgSprite) {
        Button newButton = Instantiate(button, scrollback.content);
        RectTransform r = newButton.GetComponent<RectTransform>();

        r.localScale = new Vector2(1, 1);
        r.sizeDelta = new Vector2(Screen.width * 20 / 100, Screen.width * 20 / 100);
        r.anchoredPosition = new Vector2(Screen.width / 2, -Screen.height * t.id * 20 / 100);
        r.anchorMin = r.anchorMax = new Vector2(0, 1);

        newButton.onClick.AddListener(delegate {
            string writeData = JsonConvert.SerializeObject(t);
            if (Application.platform == RuntimePlatform.Android) {
                Directory.CreateDirectory(Application.persistentDataPath + "//Data//");
                File.WriteAllText(Application.persistentDataPath + "//Data//task.json", writeData);
            } else {
                Directory.CreateDirectory(Application.dataPath + "//Data//");
                File.WriteAllText(Application.dataPath + "//Data//task.json", writeData);
            }
            SceneManager.LoadScene("Task", LoadSceneMode.Additive);
        });
    }
}
