using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class journeyCore : MonoBehaviour {

    private Challenge challenge;

    public Text challengeName;
    public Text challengeDes;
    
    void Start() {

        //request challenge from server

        challenge = challenge.ReadCurrentChallenge();

        challengeName.text = challenge.challengeName;
        challengeDes.text = challenge.challengeDescription;

        SceneManager.LoadScene("Progress", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
