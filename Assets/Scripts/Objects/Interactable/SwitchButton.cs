using System.Diagnostics;
using UnityEngine;

public class SwitchButton : InteractableObject, IInteractable
{
    [SerializeField] private Sprite onSprite, offSprite;
    public enum StartingStateKay { on, off }
    public StartingStateKay startingState;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.Add("on", new ButtonSwitchOnState());
        stateMachine.Add("off", new ButtonSwitchOffState());
        ChangeState(startingState.ToString());
    }
    protected override void Start()
    {
        stateMachine.SetStartingState(startingState.ToString());
        base.Start();
    }
    public override void Interact()
    {
        base.Interact();
        if (currentStateId == "off")
        {
            ChangeState("on");
        }
        else if (currentStateId == "on")
        {
            ChangeState("off");
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
    public void ChangeState(string kay)
    {
        stateMachine.Change(kay, toSwitchList, renderer, onSprite, offSprite);
    }
}
