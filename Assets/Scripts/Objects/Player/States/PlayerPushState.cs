using UnityEngine;
public partial class PlayerController
{
    class PlayerPushState : PlayerStateFields, IState
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
        }

        public void Update()
        {
            player.Movement(horizontalInputValue, player.pushSpeed);
        }
    }
}

