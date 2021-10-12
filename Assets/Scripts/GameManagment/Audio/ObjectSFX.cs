using System.Collections.Generic;
using UnityEngine;

public class ObjectSFX : AudioManager
{
    [SerializeField] protected AudioClip objBtn, objLever, objPush, objLedderUse, objBoxFall, objShootedButton, noteInteraction, mainMenuBtn;
    protected override void Awake()
    {
        musicDictionary.Add("button", objBtn);
        musicDictionary.Add("lever", objLever);
        musicDictionary.Add("ledder", objLedderUse);
        musicDictionary.Add("boxfall", objBoxFall);
        musicDictionary.Add("push", objPush);
        musicDictionary.Add("btnshoot", objShootedButton);
        musicDictionary.Add("note", noteInteraction);
        musicDictionary.Add("menubtn", mainMenuBtn);
        base.Awake();
        EventBroker.PlayObjectSfx += PlaySound;
        EventBroker.StopPlayingObjectSfx += StopPlaying;
    }
}
