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
        Debug.Log("switch light");
      
        if (currentStateId == "on")
        {
            Debug.Log("changeState to off");
            stateMachine.Change("off", light);
        }
        else if (currentStateId == "off")
        {
            Debug.Log("changeState to on");
            stateMachine.Change("on", light);
        }
        currentStateId = stateMachine.currentStateId;
    }
}
