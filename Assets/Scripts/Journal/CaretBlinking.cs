using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CaretBlinking : MonoBehaviour
{
    public Text TextObject;
    public GameObject Cursor;
    public float CursorDelay = 0.3f;
    private Vector3 CursorOffset = new Vector3(10, 10);

    private string _msg = "";
    private float _timer = 0f;

    private void Update()
    {
        //update msg
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (_msg.Length != 0)
                {
                    _msg = _msg.Substring(0, _msg.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                _msg += "\r\n";
            }
            else
            {
                _msg += c;
            }
        }

        TextObject.text = _msg;

        //place cursor
        var gen = TextObject.cachedTextGenerator;
        if (gen.verts.Count > 0)
        {
            var v = gen.verts[gen.verts.Count - 1].position;
            v = TextObject.transform.TransformPoint(v);
            Cursor.transform.position = v + CursorOffset;
        }

        //do blink
        _timer += Time.deltaTime;
        if (_timer > CursorDelay)
        {
            _timer = 0f;
            Cursor.SetActive(!Cursor.activeSelf);
        }
    }
}