using System.Diagnostics;

public class SwitchButton : InteractableObject, IInteractable
{
    public enum StartingStateKay { on, off }
    public StartingStateKay startingState;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.Add("on", new ButtonSwitchOnState());
        stateMachine.Add("off", new ButtonSwitchOffState());
        stateMachine.Change(startingState.ToString(), toSwitchList, renderer);
    }
    protected override void Start()
    {
        stateMachine.SetStartingState("off");
        base.Start();
    }
    public override void Interact()
    {
        base.Interact();
        if (currentStateId == "off")
        {
            stateMachine.Change("on", toSwitchList, renderer);
        }
        else if (currentStateId == "on")
        {
            stateMachine.Change("off", toSwitchList, renderer);
        }
        foreach(ISwitchable obj in toSwitchList)
        {
            obj.SwitchObject();
        }
        currentStateId = stateMachine.currentStateId;
    }
    private void Update()
    {
        stateMachine.Update();
        stateMachine.HandleInput();
    }
}
