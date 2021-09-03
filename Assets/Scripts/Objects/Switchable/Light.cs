using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Light : InteractableObject, ISwitchable
{
    enum State { on, off }
    [SerializeField] private State startingState;
    [SerializeField] Light2D light;

    protected override void Awake()
    {
        stateMachine.Add("on", new LightsOnState());
        stateMachine.Add("off", new LightsOffState());
        stateMachine.Change(startingState.ToString(), light);
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }
    public void SwitchObject()
    {  
        if (currentStateId == "on")
        {
            stateMachine.Change("off", light);
        }
        else if (currentStateId == "off")
        {
            stateMachine.Change("on", light);
        }
        currentStateId = stateMachine.currentStateId;
    }
}
