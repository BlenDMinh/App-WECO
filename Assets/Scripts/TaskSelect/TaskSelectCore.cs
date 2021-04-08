using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSelectCore : MonoBehaviour {

    [SerializeField]
    private GameObject taskBoard, overallTasksBoard, taskElementPrefab;

    void Start() {
        List<TaskElement> taskElements = new List<TaskElement>();

        //tempo read
        string json = (Resources.Load("taskSelect") as TextAsset).text;      
        taskElements = JsonConvert.DeserializeObject<List<TaskElement>>(json);

        int i = 0;
        foreach(TaskElement te in taskElements) {
            Debug.Log(te);
            GameObject taskElementObj = UIHelper.PushAndGetPrefabToParent(taskElementPrefab, taskBoard.transform, 0); // offset is 0 because Canvas Group is doing everything for us OwO
            TaskElement taskElement = taskElementObj.GetComponent<TaskElement>();
            taskElement = te;
            taskElement.id = i;
            i++;
            taskElement.UpdateTaskElement_ALL(taskElement);

            // Init selection (default -1 for this TE)
            TaskSelectDataManager.Instance.selection.Add(-1);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
