using UnityEngine;

public class MovingElevatorState : IState
{
    private Elevator elevator;
    private Transform moveToTransform;
    private float moveSpeed;
    private StateMachine stateMachine;
    private bool startingPositionBool;
    private PlayerController player;
    public void Enter(params object[] args)
    {
        elevator = (Elevator)args[0];
        moveToTransform = (Transform)args[1];
        moveSpeed = (float)args[2];
        stateMachine = (StateMachine)args[3];
        elevator.startingPositionBool = false;
        player = (PlayerController)args[4];
        if (player != null)
        {
            player.GetElevatorAsParemt(elevator.gameObject);
            if (player.onElevator)
            {
                player.ChangeState("empty");
            }
        }
        EventBroker.CallObjectPlaySfxLayer2("elevator");
    }

    public void Exit()
    {
        if (player != null)
        {
            player.SetParrentNull();
            player.ChangeState("idle");
        }
        EventBroker.CallStopPlayingObjectSfxLayer2();
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, moveToTransform.position, moveSpeed * Time.deltaTime);
        if (elevator.transform.position == moveToTransform.position)
        {
            stateMachine.Change("empty");
        }
    }
}
