using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableObject, ISwitchable
{
    [SerializeField] private Transform moveToTransform;
    [SerializeField] private Transform startTransform;
    private StateMachine stateMachine = new StateMachine();
    [HideInInspector] public bool startingPositionBool = true;
    [SerializeField][Range(1f,4f)] private float moveSpeed;
    public void SwitchObject()
    {
        if (startingPositionBool)
        {
            Debug.Log("go up");
            stateMachine.Change("move", this, moveToTransform, moveSpeed, stateMachine);
        }
        else
        {
            stateMachine.Change("move", this, startTransform, moveSpeed, stateMachine);
            Debug.Log("go down");
            startingPositionBool = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine.Add("empty", new EmptyState());
        stateMachine.Add("move", new MovingElevatorState());
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        stateMachine.HandleInput();
       // Debug.Log(transform.position);
    }
}
