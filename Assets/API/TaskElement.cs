using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskElement : MonoBehaviour {
    //===============BlenDMinh===========DO NOT TOUCH===========//

    //Index of this TaskElement
    public int id;

    //selection = true if this TaskElement is selected
    private bool selection = false;

    public string story, title; //current title

    //List of task difficulties user can choose (currently string)
    public List<string> taskDifficulties;
    //Number of fishImage receive;
    public List<int> reward;

    //TaskElement() { }

    /*TaskElement(int index, string title, string story, List<string> taskDifficulties, List<int> reward) {
        id = index;
        this.title = title;
        this.story = story;
        this.taskDifficulties = taskDifficulties;
        this.reward = reward;
    }*/

    public void cloneTaskElement(TaskElement taskElement) {
        id = taskElement.id;
        title = taskElement.title;
        taskDifficulties = taskElement.taskDifficulties;
        reward = taskElement.reward;
        story = taskElement.story;
    }

    /*TaskElement(TaskElement taskElement) {
        cloneTaskElement(taskElement);
    }*/

    //===============Monk======================================//

    
    public int currentDifficulty = 0; //current task difficulty
    private Color[] diffColor = new Color[3]{
        Color.cyan, Color.yellow, Color.red
    };

    [SerializeField]
    private Image taskBarImage, fishImage;

    [SerializeField]
    Text currentReward, Title_t;

    private void InitTaskElement() {
        //Display TaskElement on Unity
        //Examples: set 'reward' as Text to view on a Unity scene

        if(title != null)
            Title_t.text = title;
        currentReward.text = "0"; //default is unselected so there will be no fish award
        fishImage.color = diffColor[currentDifficulty];

        if (!selection) {
            fishImage.enabled = true;
        }
    }


    public void taskBarImage_Selected() {
        Image fishImage = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        this.selection = !this.selection;
        if (this.selection) {
            //script that leads to difficulty scene here
            fishImage.enabled = true;
        }
        if (!this.selection) {
            fishImage.enabled = false;
        }
    }

    //===============Monk=====================================//


    private void Start() {
        InitTaskElement();

    }

    //Call this when you want to sync all Unity Element with current TaskElement (if changed)
    public void UpdateTaskElement(TaskElement taskElement) {
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