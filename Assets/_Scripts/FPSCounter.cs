﻿using UnityEngine;
using UnityEngine.UI;

// Display FPS on a Unity UGUI Text Panel
public class FPSCounter : MonoBehaviour 
{
    public Text text;

    private const int targetFPS = 60;
    private const float updateInterval = 0.5f;

    private int framesCount; 
    private float framesTime; 

    void Start()
    { 
        // no text object set? see if our gameobject has one to use
        if (text == null) 
        { 
            text = GetComponent<Text>(); 
        }
    }
    
    void Update()
    {
        // monitoring frame counter and the total time
        framesCount++;
        framesTime += Time.unscaledDeltaTime; 

        // measuring interval ended, so calculate FPS and display on Text
        if (framesTime > updateInterval)
        {
            if (text != null)
            {
                float fps = framesCount/framesTime;
                text.text = System.String.Format("{0:F2} FPS", fps);
                text.color = (fps > (targetFPS-5) ? Color.green :
                                (fps > (targetFPS-30) ?  Color.yellow : 
                                Color.red));
            }
            // reset for the next interval to measure
            framesCount = 0;
            framesTime = 0;
        }
    }
}
