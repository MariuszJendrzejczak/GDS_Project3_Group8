using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : SwitchButton, IInteractable
{
    private bool stepedByPlayer = false;
    private bool stepedByBox = false;
    private enum StepedState { steped, released}
    private StepedState stepedState = StepedState.released;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.tag == "Box")
        {
            EventBroker.InteractWithObject += Interact;
            EventBroker.CallUpdateTipText(tipText);
            stepedByBox = true;
        }
        if(collision.tag == "Player")
        {
            stepedByPlayer = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.tag == "Box")
        {
            EventBroker.InteractWithObject -= Interact;
            EventBroker.CallUpdateTipText("");
            stepedByBox = false;
        }
        if (collision.tag == "Player")
        {
            stepedByPlayer = false;
        }
    }
    private void Update()
    {
        CheckStepedState();
    }
    private void CheckStepedState()
    {
        switch (stepedState)
        {
            case StepedState.released:
                if (stepedByBox == true || stepedByPlayer == true)
                {
                    Interact();
                    stepedState = StepedState.steped;
                }
                break;
            case StepedState.steped:
                if (stepedByBox == false && stepedByPlayer == false)
                {
                    Interact();
                    stepedState = StepedState.released;
                }
                break;
        }
    }

}
