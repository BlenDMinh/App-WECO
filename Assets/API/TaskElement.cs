using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskElement : MonoBehaviour {
    //===============BlenDMinh===========DO NOT TOUCH===========//

    public int id; // Task Element id

    //selection = true if this TaskElement is selected
    public bool selection = false;
    
    //List of task difficulties user can choose (currently string)
    public List<string> taskDifficulties;

    public string story;

    public string title; //current title

    //Number of fishImage receive;
    public List<int> reward;

    TaskElement() { }
    TaskElement(List<string> taskDifficulties, List<int> reward) {
        this.taskDifficulties = taskDifficulties;
        this.reward = reward;
    }

    TaskElement(List<string> taskDifficulties, string story, List<int> reward) {
        this.taskDifficulties = taskDifficulties;
        this.story = story;
        this.reward = reward;
    }

    private void cloneTaskElement(TaskElement taskElement) {
        taskDifficulties = taskElement.taskDifficulties;
        reward = taskElement.reward;
        story = taskElement.story;
    }

    TaskElement(TaskElement taskElement) {
        cloneTaskElement(taskElement);
    }

    //===============Monk======================================//

    
    private int currentDifficulty = 0; //current task difficulty
    private Color[] diffColor = new Color[3]{
        Color.cyan, Color.yellow, Color.red
    };

    private void SetCurrentDifficulty(int newdif) {
        currentDifficulty = newdif;
    }
    private int GetCurrentDifficulty() {
        return currentDifficulty;
    }

    [SerializeField]
    private Image taskBarImage, fishImage;

    [SerializeField]
    Text currentReward, Title_t;

    private void InitTaskElement() {
        //Display TaskElement on Unity
        //Examples: set 'reward' as Text to view on a Unity scene

        Title_t.text = this.title;
        currentReward.text = this.reward.ToString();
        //fishImage.color = diffColor[currentDifficulty];

        if (!this.selection) {
            fishImage.enabled = true;
        }
    }

    //On this Task Element select
    //script that leads to difficulty scene here
    public void onSelected() {
        Image fishImage = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        this.selection = !this.selection;
        if (this.selection) {
            fishImage.enabled = true;
        }
        if (!this.selection) {
            fishImage.enabled = false;
        }

        // BlenD part
        TaskSelectDataManager.Instance.currentTE_ID = id;

        // TO-DO: OPEN Task Config Panel

    }

    //===============Monk=====================================//


    private void Start() {
        InitTaskElement();

    }

    //Call this when you want to sync all Unity Element with current TaskElement (if changed)
    public void UpdateTaskElement_ALL(TaskElement taskElement) {
        cloneTaskElement(taskElement);
        InitTaskElement();
    }

    public void UpdateTaskElement_Appearance() {
        InitTaskElement();
    }
}