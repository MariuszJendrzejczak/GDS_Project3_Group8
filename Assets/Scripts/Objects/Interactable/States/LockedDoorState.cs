using UnityEngine;

public class LockedDoorState : IState
{
    public void Enter(params object[] args)
    {
        SpriteRenderer open = (SpriteRenderer)args[0];
        SpriteRenderer close = (SpriteRenderer)args[1];
        BoxCollider2D collider = (BoxCollider2D)args[2];
        open.enabled = false;
        close.enabled = true;
        collider.enabled = true;
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
