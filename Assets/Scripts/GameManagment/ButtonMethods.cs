using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMethods : MonoBehaviour
{
    private CanvasPanels panels;
    private int introCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        panels = GetComponent<CanvasPanels>();
    }

    #region MainMenuBtns
    public void StartGameButton()
    {
        panels.IntroPanel.SetActive(true);
        panels.IntroImage1.SetActive(true);
    }

    public void HowToPlayBtn()
    {
        panels.HowToPlayPanel.SetActive(true);
    }

    public void CreditsBtn()
    {
        panels.CredistPanel.SetActive(true);
        panels.OutroPanel.SetActive(false);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
    #endregion

    #region IntroPanelBtn
    public void NextBtn()
    {
        switch (introCounter)
        {
            case 0:
                panels.IntroImage1.SetActive(false);
                panels.IntroImege2.SetActive(true);
                introCounter++;
                break;
            case 1:
                panels.IntroImege2.SetActive(false);
                panels.IntroImage3.SetActive(true);
                introCounter++;
                break;
            case 2:
                panels.IntroImage3.SetActive(false);
                panels.IntroPanel.SetActive(false);
                introCounter = 0;
                SceneManager.LoadScene(1);
                break;
        }
    }
    #endregion

    #region PanelsBackBtns
    public void CreditsBackBtn()
    {
        panels.CredistPanel.SetActive(false);
    }
    public void HowToPlayBackBtn()
    {
        panels.HowToPlayPanel.SetActive(false);
    }
    #endregion

    #region OutroBtns

    #endregion
}
