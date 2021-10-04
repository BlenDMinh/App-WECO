using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelectCore : MonoBehaviour {

    [SerializeField]
    private GameObject taskBoard, overallTasksBoard, taskElementPrefab;

    void Start() {

        //tempo read
        string json = (Resources.Load("taskSelect") as TextAsset).ToString();
        List<TaskElement> teList =
        TaskSelectDataManager.Instance.taskElements = JsonConvert.DeserializeObject<List<TaskElement>>(json);

        int i = 0;
        foreach(TaskElement te in teList) {
            GameObject taskElementObj = UIHelper.PushAndGetPrefabToParent(taskElementPrefab, taskBoard.transform, 0); // offset is 0 because Canvas Group is doing everything for us OwO

            TaskElement taskElement = taskElementObj.GetComponent<TaskElement>();
            te.id = i++;
            taskElement.UpdateTaskElement_ALL(te);
            // Init selection (default -1 for this TE)
            TaskSelectDataManager.Instance.selection.Add(-1);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
