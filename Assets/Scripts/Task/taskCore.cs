using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taskCore : MonoBehaviour {

    public Text taskDesc;
    void Start() {
        Task task = Task.LoadCurrentTask();
        taskDesc.text = task.taskDescription;

    }
}
