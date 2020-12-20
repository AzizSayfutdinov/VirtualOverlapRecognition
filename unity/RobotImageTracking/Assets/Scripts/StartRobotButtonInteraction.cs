using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.UI;

[System.Serializable]
public class GameObjEvent : UnityEvent<GameObject> { }

public class StartRobotButtonInteraction : Button
{

    public UnityEvent OnMyButtonClicked;

    void Start()
    {
        if(OnMyButtonClicked == null)
        {
            this.OnMyButtonClicked = new UnityEvent();
        }
        OnMyButtonClicked.AddListener(OutputDebug);
        
    }

    public void OutputDebug()
    {
        Debug.Log("Hello World");
    }

}
