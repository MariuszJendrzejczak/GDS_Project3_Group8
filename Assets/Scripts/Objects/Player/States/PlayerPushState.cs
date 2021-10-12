using UnityEngine;
public partial class PlayerController
{
    class PlayerPushState : PlayerStateFields, IState
    {
        private bool effortsfxPlayed = false;
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            EventBroker.StopPlayingCharacterSfx += EffortPlayingSFXBoolToFalse;
        }

        public void Exit()
        {
            //player.animator.SetTrigger("idle");
            EventBroker.StopPlayingCharacterSfx -= EffortPlayingSFXBoolToFalse;
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
                    PlayEffortPlayingSFX();
                }
                else if (horizontalInputValue < 0)
                {
                    player.animator.SetTrigger("pull");
                    PlayEffortPlayingSFX();
                }
            }
            if (player.faceing == Facing.left)
            {
                if (horizontalInputValue > 0)
                {
                    player.animator.SetTrigger("pull");
                    PlayEffortPlayingSFX();
                }
                else if (horizontalInputValue < 0)
                {
                    player.animator.SetTrigger("push");
                    PlayEffortPlayingSFX();
                }
            }
        }

        private void PlayEffortPlayingSFX()
        {
            if (effortsfxPlayed == false)
            {
                EventBroker.CallCharacterPlaySfx("effort");
                effortsfxPlayed = true;
            }
        }
        private void EffortPlayingSFXBoolToFalse()
        {
            effortsfxPlayed = false;
        }
    }
}

