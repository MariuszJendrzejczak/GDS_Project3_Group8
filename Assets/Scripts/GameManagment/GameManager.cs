using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject currentCheckPoint;
    [SerializeField] private GameObject playerObject;
    private PlayerController player;
    private GameObject mainCamera;
    private GameObject canvas;
    private bool playerDeath = false;
    private int currentScenebuildIndex;

    private void Awake()
    {
    }

    void Start()
    {
        EventBroker.CheckPointReached += GetCurrentCheckPoint;
        EventBroker.PlayerDeath += OnPlayerDeath;
    }

    void Update()
    {
        if (playerDeath && Input.GetKeyDown(KeyCode.Return))
        {
            RespawnPlayer(currentCheckPoint.transform.position);
            EventBroker.CallRespawntoCheckPoint();
        }
    }

    public void GetParmsFromSceneSetup(params object[] args)
    {
        playerObject = (GameObject)args[0];
        player = playerObject.GetComponent<PlayerController>();
        currentCheckPoint = (GameObject)args[1];
        mainCamera = (GameObject)args[2];
        canvas = (GameObject)args[3];
    }
    public void StartScene()
    {
        var spownedPlayer = Instantiate(playerObject, currentCheckPoint.transform);
        spownedPlayer.name = "PlayerCharacter";
        spownedPlayer.transform.SetParent(null);
        mainCamera.GetComponent<FollowObjectTransform>().SetObjectToFollow(spownedPlayer.transform);
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
