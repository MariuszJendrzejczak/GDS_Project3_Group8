using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    protected StateMachine stateMachine = new StateMachine();
    public Dictionary<string, IState> stateDictionary;
    public List<InteractableObject> toSwitchList;
    protected string currentStateId;
    protected PlayerController player;
    protected SpriteRenderer renderer;
    [SerializeField] protected string tipText;
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Kolizja z: " + collision.name);
            player = collision.GetComponent<PlayerController>();
            EventBroker.InteractWithObject += Interact;
            EventBroker.CallUpdateTipText(tipText);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventBroker.InteractWithObject -= Interact;
            EventBroker.CallUpdateTipText("");
        }
    }
}
