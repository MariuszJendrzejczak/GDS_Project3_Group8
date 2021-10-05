using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject currentCheckPoint;
    private GameObject playerObject;
    private PlayerController player;
    private GameObject mainCamera;
    private GameObject canvas;
    private GameObject globalLight2D;
    private bool playerDeath = false;
    private int currentScenebuildIndex;
    [SerializeField] private PoolingObject playerBullets, enemyBullets;

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
        RespawnPlayer();
    }

    public void GetParmsFromSceneSetup(params object[] args)
    {
        playerObject = (GameObject)args[0];
        currentCheckPoint = (GameObject)args[1];
        mainCamera = (GameObject)args[2];
        canvas = (GameObject)args[3];
        globalLight2D = (GameObject)args[4];
        playerBullets = (PoolingObject)args[5];
        enemyBullets = (PoolingObject)args[6];
    }
    public void StartScene()
    {
        var spownedPlayer = Instantiate(playerObject, currentCheckPoint.transform.position, Quaternion.identity);
        spownedPlayer.name = "PlayerCharacter";
        spownedPlayer.transform.SetParent(null);
        player = spownedPlayer.GetComponent<PlayerController>();
        player.GetParamsFromGameManager(playerBullets);
        EventBroker.CallGiveAllEnemyesOnSceneBulletPoolReference(enemyBullets);
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
    private void RespawnPlayer()
    {
        if (playerDeath && Input.GetKeyDown(KeyCode.Return))
        {
            player.Respawn(currentCheckPoint.transform.position);
            playerDeath = false;
            EventBroker.CallRespawntoCheckPoint();
        }
    }
}
