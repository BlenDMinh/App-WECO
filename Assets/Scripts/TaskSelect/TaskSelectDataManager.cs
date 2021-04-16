using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSelectDataManager : MonoBehaviour {
    public static TaskSelectDataManager Instance { get; private set; }

    public List<TaskElement> taskElements;
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
    GameObject TaskConfigPanel, diffPrefab, diffList;
    [SerializeField]
    Text storyText;

    private void Update() {
        // Toggle Task Config Panel
        if (currentTE_ID != -1) {
            if (TaskConfigPanel.activeSelf)
                return;
            FetchConfigBoardWithTaskElement(currentTE_ID);
            TaskConfigPanel.SetActive(true);
        }
        else {
            if (!TaskConfigPanel.activeSelf)
                return;
            TaskConfigPanel.SetActive(false);
            foreach (Transform child in diffList.transform)
                Destroy(child.gameObject);
        }
    }

    private void FetchConfigBoardWithTaskElement(int currentTE_ID) {
        TaskElement taskElement = taskElements[currentTE_ID];
        storyText.text = taskElement.story;

        for (int id = 0; id < taskElement.taskDifficulties.Count; id++) {
            GameObject diffSelection = Instantiate(diffPrefab);
            string dif = taskElement.taskDifficulties[id];
            int reward = taskElement.reward[id];
            diffSelection.transform.GetChild(0).GetComponent<Text>().text = dif;
            diffSelection.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = reward.ToString();
            GameObject dS = UIHelper.PushAndGetPrefabToParent(diffSelection, diffList.transform, 30);
            dS.GetComponent<RectTransform>().sizeDelta = new Vector2(dS.GetComponent<RectTransform>().sizeDelta.x, dS.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
            Debug.Log(dS.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y);
        }
    }
}
