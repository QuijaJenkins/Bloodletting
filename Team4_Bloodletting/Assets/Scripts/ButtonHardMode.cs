using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHardMode : MonoBehaviour
{
    private Text hardMode;
    [SerializeField] private Slider slider;
    [SerializeField] private CanvasGroup cgroup;
    private float maxSliderAmount = 1;

    private void Start()
    {
        hardMode = GetComponentInChildren<Text>();

    }

    public void SliderChange()
    {
        float localValue = slider.value * maxSliderAmount;
        cgroup.alpha = 1-localValue;
        Debug.Log(localValue);
    }

    public void ButtonHard()
        {        
            if(GameHandler.hard == true)
            {
                hardMode.text = "Hard Mode: On";
            }
            else
            {
                hardMode.text = "Hard Mode: Off";
            }
        
        }

}
