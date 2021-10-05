using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    public class PlayerFallState : PlayerStateFields, IState
    {
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            player.animator.SetTrigger("fall");
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
        }

        public void Update()
        {
            if(player.IsGrounded())
            {
                player.ChangeState("idle");
            }
        }
    }
}
