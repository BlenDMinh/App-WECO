using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelectCore : MonoBehaviour {

    [SerializeField]
    private GameObject taskBoard, overallTasksBoard, taskElementPrefab;
    void Start() {
        for(int i = 0; i < 6; i++) {
            GameObject taskElementObj = UIHelper.PushAndGetPrefabToParent(taskElementPrefab, taskBoard.transform, 0);
            TaskElement taskElement = taskElementObj.GetComponent<TaskElement>();
            taskElement.SetTitle("HAHA");
            taskElement.UpdateTaskElement(taskElement);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
