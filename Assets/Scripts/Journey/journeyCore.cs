using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class journeyCore : MonoBehaviour {

    [SerializeField]
    private Button button;
    public Image i;
    public ScrollRect scrollback;
    private Challenge challenge;
    private float scale;
    private void Start() {
        scale = scrollback.transform.parent.transform.localScale.x;
        challenge = Challenge.LoadCurrentChallenge();
        Sprite sprite = Resources.Load<Sprite>("challengeTexture\\" + challenge.challengeName + "_BG");
        i.sprite = sprite;
        i.GetComponent<RectTransform>().sizeDelta = new Vector2(challenge.bgW, challenge.bgH);

        RectTransform content = scrollback.content;
        content.sizeDelta = i.rectTransform.rect.size;

        foreach(Task t in challenge.tasks) {
            Button newButton = Instantiate(button, scrollback.content);
            RectTransform r = newButton.GetComponent<RectTransform>();

            r.sizeDelta = new Vector2(challenge.btW, challenge.btH);
            r.anchoredPosition = new Vector2((t.x / sprite.rect.width) * challenge.bgW * scale, -(t.y / sprite.rect.height) * challenge.bgH * scale);
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
            });
        }
    }
}
