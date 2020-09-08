using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class challengeBoardCore : MonoBehaviour {
    public Button challengeButton;
    public Transform buttonParent;
    public RectTransform challengesContent;
    public Text debugText;
    void Start() {

        //Generate buttons

        //Read all challenges in to List<Challenge> challenge
        List<string> challenge = new List<string>();
        //debugText.text = Application.streamingAssetsPath;
        /*foreach (string path in challengeFilePath) {
            debugText.text += path + "\n";
            challenge.Add(JsonUtility.FromJson<Challenge>(new StreamReader(path).ReadToEnd()));
        }*/

        challenge = JsonConvert.DeserializeObject<List<string>>((Resources.Load("challengeList") as TextAsset).ToString());

        challengesContent.transform.position = new Vector2(0, buttonParent.parent.parent.transform.position.y);
        challengesContent.sizeDelta = new Vector2(challengesContent.rect.width, System.Math.Abs(665 - 350 * challenge.Count - (365 + 150)) + 150);
        float curY = (challengesContent.transform.position.y + 350) * buttonParent.parent.parent.transform.localScale.y;

        for(int i = 0; i < challenge.Count; i++) {
            curY -= 350 * buttonParent.parent.parent.transform.localScale.y;
            Button button = Instantiate(challengeButton, buttonParent);
            button.transform.SetParent(buttonParent);
            button.transform.position = new Vector2(0, curY);
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>("challengeTexture\\" + challenge[i] + "_button");
            button.GetComponent<Image>().type = Image.Type.Simple;
            button.GetComponent<Image>().preserveAspect = true;
            string challengeName = challenge[i];
            if(challenge[i] != "Coming soon")
                button.onClick.AddListener(delegate { ToChallenge(challengeName); });
        }
    }

    void ToChallenge(string challengeName) {
        string writeData = (Resources.Load("challenges\\" + challengeName) as TextAsset).text;
        if (Application.platform == RuntimePlatform.Android) {
            Directory.CreateDirectory(Application.persistentDataPath + "//Data//");
            File.WriteAllText(Application.persistentDataPath + "//Data//challenge.json", writeData);
        } else {
            Directory.CreateDirectory(Application.dataPath + "//Data//");
            File.WriteAllText(Application.dataPath + "//Data//challenge.json", writeData);
        }
        SceneManager.LoadScene("Journey");
    }
}