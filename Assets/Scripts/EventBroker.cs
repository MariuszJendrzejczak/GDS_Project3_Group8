
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
}
