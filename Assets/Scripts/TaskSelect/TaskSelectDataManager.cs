using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelectDataManager : MonoBehaviour {
    public static TaskSelectDataManager Instance { get; private set; }

    public int currentTE_ID = -1; // -1 = not selecting any Task Element

    // -1 = unslected; 1, 2, 3, ... : select difficulty number 1, 2, 3, ...
    public List<int> selection;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField]
    GameObject TaskConfigPanel;

    private void Update() {
        // Toggle Task Config Panel
        if (currentTE_ID != -1)
            TaskConfigPanel.SetActive(true);
        else
            TaskConfigPanel.SetActive(false);
    }
}
