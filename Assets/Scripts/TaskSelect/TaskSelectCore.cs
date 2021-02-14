using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelectCore : MonoBehaviour {

    [SerializeField]
    private GameObject taskBoard, overallTasksBoard, taskElementPrefab;

    void Start() {

        //Load JSON
        List<TaskElement> taskElements = new List<TaskElement>();
        string challengeName = null;
        if (Manager.Instance == null || Manager.Instance.currentChallenge == null)
            challengeName = "testSubject";
        else
            challengeName = Manager.Instance.currentChallenge;
        string jsonLoad = (Resources.Load(@"challenges\" + challengeName) as TextAsset).ToString();
        taskElements = JsonConvert.DeserializeObject<List<TaskElement>>(jsonLoad);

        //Load TaskElement to screen
        foreach (TaskElement te in taskElements) {
            GameObject taskElementObj = UIHelper.PushAndGetPrefabToParent(taskElementPrefab, taskBoard.transform, 0);
            TaskElement taskElement = taskElementObj.GetComponent<TaskElement>();
            taskElement.cloneTaskElement(te);
            taskElement.UpdateTaskElement(taskElement);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
