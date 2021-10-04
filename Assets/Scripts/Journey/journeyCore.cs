using Newtonsoft.Json;
using System.Collections;
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

    private List<Vector3> pastPos = new List<Vector3>() {new Vector3(0,0,0)}; //supposed to be edge of map but this still works somehow

    private void Start() {
        // Load ongoing tasks
        user = UserData.LoadUserData();
        challenge = Challenge.LoadCurrentChallenge();
        bgSprite = Resources.Load<Sprite>("challengeTexture\\" + challenge.challengeName + "_BG");
        i.sprite = bgSprite;
        i.GetComponent<RectTransform>().sizeDelta = new Vector2(challenge.bgW, challenge.bgH);

        StartCoroutine(Init());
    }

    private void CreateHover(Task t, Sprite bgSprite, Sprite sprite) {
        GameObject newImage = new GameObject("hover", typeof(Image));
        newImage.transform.SetParent(scrollback.content);
        newImage.GetComponent<Image>().sprite = sprite;
        RectTransform r = newImage.GetComponent<RectTransform>();

        r.localScale = new Vector2(1, 1);
        r.pivot = new Vector2(0, 1);
        r.sizeDelta = new Vector2(challenge.btW * 120 / 100, challenge.btH * 120 / 100);
        r.anchoredPosition = new Vector2((t.x / bgSprite.rect.width) * challenge.bgW - challenge.btW * 10 / 100, -(t.y / bgSprite.rect.height) * challenge.bgH + challenge.btH * 10 / 100);
        r.anchorMin = r.anchorMax = new Vector2(0, 1);
    }

    public int ButtonMinDistance = 500; //change this to change minimum distance between nodes. 500 is, from what I've seen, the most consistent. further testing is required

    private void CreateButton(Task t) {
        Vector3 rndPosWithin = new Vector3();
        bool check = false; //check if the new position is farther than minDist compared to others

        //Random position
        while (check != true)
        {
            rndPosWithin = new Vector3(Random.Range(0.2f, 0.8f) * challenge.bgW, Random.Range(-0.8f, -0.2f) * challenge.bgH, 3);
            foreach (Vector3 item in pastPos)
            {
                if (Vector3.Distance(rndPosWithin, item) < ButtonMinDistance)
                {
                    check = false;
                    break; //if break triggers then the check = true below will be skipped, so the check will only return true if this is never tripped
                }
                check = true;
            }
        }
        pastPos.Add(rndPosWithin);

        //rndPosWithin = i.transform.TransformPoint(rndPosWithin * .5f);

        Button newButton = Instantiate(button, rndPosWithin, Quaternion.identity, i.transform);
        RectTransform r = newButton.GetComponent<RectTransform>();

        r.sizeDelta = new Vector2(challenge.btW * 2, challenge.btH * 2);
        r.anchoredPosition = rndPosWithin;

        newButton.onClick.AddListener(delegate {
            string writeData = JsonConvert.SerializeObject(t);
            if (Application.platform == RuntimePlatform.Android) {
                Directory.CreateDirectory(Application.persistentDataPath + "//Data//");
                File.WriteAllText(Application.persistentDataPath + "//Data//task.json", writeData);
            }
            else {
                Directory.CreateDirectory(Application.dataPath + "//Data//");
                File.WriteAllText(Application.dataPath + "//Data//task.json", writeData);
            }
            SceneManager.LoadScene("Task", LoadSceneMode.Additive);
        });
    }

    IEnumerator Init() {
        yield return 0;
        RectTransform content = scrollback.content;
        content.sizeDelta = i.rectTransform.rect.size;

        int id = 0;
        List<bool> progress = UserData.GetChallengeProgress(user, challenge);

        foreach (Task t in challenge.tasks) {

            // Tạo button cho mỗi Task
            t.id = id;
            if (!progress[id]) {
                CreateButton(t);
            }

            id++;
            if (id > 8)
                break;
        }
    }
}