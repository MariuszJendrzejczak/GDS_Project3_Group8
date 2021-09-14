using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class PlayerController
{
    class PlayerHangingEgdeState : PlayerStateFields, IState
    {
        List<Transform> transformList;
        Transform target;
        Edge edge;
        private bool climbedUp = false;
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
            climbedUp = false;
            
        }

        public void Exit()
        {
            player.SetGravityValue(1);
            edge.colliderToIngrre.enabled = true;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (climbedUp == false)
                {
                    ClimbUp();
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                player.ChangeState("idle");
                Debug.Log("tu jestem");
            }
        }

        public void Update()
        {
        }
        private void ClimbUp()
        {
            climbedUp = true;
            player.animator.SetTrigger("climb");
            player.StartCoroutine(ClimbingUPRutine());
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
                if(player.interactableObject != null)
                {
                    edge.colliderToIngrre.enabled = true;
                }
            }
        }
        private IEnumerator ClimbingUPRutine()
        {
            yield return new WaitForSeconds(2f);
            player.transform.position = target.position;
            player.stateMachine.Change("idle", player);
        }
    }
}
