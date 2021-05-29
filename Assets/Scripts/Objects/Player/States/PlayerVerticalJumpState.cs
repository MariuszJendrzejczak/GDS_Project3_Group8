using UnityEngine;

public partial class PlayerController
{
    class PlayerVerticalJumpState : PlayerStateFields, IState
    {
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            player.Jump(player.jumpForceVertical);
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            verticalInputValue = Input.GetAxis("Vertical");
            if (player.IsGrounded() && Input.GetKeyDown(KeyCode.A) || player.IsGrounded() && Input.GetKeyDown(KeyCode.D))
            {
                player.stateMachine.Change("walk", player);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                player.Jump(player.jumpForceVertical);
            }
        }

        public void Update()
        {
        }
    }
}
