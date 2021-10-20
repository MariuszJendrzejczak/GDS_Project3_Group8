using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWithStoryNote : InteractableObject, IInteractable
{
    [SerializeField] private StoryTextNote storyNote;
    private Animator animator;
    [SerializeField] private bool outro = false;
    private bool activated = false;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    public override void Interact()
    {
        if(outro)
        {
            EventBroker.InteractWithObject -= Interact;
            SceneManager.LoadScene(5);
        }
        else
        {
            animator.SetTrigger("activate");
            EventBroker.CallUpdateStoryText(storyNote.StoryIndex.ToString(), storyNote.StoryHeader, storyNote.StoryText);
            EventBroker.CallSwithcOnOffStoryPanel();
            if(activated == false)
            {
                EventBroker.CallObjectPlaySfx("note");
                activated = true;
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        EventBroker.CallUpdateStoryText("", "", "");
        EventBroker.CallSwitchOffStoryPanel();
    }

}
