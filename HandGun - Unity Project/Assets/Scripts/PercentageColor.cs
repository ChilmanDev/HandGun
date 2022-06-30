using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PercentageColor : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] Color[] color = new Color[4];
    [SerializeField] int[] rangeMax = new int[4];

    void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        int value = (int)Single.Parse(text.text);
        Debug.Log(value);

        if(value <= rangeMax[0])
            text.color = color[0];

        else if(value <= rangeMax[1])
            text.color = color[1];

        else if(value <= rangeMax[2])
            text.color = color[2];

        else// if(value <= rangeMax[3])
            text.color = color[3];

        text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
    }
}

