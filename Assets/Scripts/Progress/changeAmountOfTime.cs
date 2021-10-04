using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class changeAmountOfTime : MonoBehaviour
{
    //private Button change_time;
    [SerializeField] private Text title;
    [SerializeField] private GameObject graph_holder;
    [SerializeField] private progressCore Prog;
    private int counter = 1;

    public void destroyChildren(GameObject parent)
    {
        int i = 0;

        GameObject[] allChildren = new GameObject[parent.transform.childCount];

        foreach (Transform child in parent.transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            if ((child.name != "StartDate") && (child.name != "EndDate") && (child.name != "tasks"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void getNewJSON(int mode)
    {
        //idk lmao
    }

    public void change()
    {
        counter++; // 1 = day, 2 = month, 3 = year
        
        destroyChildren(graph_holder);
        getNewJSON(counter);
        Prog.drawGraph();

        if (counter == 1)
        {
            title.text = "TASKS DONE PER DAY";
        }
        if (counter == 2)
        {
            title.text = "TASKS DONE PER MONTH";
        }
        if (counter == 3)
        {
            title.text = "TASKS DONE PER YEAR";
            counter = 0;
        }
    }
}
