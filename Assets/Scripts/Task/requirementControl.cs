using System;
using UnityEngine;
using UnityEngine.UI;

public class requirementControl : MonoBehaviour {
    public Slider slider;
    public InputField inputField;
    
    public void updateInputField() {
        inputField.text = slider.value.ToString();
    }

    public void updateSlider() {
        int value = Int32.Parse(inputField.text);
        if (value > slider.maxValue) {
            value = (int) slider.maxValue;
            inputField.text = value.ToString();
        }
        slider.value = value;
    }
}
