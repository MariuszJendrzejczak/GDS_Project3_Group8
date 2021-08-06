using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject currentCheckPoint;
    [SerializeField] private GameObject playerObject;
    private PlayerController player;
    private bool playerDeath = false;

    private void Awake()
    {
        // defensive programing for protection from always forgeting to attach obj to script designers!
        if (playerObject == null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogError("Without a player character placed and attached on the scene, we can't play the scene my dear designer!");
            }
        }
    }

    void Start()
    {
        EventBroker.CheckPointReached += GetCurrentCheckPoint;
        EventBroker.PlayerDeath += OnPlayerDeath;
        player = playerObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerDeath && Input.GetKeyDown(KeyCode.Return))
        {
            RespawnPlayer(currentCheckPoint.transform.position);
            EventBroker.CallRespawntoCheckPoint();
        }
    }
    public void OnPlayerDeath()
    {
        playerDeath = true;
    }

    public void GetCurrentCheckPoint(GameObject value)
    {
        currentCheckPoint = value;
    }

    public void RespawnPlayer(Vector2 value)
    {
        player.Respawn(value);
        playerDeath = false;
    }

    
}
