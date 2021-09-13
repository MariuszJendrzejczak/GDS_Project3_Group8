using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : InteractableObject
{
    private enum SceneToLoad { tutorial, hub, yellow, red}
    [SerializeField] private SceneToLoad sceneToLoad;
    private int buildIndex;
    public override void Interact()
    {
        switch(sceneToLoad)
        {
            case SceneToLoad.tutorial:
                buildIndex = 1;
                break;

            case SceneToLoad.hub:
                buildIndex = 2;
                break;

            case SceneToLoad.yellow:
                buildIndex = 3;
                break;

            case SceneToLoad.red:
                buildIndex = 4;
                break;
        }
        Debug.Log("BuildIndex: " + buildIndex);
        EventBroker.InteractWithObject -= Interact;
        SceneManager.LoadScene(buildIndex);
    }
}
