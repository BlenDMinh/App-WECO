using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskElement : MonoBehaviour {
    //===============BlenDMinh===========DO NOT TOUCH===========//

    //selection = true if this TaskElement is selected
    private bool selection = false;

    //List of task difficulties user can choose (currently string)
    private List<string> taskDifficulties;

    //Number of fish receive;
    private int reward;


    //Set current status: Selected / Deselected
    public void SetSelection(bool status) {
        selection = status;
    }
    public bool isSelected() {
        return selection;
    }

    //Set the task difficuties for this TaskElement
    public void SetTaskDifficulties(List<string> gimmeAList) {
        taskDifficulties = gimmeAList;
    }

    //Get...........
    public List<String> GetTaskDifficulties() {
        return taskDifficulties;
    }

    //Set and get...what u know already
    public void SetReward(int reward) {
        this.reward = reward;
    }
    public int GetReward() {
        return reward;
    }

    TaskElement() { }
    TaskElement(List<String> taskDifficulties, int reward) {
        this.taskDifficulties = taskDifficulties;
        this.reward = reward;
    }

    private void cloneTaskElement(TaskElement taskElement) {
        taskDifficulties = taskElement.taskDifficulties;
        reward = taskElement.reward;
    }

    TaskElement(TaskElement taskElement) {
        cloneTaskElement(taskElement);
    }

    //===============Monk======================================//
    
    private string Title_s; //current title
    private int currentDifficulty = 0; //current task difficulty
    private Color[] diffColor = new Color[3]{
        Color.cyan, Color.yellow, Color.red
    };

    private void setcurrentDifficulty(int newdif) {
    	this.currentDifficulty = newdif;
    }
    private int getcurrentDifficulty(){
    	return this.currentDifficulty;
    }


    private void InitTaskElement() {
        //Display TaskElement on Unity
        //Examples: set 'reward' as Text to view on a Unity scene

    	Image taskbar = this.gameObject.GetComponent<Image>();
    	Image fish = this.gameObject.transform.GetChild(0).GetComponent<Image>();
    	Text currentReward = fish.transform.GetChild(0).GetComponent<Text>();
    	Text Title_t = this.gameObject.transform.GetChild(1).GetComponent<Text>();

    	Title_t.text = this.Title_s;
    	currentReward.text = this.reward.ToString();
    	fish.color = diffColor[currentDifficulty];

    	if (!this.selection){
    		fish.enabled = true;
    	}
    }
    
    
    public void TaskBar_Selected(){
    	Image fish = this.gameObject.transform.GetChild(0).GetComponent<Image>();
    	this.selection = !this.selection;
    	if (this.selection) {
    		//script that leads to difficulty scene here
    		fish.enabled = true;
    	} 
    	if (!this.selection){
    		fish.enabled = false;
    	}
    }
    
    //===============Monk=====================================//


    private void Start() {
        InitTaskElement();

    }

    //Call this when you want to sync all Unity Element with current TaskElement (if changed)
    private void UpdateTaskElement(TaskElement taskElement) {
        cloneTaskElement(taskElement);
        InitTaskElement();
    }

    private bool lastSelection;
    private bool onSelectionChange(bool selection) {
        if (selection != lastSelection) {
            lastSelection = selection;
            return true;
        }
        return false;
    }
}