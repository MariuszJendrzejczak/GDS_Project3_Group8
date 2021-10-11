using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private GameObject currentGameManager;
    [SerializeField] private GameObject canvas;
    private GameObject currentCanvas;
    private enum SceneType {mainMenu, hub, red, yellow, tutorial}
    [SerializeField] private SceneType scenetype;
    public enum HubState { fromTutorial, fromYellow, fromRed}
    [SerializeField] public HubState hubState;
    [SerializeField] private GameObject hubLevelFromTutorial;
    [SerializeField] private GameObject hubLevelFromYellow;
    [SerializeField] private GameObject hubLevelFromRed;
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject audioManager;
    [SerializeField] private GameObject startingPoint;
    [SerializeField] private GameObject mainCamera;
    private GameObject currentMainCamera;
    [SerializeField] private GameObject globalLight2D;
    [SerializeField] private GameObject currentGlobalLight2d;
    [SerializeField] private GameObject poolingObjects;
    [SerializeField] private PoolingObject playerBullets, enemyBullets, turretBullets;
    [SerializeField] private bool outro;
    private bool firstApperence;
    void Awake()
    {
        if (scenetype == SceneType.hub)
        {
            MakeHubLevel();
        }
        MakeMainCamera();
        MakeObjectPools();
        MakeGameManager();
        MakeCanvas();
        MakeGlobalLight();
        MakeAudioManager();
    }
    private void Start()
    {
        StartScene();
    }

    private void MakeHubLevel()
    {
        GameObject level;
        switch (hubState)
        {
            case HubState.fromTutorial:
                level = Instantiate(hubLevelFromTutorial);
                var obj = GameObject.Find("Start").transform.GetChild(0).gameObject;
                break;

            case HubState.fromYellow:
                level = Instantiate(hubLevelFromYellow);
                startingPoint = GameObject.Find("Start").transform.GetChild(0).gameObject;
                break;

            case HubState.fromRed:
                level = Instantiate(hubLevelFromRed);
                startingPoint = GameObject.Find("Start").transform.GetChild(0).gameObject;
                break;
        }
    }
    private void MakeGameManager()
    {
        currentGameManager = GameObject.Find("GameManagerContainer");
        if (currentGameManager == null)
        {
            currentGameManager = Instantiate(gameManager);
            currentGameManager.name = "GameManagerContainer";
        }
        var script = currentGameManager.GetComponent<GameManager>();
        script.GetParmsFromSceneSetup(playerCharacter, startingPoint, currentMainCamera, currentCanvas, currentGlobalLight2d, playerBullets, enemyBullets, turretBullets, this);

    }
    private void StartScene()
    {
        if (scenetype == SceneType.red || scenetype == SceneType.hub || scenetype == SceneType.tutorial || scenetype == SceneType.yellow)
        {
            currentGameManager.GetComponent<GameManager>().StartScene();
        }

    }
    private void MakeCanvas()
    {
        currentCanvas = GameObject.Find("Canvas");
        if (currentCanvas == null)
        {
            currentCanvas = Instantiate(canvas);
            currentCanvas.name = "Canvas";
        }
        var script = currentCanvas.GetComponent<CanvasPanels>();
        if (scenetype == SceneType.red || scenetype == SceneType.hub || scenetype == SceneType.tutorial || scenetype == SceneType.yellow)
        {
            script.MainMenuPanel.SetActive(false);
            script.TipsPanel.SetActive(true);
        }
        if (scenetype == SceneType.mainMenu)
        {
            script.MainMenuPanel.SetActive(true);
            script.TipsPanel.SetActive(false);
            if(outro)
            {
                script.OutroPanel.SetActive(true);
            }
        }
    }
    private void MakeMainCamera()
    {
        currentMainCamera = GameObject.Find("MainCamera");
        if (currentMainCamera == null)
        {
            currentMainCamera = Instantiate(mainCamera);
            currentMainCamera.name = "MainCamera";
        }
    }

    private void MakeGlobalLight()
    {
        currentGlobalLight2d = GameObject.Find("Global Light 2D");
        if(currentGlobalLight2d == null)
        {
            currentGlobalLight2d = Instantiate(globalLight2D);
            currentGlobalLight2d.name = "Global Light 2D";
        }
    }
    private void MakeObjectPools()
    {
        GameObject pool = Instantiate(poolingObjects);
        pool.name = "ObjectPools";
        playerBullets = pool.transform.GetChild(0).GetComponent<PoolingObject>();
        enemyBullets = pool.transform.GetChild(1).GetComponent<PoolingObject>();
        turretBullets = pool.transform.GetChild(2).GetComponent<PoolingObject>();
    }

    private void MakeAudioManager()
    {
        GameObject currentAudioManager = GameObject.Find("AudioManager");
        if(currentAudioManager == null)
        {
            GameObject audio = Instantiate(audioManager);
            audio.name = "AudioManager";
        }
        switch(scenetype)
        {
            case SceneType.hub:
            case SceneType.tutorial:
            case SceneType.red:
            case SceneType.yellow:
                EventBroker.PlayThameSfx(scenetype.ToString());
                break;
            case SceneType.mainMenu:
                EventBroker.PlayThameSfx("menu");
                break;
        }
    }
}
