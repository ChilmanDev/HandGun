using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Win : MonoBehaviour
{
    public Slider slider;
    public Text text;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value >= slider.maxValue){
            text.enabled = true;
        }
    }
}
