using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    private GameObject currentGameManager;
    [SerializeField] private GameObject canvas;
    private GameObject currentCanvas;
    [SerializeField] private bool playAble;
    [SerializeField] private bool mainMenu;
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private GameObject startingPoint;
    [SerializeField] private GameObject mainCamera;
    private GameObject currentMainCamera;
    private bool firstApperence;
    void Awake()
    {
        MakeMainCamera();
        MakeGameManager();
        MakeCanvas();
        StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        script.GetParmsFromSceneSetup(playerCharacter, startingPoint, currentMainCamera);

    }
    private void StartScene()
    {
        if (playAble)
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
        if (playAble)
        {
            script.MainMenuPanel.SetActive(false);
            script.TipsPanel.SetActive(true);
        }
        if (mainMenu)
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
}
