using System;
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
    public static bool playerDeath = false;
    public GameObject DeathPanel;
    private int currentScenebuildIndex;
    private SceneSetup sceneSetup;
    [SerializeField] private PoolingObject playerBullets, enemyBullets, turretBullets;
    public enum HubState { fromTutorial, fromYellow, fromRed }
    [SerializeField] public HubState HubStateGM = HubState.fromTutorial;
    private string thameKay;


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
        SwitchOnOffHowToPlayPanel();
        RespawnPlayer();
        if(playerDeath == false)
        {
            DeathPanel.SetActive(false);
        }
    }

    public HubState ReturnHubStateToSceneSetup()
    {
        return HubStateGM;
    }

    private void SwitchOnOffHowToPlayPanel()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            var script = canvas.GetComponent<CanvasPanels>();
            if(script.HowToPlayPanel.activeInHierarchy)
            {
                script.HowToPlayBackButton.SetActive(true);
                script.HowToPlayPanel.SetActive(false);
            }
            else
            {
                script.HowToPlayBackButton.SetActive(false);
                script.HowToPlayPanel.SetActive(true);
            }

        }
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
        turretBullets = (PoolingObject)args[7];
        sceneSetup = (SceneSetup)args[8];
        thameKay = (string)args[9];
        DeathPanel = (GameObject)args[10];
    }
    public void StartScene()
    {
        GameObject existCheck = GameObject.FindGameObjectWithTag("Player");
        if(existCheck != null)
        {
            Destroy(existCheck);
        }
        var spownedPlayer = Instantiate(playerObject, currentCheckPoint.transform.position, Quaternion.identity);
        spownedPlayer.name = "PlayerCharacter";
        spownedPlayer.transform.SetParent(null);
        player = spownedPlayer.GetComponent<PlayerController>();
        player.GetParamsFromGameManager(playerBullets);
        EventBroker.CallGiveAllEnemyesOnSceneBulletPoolReference(enemyBullets);
        EventBroker.CallGiveAllTurretsOnSceneBulletPoolRefenece(turretBullets);
        mainCamera.GetComponent<FollowObjectTransform>().SetObjectToFollow(spownedPlayer.transform);
    }
    public void OnPlayerDeath()
    {
        DeathPanel.SetActive(true);
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
            EventBroker.CallPlayThemeSfx(thameKay);
            DeathPanel.SetActive(false);
        }
    }
}
