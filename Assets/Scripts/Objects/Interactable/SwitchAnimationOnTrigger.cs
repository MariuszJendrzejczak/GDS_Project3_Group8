using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimationOnTrigger : InteractableObject, ISwitchable
{
    [SerializeField] private string trigger;
    private Animator animator;
    public void SwitchObject()
    {
        animator.SetTrigger(trigger);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
