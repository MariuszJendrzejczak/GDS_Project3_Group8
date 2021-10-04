﻿using System.Collections.Generic;
using UnityEngine;

public class ThamesSFX : AudioManager
{
    [SerializeField] protected AudioClip mainMenuThame, mainMenuBtn, levelThame, levelEnd, deathThame;
    protected override void Awake()
    {
        musicDictionary.Add("menu", mainMenuThame);
        musicDictionary.Add("menubtn", mainMenuBtn);
        musicDictionary.Add("level", levelThame);
        musicDictionary.Add("levelend", levelEnd);
        musicDictionary.Add("deaththame", deathThame);
        base.Awake();
        EventBroker.PlayThameSfx += PlaySound;
        EventBroker.StopPlayingThameSfx += StopPlaying;
    }
}