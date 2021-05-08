using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    protected StateMachine stateMachine = new StateMachine();
    public Dictionary<string, IState> stateDictionary;
    public List<InteractableObject> toSwitchList;
    protected string currentStateId;
    protected SpriteRenderer renderer;
    protected virtual void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
       currentStateId = stateMachine.currentStateId;
    }

    public void Update()
    {
        stateMachine.Update();
        stateMachine.HandleInput();
        //currentStateId = stateMachine.currentStateId;
    }

    public virtual void Interact()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<BasicPlayerController>().InteractWithObject += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<BasicPlayerController>().InteractWithObject -= Interact;
        }
    }
}
