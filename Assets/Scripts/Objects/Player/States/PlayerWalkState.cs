using UnityEngine;

public partial class PlayerController
{
    class PlayerWalkState : PlayerStateFields, IState
    {
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            horizontalInputValue = Input.GetAxis("Horizontal");
            if(horizontalInputValue == 0)
            {
                player.stateMachine.Change("idle", player);
            }
            if (player.IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                float directionVelocity = horizontalInputValue;
                player.stateMachine.Change("jump", player, directionVelocity);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Jump(player.jumpForce);
            }
        }

        public void Update()
        {
            if (player != null)
            {
                player.animator.SetTrigger("run");
                player.Movement(horizontalInputValue);
                player.HorizontalTurn(horizontalInputValue);

            }
            else Debug.Log("player is null");
           
        }
    }
}
