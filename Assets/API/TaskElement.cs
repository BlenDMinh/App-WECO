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
    public int i = 0;

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

    	//Canvas taskbox = GetComponents<Canvas>()[1]; //main canvas
    	Text[] texts = GetComponents<Text>(); //3 text: text 0, difficulty 1, reward 2
    	Image[] images = GetComponents<Image>(); //2 sprites: feesh 0, type 1

    	texts[1].text = GetTaskDifficulties()[0];
    	texts[2].text = reward.ToString(); //reward
    	
    	images[1].sprite = type;
    }
    
    public void DisplayTask(){
    	//Display tasks according to difficulty
    }
    
    public void TaskBar_Selected(Canvas can){
    	//Change TaskBar appearance when it is selected 
    	selection = true;
    	Image img = can.GetComponent<Image>();
    	img.color = UnityEngine.Color.cyan;
    }
    
    public void Change_Difficulty(Text t){
    	//Change the difficulty of the selected challenge. Cycle through the diffs
    	i++;
    	t.text = GetTaskDifficulties()[i];
    	if (i == 2){
    		i = 0;
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