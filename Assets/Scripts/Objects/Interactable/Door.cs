using UnityEngine;

public class Door : InteractableObject, ISwitchable
{
    enum StartingStateKay {locked, close, open}
    [SerializeField]
    private StartingStateKay startingState;
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
            Debug.Log("Otwieram drzwi");
            stateMachine.Change("open");
        }
        else if (currentStateId == "open")
        {
            Debug.Log("Zamykam Drzwi");
            stateMachine.Change("close");
        }
    }

    public void SwitchObject()
    {
        stateMachine.Change("close");
    }
}
