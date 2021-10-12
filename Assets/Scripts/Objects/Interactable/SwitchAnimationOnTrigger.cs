using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimationOnTrigger : InteractableObject, ISwitchable
{
    [SerializeField] private string trigger;
    private Animator animator;
    private bool opened = false;
    public void SwitchObject()
    {
        animator.SetTrigger(trigger);
        if (opened == false)
        {
            EventBroker.CallObjectPlaySfxLayer2("door");
            opened = true;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
