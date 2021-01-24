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
    
    private Sprite type; //the type of challenge ie: plastic bottles, plastic bags...
    private string Title;

    private Sprite GetType() {
    	return this.type;
    }

    private void SetType(Sprite newtype) {
    	this.type = newtype;
    }

    private void InitTaskElement() {
        //Display TaskElement on Unity
        //Examples: set 'reward' as Text to view on a Unity scene
        //...

    	Canvas taskbar = gameObject.GetComponent<Canvas>(); //main canvas
    	Text[] texts = taskbar.GetComponents<Text>(); //2 text: Reward 0, Title 1
    	Image[] images = taskbar.GetComponents<Image>(); //2 sprites: feesh 0, type 1

    	texts[0].text = reward.ToString(); //reward
    	texts[1].text = Title; //title
    	
    	images[1].sprite = type; //type of challange
    }
    
    
    public void TaskBar_Selected(){
    	this.selection = true;
    	//script that leads to difficulty scene
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