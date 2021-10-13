using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnCollision : MonoBehaviour
{
    [Tooltip("0 - MainMenu, 1 - Tutorial, 2 - Hub, 3 - Yellow, 4 - Red, 5 - Outro")]
    [SerializeField] private int sceneBuildIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}
