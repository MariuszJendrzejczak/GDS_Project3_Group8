using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : InteractableObject, ISwitchable
{
    enum StartingStateKay { on, off }
    [SerializeField]
    private StartingStateKay startingState;
    public override void Start()
    {
        stateMachine.Add("on", new LightsOnState());
        stateMachine.Add("off", new LightsOffState());
        stateMachine.SetStartingState(startingState.ToString());
        base.Start();
    }
    public void SwitchObject()
    {
        if (currentStateId == "on")
        {
            Debug.Log("Gasze �wiat�o");
            stateMachine.Change("off");
        }
        else if (currentStateId == "off")
        {
            Debug.Log("W��czam �wiat�o");
            stateMachine.Change("on");
        }
    }
}
