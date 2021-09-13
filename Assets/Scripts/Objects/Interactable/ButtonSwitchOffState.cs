using System.Collections.Generic;
using UnityEngine;

public class ButtonSwitchOffState : IState
{
    private Sprite onSprite, offSprite;
    public void Enter(params object[] args)
    {
        List<InteractableObject> toSwitchList = (List<InteractableObject>)args[0];
        SpriteRenderer renderer = (SpriteRenderer)args[1];
        onSprite = (Sprite)args[2];
        offSprite = (Sprite)args[3];
        if(renderer != null)
        {
            renderer.sprite = offSprite;
        }
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
