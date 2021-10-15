using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI text;
    private DateTime deadline;
    private DateTime delta;
    
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        var delta = deadline - DateTime.Now;
        var seconds = delta.Minutes * 60 + delta.Seconds;
        if (0 < seconds && seconds < 300)
        {
            var timeAsString = seconds.ToString();
            if (text.text != timeAsString)
            {
                text.text = timeAsString;
            }
        }
        else if (text.text != " ")
            text.text = " ";
    }

    public void SetTimer(DateTime timestamp)
    {
        deadline = timestamp;
    }
}