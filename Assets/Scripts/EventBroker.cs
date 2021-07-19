
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
}
