﻿public partial class PlayerController
{
    class PlayerDeathState : PlayerStateFields, IState
    {
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            player.animator.SetTrigger("death");
            EventBroker.CallPlayerDeath();
            player.animator.SetTrigger("death");
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
}
 