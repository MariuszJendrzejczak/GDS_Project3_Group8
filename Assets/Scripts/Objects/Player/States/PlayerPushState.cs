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
            //player.animator.SetTrigger("idle");
        }

        public void HandleInput()
        {
            horizontalInputValue = Input.GetAxis("Horizontal");
            PushAndPullAnimationHandle();
        }

        public void Update()
        {
            player.Movement(horizontalInputValue, player.pushSpeed);
        }
        private void PushAndPullAnimationHandle()
        {
            if (player.faceing == Facing.right)
            {
                if(horizontalInputValue > 0)
                {
                    player.animator.SetTrigger("push");
                }
                else if (horizontalInputValue < 0)
                {
                    player.animator.SetTrigger("pull");
                }
            }
            if (player.faceing == Facing.left)
            {
                if (horizontalInputValue > 0)
                {
                    player.animator.SetTrigger("pull");
                }
                else if (horizontalInputValue < 0)
                {
                    player.animator.SetTrigger("push");
                }
            }
        }
    }
}

