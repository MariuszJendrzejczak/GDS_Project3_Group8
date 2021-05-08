using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitchOffState : IState
{
    public void Enter(params object[] args)
    {
        List<InteractableObject> toSwitchList = (List<InteractableObject>)args[0];
        SpriteRenderer renderer = (SpriteRenderer)args[1];
        renderer.color = Color.red;
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
