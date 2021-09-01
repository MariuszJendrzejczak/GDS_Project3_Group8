using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithStoryNote : InteractableObject, IInteractable
{
    [SerializeField] StoryTextNote storyNote;

    public override void Interact()
    {
        EventBroker.CallUpdateStoryText(storyNote.StoryIndex.ToString(), storyNote.StoryHeader, storyNote.StoryText);
        EventBroker.CallSwithcOnOffStoryPanel();
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        EventBroker.CallUpdateStoryText("", "", "");
        EventBroker.CallSwitchOffStoryPanel();
    }

}
