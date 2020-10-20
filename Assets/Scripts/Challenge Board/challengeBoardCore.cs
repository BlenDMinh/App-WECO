using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class challengeBoardCore : MonoBehaviour {
    public Button challengeButton;
    public RectTransform challengesContent;
    void Start() {
        //Generate buttons
        //Read all challenges in to List<Challenge> challenge
        List<string> challenge = new List<string>();
        float buttonHeight = challengeButton.GetComponent<RectTransform>().rect.height;
        challenge = JsonConvert.DeserializeObject<List<string>>((Resources.Load("challengeList") as TextAsset).ToString());

        challengesContent.anchoredPosition = new Vector2(0, 0);
        challengesContent.sizeDelta = new Vector2(challengesContent.rect.width, buttonHeight * challenge.Count);
        float curY = buttonHeight;

        for(int i = 0; i < challenge.Count; i++) {
            curY -= buttonHeight;
            Button button = Instantiate(challengeButton, challengesContent);
            button.transform.SetParent(challengesContent);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, curY);
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

    private void Update() {
        Debug.Log(challengesContent.anchoredPosition.y);
    }
}