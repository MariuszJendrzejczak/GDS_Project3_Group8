using System.Collections.Generic;
using UnityEngine;

public class ThamesSFX : AudioManager
{
    [SerializeField] protected AudioClip mainMenuThame, tutorialThame, hubThame, yellowThame, redTheme, levelEnd, deathThame;
    protected override void Awake()
    {
        musicDictionary.Add("menu", mainMenuThame);
        musicDictionary.Add("tutorial", tutorialThame);
        musicDictionary.Add("hub", hubThame);
        musicDictionary.Add("yellow", yellowThame);
        musicDictionary.Add("red", redTheme);
        musicDictionary.Add("levelend", levelEnd);
        musicDictionary.Add("deaththame", deathThame);
        base.Awake();
        EventBroker.PlayThameSfx += PlaySound;
        EventBroker.StopPlayingThameSfx += StopPlaying;
    }
}