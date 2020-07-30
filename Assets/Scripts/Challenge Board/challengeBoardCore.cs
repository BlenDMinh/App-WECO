using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class challengeBoardCore : MonoBehaviour {
    public Button challengeButton;
    public Transform buttonParent;
    public RectTransform challengesContent;
    void Start() {

        //Generate buttons
        float curY = 665 * buttonParent.parent.parent.transform.localScale.y;

        //Read all challenges in to List<Challenge> challenge
        List<Challenge> challenge = new List<Challenge>();
        string[] challengeFilePath = Directory.GetFiles(Application.dataPath + "\\Challenges", "*.cha");
        foreach(string path in challengeFilePath)
            challenge.Add(JsonUtility.FromJson<Challenge>(new StreamReader(path).ReadToEnd()));

        challengesContent.sizeDelta = new Vector2(challengesContent.rect.width, System.Math.Abs(665 - 300 * challenge.Count - (365 + 150)) + 150);

        for(int i = 0; i < challengeFilePath.Length; i++) {
            Challenge c = challenge[i];
            curY -= 300 * buttonParent.parent.parent.transform.localScale.y;
            Button button = Instantiate(challengeButton, buttonParent);
            button.transform.SetParent(buttonParent);
            button.transform.position = new Vector2(0, curY);
            button.GetComponentInChildren<Text>().text = c.challengeName;
            string path = challengeFilePath[i];
            button.onClick.AddListener(delegate { ToChallenge(path); });
        }
    }

    void ToChallenge(string challengePath) {
        Debug.Log(challengePath);
        File.Copy(challengePath, Application.dataPath + "\\challenge.json", true);
        SceneManager.LoadScene("Challenge");
    }
}