using UnityEngine;
using System.Collections.Generic;
public partial class PlayerController
{
    // not used state. Cuted by design decision.
    public class PlayerClimbinDownState : PlayerStateFields, IState
    {
        List<Transform> transformList;
        Transform target;
        Edge edge;
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            edge = player.interactableObject.GetComponent<Edge>();
            transformList = edge.transformList;
            player.SetGravityValue(0);
            target = transformList[0];
            edge.colliderToIngrre.enabled = false;
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
        }

        public void Update()
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, target.position, player.climbEdgeSpeed);
            if(player.transform.position == target.position)
            {
                player.stateMachine.Change("hang", player);
            }
        }
    }
}

