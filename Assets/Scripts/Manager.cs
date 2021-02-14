using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour{
    public static Manager Instance { get; private set; }

    public string currentChallenge;
    public string currentTaskElement;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
