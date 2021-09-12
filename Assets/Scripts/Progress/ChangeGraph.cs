using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ChangeGraph : MonoBehaviour
{
	public Text Title;
	public int times = 0;
	public RectTransform chartContainer;
	public Vector3 ChangeVector = new Vector3(-1400, 0, 0);

	
    // Start is called before the first frame update
    void Start()
    {
    	chartContainer.localPosition = chartContainer.localPosition + ChangeVector;
    }

    public void Change()
    {
    	if (times%2 == 0) 
    	{
    		Title.text = "PAST 7 DAYS";
    		times = times + 1;
    		chartContainer.localPosition = chartContainer.localPosition - ChangeVector;
    	}
    	else
    	{
    		Title.text = "TASKS DONE PER DAY";
    		times = times + 1;
    		chartContainer.localPosition = chartContainer.localPosition + ChangeVector;
    	}
    	
    }
}
