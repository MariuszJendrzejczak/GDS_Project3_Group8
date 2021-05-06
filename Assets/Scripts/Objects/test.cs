using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject obj;

    private void Start()
    {
        Door door = obj.GetComponent<Door>();
        door.Interact();
    }
}
