
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroker : MonoBehaviour
{
    public static event Action PlayerDeath;
    public static void CallPlayerDeath()
    {
        if (PlayerDeath != null)
        {
            PlayerDeath();
        }
    }

    public static event Action InteractWithObject;
    public static void CallInteractWithObject()
    {
        if (InteractWithObject != null)
        {
            InteractWithObject();
        }
    }

    public static event Action RespawnToCheckPoint;
    public static void CallRespawntoCheckPoint()
    {
        if (RespawnToCheckPoint != null)
        {
            RespawnToCheckPoint();
        }
    }

    public static event Action<GameObject> CheckPointReached;
    public static void CallCheckPointReached(GameObject value)
    {
        if (CheckPointReached != null)
        {
            CheckPointReached(value);
        }
    }

    public static event Action<String> UpdateTipText;
    public static void CallUpdateTipText(String value)
    {
        if (UpdateTipText != null)
        {
            UpdateTipText(value);
        }
    }

    public static event Action<String, String, String> UpdateStoryText;
    public static void CallUpdateStoryText(String value, String value2, String value3)
    {
        if(UpdateStoryText != null)
        {
            UpdateStoryText(value, value2, value3);
        }
    }
    public static event Action SwitchOnOffStoryPanel;
    public static void CallSwithcOnOffStoryPanel()
    {
        if(SwitchOnOffStoryPanel != null)
        {
            SwitchOnOffStoryPanel();
        }
    }

    public static event Action SwitchOffStoryPanel;
    public static void CallSwitchOffStoryPanel()
    {
        if(SwitchOffStoryPanel != null)
        {
            SwitchOffStoryPanel();
        }
    }
}
