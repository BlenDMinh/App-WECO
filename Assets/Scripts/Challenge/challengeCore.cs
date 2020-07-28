using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class challengeCore : MonoBehaviour {

    private Challenge challenge;

    public Text challengeName;
    public Text challengeDes;
    
    private Challenge ReadChallenge() {
        StreamReader r = new StreamReader(Application.dataPath + "\\challenge.json");
        string json = r.ReadToEnd();
        Challenge res = JsonUtility.FromJson<Challenge>(json);
        return res;
    }
    void Start() {

        //request challenge from server

        //request challenge from server

        challenge = ReadChallenge();

        challengeName.text = challenge.challengeName;
        challengeDes.text = challenge.challengeDescription;


    }

    // Update is called once per frame
    void Update() {
        
    }
}
