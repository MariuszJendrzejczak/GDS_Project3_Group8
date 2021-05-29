using UnityEngine;

public partial class PlayerController
{
    class PlayerClimbingState : PlayerStateFields, IState
    {
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            GameObject ledder = (GameObject)args[1];
            
            player.SetGravityValue(0);
            player.transform.position = new Vector2(ledder.transform.position.x, player.transform.position.y);
        }

        public void Exit()
        {
            player.GetInteractableObject(null);
            player.SetGravityValue(1);
        }

        public void HandleInput()
        {
            verticalInputValue = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.stateMachine.Change("idle", player);
            }
        }

        public void Update()
        {
            player.ClimbLedder(verticalInputValue);
        }
    }
}
