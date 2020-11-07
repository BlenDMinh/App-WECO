using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SelectText : MonoBehaviour, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public TMP_Text inputfield;
    private RectTransform rectTransform;
    private Canvas canvas;

    private bool isHoveringObject;
    private int lastIndex = -1;

    List<string> strList = new List<string>();
    string inputText = "Hello you guys";
    private string FormatText(string normalWord, string positions)
    {
        inputText = null;
        strList.Clear();
        string positionString = positions.ToString();
        for (int i = 0; i < normalWord.Length; i++)
        {
            if (int.Parse(positions[i].ToString()) == 1)
            {
                strList.Add(normalWord[i] + "");
            }
            else
            {
                strList.Add("<color=yellow>" + normalWord[i] + "</color>");
            }
        }
        for (int i = 0; i < strList.Count; i++)
        {
            inputText += strList[i];
        }
        return inputText;
    }

    private void Awake()
    {

    }
    private void LateUpdate()
    {
        //if (isHoveringObject == true)
        //{
        //    Check if Mouse / Touch intersects any of characters. If so, assign color
        //    int charIndex = TMP_TextUtilities.FindIntersectingCharacter(inputfield, Input.mousePosition, null, true);
        //    if (charIndex != -1 && charIndex != lastIndex)
        //    {
        //        lastIndex = charIndex;
        //        Color32 color = new Color32((byte)50, (byte)69, (byte)217, 255);
        //        int vertexIndex = inputfield.textInfo.characterInfo[charIndex].vertexIndex;



        //        uiVertices[vertexIndex + 0].color = color;
        //        uiVertices[vertexIndex + 1].color = color;
        //        uiVertices[vertexIndex + 2].color = color;
        //        uiVertices[vertexIndex + 3].color = color;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 touchPosition = Input.GetTouch(0).position;
        int lastIndex = inputfield.text.Length - 1;
        int charIndex = TMP_TextUtilities.FindIntersectingCharacter(inputfield, touchPosition, Camera.main, true);
        //int meshIndex = inputfield.textInfo.characterInfo[charIndex].materialReferenceIndex;
        int vertexIndex = inputfield.textInfo.characterInfo[charIndex].vertexIndex;

        if (charIndex != -1 && charIndex != lastIndex)
        {
            lastIndex = charIndex;
            Color32 color = new Color32((byte)50, (byte)69, (byte)217, 255);

            
            //vertexColors[vertexIndex + 0] = color;
            //vertexColors[vertexIndex + 1] = color;
            //vertexColors[vertexIndex + 2] = color;
            //vertexColors[vertexIndex + 3] = color;

            //Color32[] vertexColors = inputfield.textInfo.meshInfo.;

        }
    }
    public void OnPointerClick(PointerEventData click)
    {
        // Get index of character.
        int charIndex = TMP_TextUtilities.FindIntersectingCharacter(inputfield, click.position, Camera.current, true);
        if (charIndex != -1 && charIndex != lastIndex)
        {
            lastIndex = charIndex;
            // Replace text with color value for character.
            inputText = inputText.Replace(inputText[charIndex].ToString(), "<color=yellow>" + inputText[charIndex].ToString() + "</color>");
            inputfield.GetComponent<TMP_Text>().text = inputText;
        }
    }
}
