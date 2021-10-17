using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommunityUserDataHandler : MonoBehaviour {

    public CommunityUserData user;
    public static CommunityUserDataHandler Instance;

    public void Start() {
        Debug.Log("Init User Data");
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        user = CommunityUserData.LoadFromCache();
    }

    private void OnApplicationQuit() {
        UpdateCommunityUserData();
    }

    private float time = 0f, period = 50f;

    public void Update() {
        // Update User every 5 seconds
        time += Time.deltaTime;
        if(time >= period) {
            Debug.Log("Backup Data");
            UpdateCommunityUserData();
            time = 0f;
        }
    }

    private void UpdateCommunityUserData() {
        user.WriteToCache();
        user.WriteToServer();
    }
}
