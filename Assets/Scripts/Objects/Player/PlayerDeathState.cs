public partial class PlayerController
{
    class PlayerDeathState : PlayerStateFields, IState
    {
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            player.transform.tag = "Hide";
            player.animator.SetTrigger("death");
            EventBroker.CallPlayerDeath();
            player.animator.SetTrigger("death");
            //player.gameObject.SetActive(false);
            player.LaunchDeathCoroutine();
            EventBroker.CallCharacterPlaySfx("death");
            EventBroker.CallPlayThemeSfx("deaththame");
            
        }

        public void Exit()
        {
            player.transform.tag = "Player";
        }

        public void HandleInput()
        {
        }

        public void Update()
        {
        }
    }
}
 
