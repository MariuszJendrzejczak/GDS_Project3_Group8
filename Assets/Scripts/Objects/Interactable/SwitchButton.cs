public class SwitchButton : InteractableObject, IInteractable
{
    public void Interact()
    {
        foreach(ISwitchable obj in toSwitchList)
        {
            obj.SwitchObject();
        }
    }

}
