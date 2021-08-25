using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour, IRespawnAble
{
    [SerializeField] private Vector2 startPosition;
    private bool myDeath = false;

    void Start()
    {
        EventBroker.RespawnToCheckPoint += RespawnMe;
        EventBroker.CheckPointReached += CancelRespawn;

        startPosition = new Vector2(transform.position.x, transform.position.y);
    }

    public void CancelRespawn(GameObject value)
    {
        if (myDeath)
        {
            EventBroker.RespawnToCheckPoint -= RespawnMe;
        }
    }

    public void OnMyDeath()
    {
        myDeath = true;
    }

    public void RespawnMe()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Respawn " + this.name);
        var respawnBool = GetComponent<IRespawnBool>();
        if (respawnBool != null)
        {
            respawnBool.ChangeRespawnedBool();
        }
        var respawn = GetComponent<IDestroyAble>();
        if(respawn != null)
        {
            respawn.Respawn(startPosition);
        }
        this.gameObject.SetActive(true);
        myDeath = false;
    }
}
