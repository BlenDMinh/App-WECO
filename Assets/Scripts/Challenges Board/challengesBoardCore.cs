using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class challengesBoardCore : MonoBehaviour {

    private float screenScaling = Screen.height / 10;
    
    public Button challengeButton;
    public Transform buttonParent;
    void Start() {
        Debug.Log(screenScaling);
        float curY = 450 / screenScaling;

        //Read all challenges in to List<Challenge> challenge
        List<Challenge> challenge = new List<Challenge>();
        string[] challengeFilePath = Directory.GetFiles(Application.dataPath + "\\Challenges", "*.cha");
        foreach(string path in challengeFilePath)
            challenge.Add(JsonUtility.FromJson<Challenge>(new StreamReader(path).ReadToEnd()));

        foreach(Challenge c in challenge) {
            curY -= 1;
            Button button = Instantiate(challengeButton);
            button.transform.SetParent(buttonParent);
            button.transform.localScale = new Vector2(1, 1);
            button.transform.position = new Vector2(0, curY);
        }
    }
    void Update() {
        
    }
}