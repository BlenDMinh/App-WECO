
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
        id = taskElement.id;
        title = taskElement.title;
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

        Title_t.text = title;
        currentReward.text = "0";
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
        /*if (this.selection) {
            fishImage.gameObject.SetActive(true);
        }
        if (!this.selection) {
            fishImage.gameObject.SetActive(false);
        }*/

        // BlenD part
        TaskSelectDataManager.Instance.currentTE_ID = id;
        Debug.Log("Select Task Element " + id + "!");
        // TO-DO: OPEN Task Config Panel in TaskSelectDataManager: DONE

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