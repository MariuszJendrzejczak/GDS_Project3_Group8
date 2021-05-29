using UnityEngine;

public class Door : InteractableObject, ISwitchable
{
    enum StartingStateKay {locked, close, open}
    [SerializeField]
    private StartingStateKay startingState;
    [SerializeField] SpriteRenderer openDoorRenderer, closeDoorRenderer;
    [SerializeField] BoxCollider2D closedDoorCollider;
    protected override void Start()
    {
        stateMachine.Add("locked", new LockedDoorState());
        stateMachine.Add("close", new ClosedDoorState());
        stateMachine.Add("open", new OpenDoorState());
        stateMachine.SetStartingState(startingState.ToString());
        base.Start();
    }
    public override void Interact()
    {
        if (currentStateId == "locked")
        {
            Debug.Log("Drzwi Zamknięte");
        }
        else if (currentStateId == "close")
        {
            stateMachine.Change("open", openDoorRenderer, closeDoorRenderer, closedDoorCollider);
        }
        else if (currentStateId == "open")
        {
            stateMachine.Change("close", openDoorRenderer, closeDoorRenderer, closedDoorCollider);
        }
        currentStateId = stateMachine.currentStateId;
    }

    public void SwitchObject()
    {
        stateMachine.Change("close");
    }
}
