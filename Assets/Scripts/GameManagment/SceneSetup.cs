using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private GameObject currentGameManager;
    [SerializeField] private GameObject canvas;
    private GameObject currentCanvas;
    private enum SceneType { playAble, mainMenu, hub}
    [SerializeField] private SceneType scenetype;
    private enum HubState { fromTutorial, fromYellow, fromRed}
    [SerializeField] private HubState hubState;
    [SerializeField] private GameObject hubLevelFromTutorial;
    [SerializeField] private GameObject hubLevelFromYellow;
    [SerializeField] private GameObject hubLevelFromRed;
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject startingPoint;
    [SerializeField] private GameObject mainCamera;
    private GameObject currentMainCamera;
    [SerializeField] private GameObject globalLight2D;
    [SerializeField] private GameObject currentGlobalLight2d;
    [SerializeField] private GameObject poolingObjects;
    [SerializeField] private PoolingObject playerBullets, enemyBullets;
    private bool firstApperence;
    void Awake()
    {
        if (scenetype == SceneType.hub)
        {
            //MakeHubLevel(); to uncomment when prefabs will be ready;
        }
        MakeMainCamera();
        MakeObjectPools();
        MakeGameManager();
        MakeCanvas();
        MakeGlobalLight();
    }
    private void Start()
    {
        StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void MakeHubLevel()
    {
        GameObject level;
        switch (hubState)
        {
            case HubState.fromTutorial:
                level = Instantiate(hubLevelFromTutorial);
                startingPoint = level.GetComponent<StartingCheckPointField>().StartingPoint;
                break;

            case HubState.fromYellow:
                level = Instantiate(hubLevelFromYellow);
                startingPoint = level.GetComponent<StartingCheckPointField>().StartingPoint;
                break;

            case HubState.fromRed:
                level = Instantiate(hubLevelFromRed);
                startingPoint = level.GetComponent<StartingCheckPointField>().StartingPoint;
                break;
        }
    }
    private void MakeGameManager()
    {
        currentGameManager = GameObject.Find("GameManagerConteiner");
        if (currentGameManager == null)
        {
            currentGameManager = Instantiate(gameManager);
            currentGameManager.name = "GameManagerContainer";
        }
        var script = currentGameManager.GetComponent<GameManager>();
        script.GetParmsFromSceneSetup(playerCharacter, startingPoint, currentMainCamera, currentCanvas, currentGlobalLight2d, playerBullets, enemyBullets);

    }
    private void StartScene()
    {
        if (scenetype == SceneType.playAble || scenetype == SceneType.hub)
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
        if (scenetype == SceneType.playAble || scenetype == SceneType.hub)
        {
            script.MainMenuPanel.SetActive(false);
            script.TipsPanel.SetActive(true);
        }
        if (scenetype == SceneType.mainMenu)
        {
            script.MainMenuPanel.SetActive(true);
            script.TipsPanel.SetActive(false);
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

    }
}
