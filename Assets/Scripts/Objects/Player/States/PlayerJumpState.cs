using UnityEngine;

public partial class PlayerController
{
    class PlayerJumpState : PlayerStateFields, IState
    {
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            horizontalInputValue = (float)args[1];
            //player.animator.SetTrigger("jump");
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            if (player.IsGrounded() && horizontalInputValue == 0)
            {
                player.stateMachine.Change("idle", player);
            }
            else if (player.IsGrounded() && horizontalInputValue != 0)
            {
                player.stateMachine.Change("walk", player);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Jump(player.jumpForce);
            }
        }

        public void Update()
        {
            player.Movement(horizontalInputValue);
        }
    }
}
