using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightsOffState : IState
{
    public void Enter(params object[] args)
    {
        Debug.Log("Gasze światło");
        Light2D light = (Light2D)args[0];
        light.enabled = false;
    }

    public void Exit()
    { 
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
    }
}
