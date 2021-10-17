using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private GameObject currentGameManager;
    [SerializeField] private GameObject canvas;
    private GameObject currentCanvas;
    private enum SceneType {mainMenu, hub, red, yellow, tutorial, outro}
    [SerializeField] private SceneType scenetype;
   // public enum HubState { fromTutorial, fromYellow, fromRed}
   // [SerializeField] public HubState hubState;
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
    [SerializeField] private float loadingPanelTimeOfExposition;
    private bool firstApperence;

    void Awake()
    {
        EventBroker.CallLoadingPanel(loadingPanelTimeOfExposition);
        if (scenetype == SceneType.hub)
        {
            MakeHubLevel();
        }
        MakeMainCamera();
        MakeObjectPools();
        MakeCanvas();
        MakeGlobalLight();
        MakeAudioManager();
        MakeGameManager();
    }
    private void Start()
    {
        StartScene();
    }

    private void MakeHubLevel()
    {
        currentGameManager = GameObject.Find("GameManagerContainer");
        if (currentGameManager != null)
        {
            GameManager script = currentGameManager.GetComponent<GameManager>();
            var hubState = script.ReturnHubStateToSceneSetup();
            GameObject level;
            switch (hubState)
            {
                case GameManager.HubState.fromTutorial:
                    level = Instantiate(hubLevelFromTutorial);
                    startingPoint = GameObject.Find("Start").transform.GetChild(0).gameObject;
                    break;

                case GameManager.HubState.fromYellow:
                    level = Instantiate(hubLevelFromYellow);
                    startingPoint = GameObject.Find("Start").transform.GetChild(0).gameObject;
                    break;

                case GameManager.HubState.fromRed:
                    level = Instantiate(hubLevelFromRed);
                    startingPoint = GameObject.Find("Start").transform.GetChild(0).gameObject;
                    break;
                default:
                    level = Instantiate(hubLevelFromTutorial);
                    startingPoint = GameObject.Find("Start").transform.GetChild(0).gameObject;
                    Debug.LogError("No GMHUBState!");
                    break;
            }
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
        script.GetParmsFromSceneSetup(playerCharacter, startingPoint, currentMainCamera, currentCanvas, currentGlobalLight2d, playerBullets, enemyBullets, turretBullets, this, scenetype.ToString());

        switch(scenetype)
        {
            case SceneType.red:
                script.HubStateGM = GameManager.HubState.fromRed;
                break;
            case SceneType.yellow:
                script.HubStateGM = GameManager.HubState.fromYellow;
                break;
        }

    }
    private void StartScene()
    {
        switch (scenetype)
        {
            case SceneType.red:
            case SceneType.hub:
            case SceneType.tutorial:
            case SceneType.yellow:
                currentGameManager.GetComponent<GameManager>().StartScene();
                break;
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
        switch (scenetype)
        {
            case SceneType.red:
            case SceneType.hub:
            case SceneType.tutorial:
            case SceneType.yellow:
                script.MainMenuPanel.SetActive(false);
                script.TipsPanel.SetActive(true);
                break;
            case SceneType.mainMenu:
                script.MainMenuPanel.SetActive(true);
                script.TipsPanel.SetActive(false);
                break;
            case SceneType.outro:
                script.MainMenuPanel.SetActive(true);
                script.TipsPanel.SetActive(false);
                script.OutroPanel.SetActive(true);
                break;
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
            case SceneType.outro:
                EventBroker.PlayThameSfx("outro");
                break;
        }
    }
}
