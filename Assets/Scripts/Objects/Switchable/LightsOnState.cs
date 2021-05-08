using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightsOnState : IState
{
    public void Enter(params object[] args)
    {
        Light2D light = (Light2D)args[0];
        light.enabled = true;
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
