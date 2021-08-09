using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    private Scene activeScene;
    private void Start()
    {
        activeScene = SceneManager.GetActiveScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(activeScene.buildIndex + 1);
    }

    public void LoadPreviewScene()
    {
        if (activeScene.buildIndex > 0)
        {
            SceneManager.LoadScene(activeScene.buildIndex - 1);
        }
    }

    public void LoadSceneNumberScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            LoadPreviewScene();
        }
    }
}
