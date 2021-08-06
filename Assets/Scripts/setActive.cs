using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActive : MonoBehaviour {
    [SerializeField]
    private GameObject obj;
    public void ButtonSetActive(bool status) {
        obj.SetActive(status);
    }


}
