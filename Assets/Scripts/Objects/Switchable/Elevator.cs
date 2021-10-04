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
            stateMachine.Change("move", this, moveToTransform, moveSpeed, stateMachine, player);
        }
        else
        {
            stateMachine.Change("move", this, startTransform, moveSpeed, stateMachine, player);
            startingPositionBool = true;
        }
    }

    public void GetPlayerControlerRef(PlayerController playercontroler)
    {
        player = playercontroler;
    }
    protected override void Awake()
    {
        base.Awake();
        EventBroker.GiveToAllPlayerCharacterRef += GetPlayerControlerRef;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            EventBroker.CallChangeElevatorBoolOnPlayer(true);
            Debug.Log("TRUE");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            EventBroker.CallChangeElevatorBoolOnPlayer(false);
        }
    }
}
