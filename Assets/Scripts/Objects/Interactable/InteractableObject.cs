using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    protected StateMachine stateMachine = new StateMachine();
    public Dictionary<string, IState> stateDictionary;
    public List<InteractableObject> toSwitchList;
    protected string currentStateId;
    public void Awake()
    {
     
    }

    public virtual void Start()
    {
        currentStateId = stateMachine.currentStateId;
    }

    public void Update()
    {
        stateMachine.Update();
        stateMachine.HandleInput();
    }

    public virtual void Interact()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // miejsce na subskrybowanie eventu
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //miejsce na unsubskrybowanie eventu
    }
}
