using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class challengesBoardCore : MonoBehaviour {
    
    public Button challengeButton;
    public Transform buttonParent;
    public RectTransform challengesContent;
    void Start() {
        float curY = 665 * buttonParent.parent.parent.transform.localScale.y;

        //Read all challenges in to List<Challenge> challenge
        List<Challenge> challenge = new List<Challenge>();
        string[] challengeFilePath = Directory.GetFiles(Application.dataPath + "\\Challenges", "*.cha");
        foreach(string path in challengeFilePath)
            challenge.Add(JsonUtility.FromJson<Challenge>(new StreamReader(path).ReadToEnd()));

        challengesContent.sizeDelta = new Vector2(challengesContent.rect.width, System.Math.Abs(665 - 300 * challenge.Count - (365 + 150)) + 150);

        foreach (Challenge c in challenge) {
            curY -= 300 * buttonParent.parent.parent.transform.localScale.y;
            Button button = Instantiate(challengeButton, buttonParent);
            button.transform.SetParent(buttonParent);
            button.transform.position = new Vector2(0, curY);
        }
        
    }
    void Update() {
        
    }
}