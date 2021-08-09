using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text tipText;
    private void Start()
    {
        EventBroker.UpdateTipText += UpdateTipText;
    }

    private void UpdateTipText(string value)
    {
        tipText.text = value;
    }
}
