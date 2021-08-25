using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    class PlayerHangingEgdeState : PlayerStateFields, IState
    {
        List<Transform> transformList;
        Transform target;
        Edge edge;
        bool climbUp = false;
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            edge = player.interactableObject.GetComponent<Edge>();
            transformList = edge.transformList;
            player.transform.position = new Vector2(transformList[0].position.x, transformList[0].position.y);
            player.rigidbody.velocity = Vector2.zero;
            player.SetGravityValue(0);
            target = transformList[1];
            edge.colliderToIngrre.enabled = false;
            player.animator.SetTrigger("fall");
            
        }

        public void Exit()
        {
            player.SetGravityValue(1);
            climbUp = false;
            edge.colliderToIngrre.enabled = true;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                climbUp = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                player.ChangeState("idle");
                Debug.Log("tu jestem");
            }
        }

        public void Update()
        {
            if (climbUp)
            {
                ClimbUp();
            }
            player.animator.SetTrigger("climb");
        }
        private void ClimbUp()
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, target.position, player.climbEdgeSpeed);
            player.animator.SetTrigger("climb");
            int counter = 2;
            if (player.transform.position == target.position)
            {
                for (int i = 2; i > transformList.Count; i++)
                {
                    target = transformList[i];
                    counter++;
                }
            }
            if (player.transform.position == target.position && counter == transformList.Count)
            {
                player.stateMachine.Change("idle", player);
                if(player.interactableObject != null)
                {
                    edge.colliderToIngrre.enabled = true;
                }
            }
        }
    }
}
