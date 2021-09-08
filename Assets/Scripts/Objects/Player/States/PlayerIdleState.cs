using UnityEngine;

public partial class PlayerController
{
    class PlayerIdleState : PlayerStateFields, IState
    {
        bool goingDown;

        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            player.animator.SetTrigger("idle");
        }

        public void Exit()
        {
            player.animator.ResetTrigger("idle");
        }

        public void HandleInput()
        {
           horizontalInputValue = Input.GetAxis("Horizontal");
            if (horizontalInputValue != 0)
            {
                player.stateMachine.Change("walk", player);
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
            // ClimbingDown Feateure swiched off by design decision.
            /*if (Input.GetKeyDown(KeyCode.S))
            {
                if(player.interactableObject != null)
                {
                    Edge edge = player.interactableObject.GetComponent<Edge>();
                    if (edge.facingRequirement.ToString() == player.faceing.ToString())
                    {
                        player.stateMachine.Change("climbdown", player);
                    }
                }
            }*/
        }

        public void Update()
        {
            player.Movement(horizontalInputValue, player.speed);
            player.HorizontalTurn(horizontalInputValue);
            player.animator.SetTrigger("idle");
        }
    }
}
