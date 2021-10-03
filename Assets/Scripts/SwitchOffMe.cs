using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffMe : InteractableObject, ISwitchable
{
    public void SwitchObject()
    {
        this.gameObject.SetActive(false);
    }
}
