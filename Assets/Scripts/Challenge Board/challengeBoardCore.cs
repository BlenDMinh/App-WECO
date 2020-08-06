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
        List<Challenge> challenge = new List<Challenge>();
        //debugText.text = Application.streamingAssetsPath;
        /*foreach (string path in challengeFilePath) {
            debugText.text += path + "\n";
            challenge.Add(JsonUtility.FromJson<Challenge>(new StreamReader(path).ReadToEnd()));
        }*/

        challenge = JsonConvert.DeserializeObject<List<Challenge>>((Resources.Load("challengeList") as TextAsset).ToString());

        challengesContent.transform.position = new Vector2(0, buttonParent.parent.parent.transform.position.y);
        challengesContent.sizeDelta = new Vector2(challengesContent.rect.width, System.Math.Abs(665 - 350 * challenge.Count - (365 + 150)) + 150);
        float curY = (challengesContent.transform.position.y + 350) * buttonParent.parent.parent.transform.localScale.y;

        for(int i = 0; i < challenge.Count; i++) {
            Challenge c = challenge[i];
            curY -= 350 * buttonParent.parent.parent.transform.localScale.y;
            Button button = Instantiate(challengeButton, buttonParent);
            button.transform.SetParent(buttonParent);
            button.transform.position = new Vector2(0, curY);
            Debug.Log("\\challengeTexture\\" + c.challengeName + "_button");
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>("challengeTexture\\" + c.challengeName + "_button");
            button.GetComponent<Image>().type = Image.Type.Simple;
            button.GetComponent<Image>().preserveAspect = true;
            string data = JsonConvert.SerializeObject(c);
            if(c.challengeName != "Coming soon")
                button.onClick.AddListener(delegate { ToChallenge(data); });
        }
    }

    void ToChallenge(string writeData) {
        Directory.CreateDirectory(Application.persistentDataPath + "\\Data\\");
        File.WriteAllText(Application.persistentDataPath + "\\Data\\challenge.json", writeData);
        SceneManager.LoadScene("Journey");
    }
}