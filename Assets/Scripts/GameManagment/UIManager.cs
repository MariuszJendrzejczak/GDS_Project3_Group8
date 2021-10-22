using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text tipText;
    [SerializeField] private Text storyIndex;
    [SerializeField] private Text storyHeader;
    [SerializeField] private Text storyText;
    private CanvasPanels canvasPanels;
    private bool storyPanelOn = false;

    private void Start()
    {
        canvasPanels = GetComponent<CanvasPanels>();
        EventBroker.UpdateTipText += UpdateTipText;
        EventBroker.UpdateStoryText += UpdateStroyText;
        EventBroker.SwitchOnOffStoryPanel += SwitchOnOffStoryPanel;
        EventBroker.SwitchOffStoryPanel += SwitchOffStoryPanel;
        EventBroker.SwitchOnOutroPanel += SwitchOnOutroPanel;
        EventBroker.LoadingPanel += StartLoadingPanel;
        EventBroker.SwitchOnOffTipPanel += SwitchOnOffTipPanel;
    }

    private void StartLoadingPanel(float seconds)
    {
        StartCoroutine(LoadingPanelCorutine(seconds));
    }

    private void SwitchOnOffTipPanel(bool value)
    {
        canvasPanels.TipsPanel.SetActive(value);
    }

    private void UpdateTipText(string value)
    {
        tipText.text = value;
    }
    private void UpdateStroyText(string index, string header, string story )
    {
        storyIndex.text = "#" + index;
        storyHeader.text = header;
        storyText.text = story;
    }
    private void SwitchOnOffStoryPanel()
    {
        if (storyPanelOn == true)
        {
            canvasPanels.StoryPanel.SetActive(false);
            storyPanelOn = false;
        }
        else if (storyPanelOn == false)
        {
            canvasPanels.StoryPanel.SetActive(true);
            storyPanelOn = true;
        }
    }
    private void SwitchOffStoryPanel()
    {
        canvasPanels.StoryPanel.SetActive(false);
        storyPanelOn = false;
    }

    private void SwitchOnOutroPanel()
    {
        canvasPanels.OutroPanel.SetActive(true);
    }

    private IEnumerator LoadingPanelCorutine(float seconds)
    {
        canvasPanels.Slider.maxValue = seconds;
        canvasPanels.Slider.value = 0;
        canvasPanels.LoadingPanel.SetActive(true);
        for(float i = 0; i < seconds; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            canvasPanels.Slider.value += 0.01f;
        }
        canvasPanels.LoadingPanel.SetActive(false);
    }
}
