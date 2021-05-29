using UnityEngine;

public class MovingElevatorState : IState
{
    private Elevator elevator;
    private Transform moveToTransform;
    private float moveSpeed;
    private StateMachine stateMachine;
    private bool startingPositionBool;
    public void Enter(params object[] args)
    {
        elevator = (Elevator)args[0];
        moveToTransform = (Transform)args[1];
        moveSpeed = (float)args[2];
        stateMachine = (StateMachine)args[3];
        elevator.startingPositionBool = false;
    }

    public void Exit()
    {
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
