using System.Collections.Generic;
using UnityEngine;

public class ObjectSFX : AudioManager
{
    [SerializeField] protected AudioClip objDoor, objBtn, objElevetorRunning, objLever, objPush, objLedderUse, objBoxFall, objShootedButton;
    protected override void Awake()
    {
        musicDictionary.Add("door", objDoor);
        musicDictionary.Add("button", objBtn);
        musicDictionary.Add("elevator", objElevetorRunning);
        musicDictionary.Add("lever", objLever);
        musicDictionary.Add("ledder", objLedderUse);
        musicDictionary.Add("boxfall", objBoxFall);
        musicDictionary.Add("push", objPush);
        musicDictionary.Add("btnshoot", objShootedButton);
        base.Awake();
        EventBroker.PlayObjectSfx += PlaySound;
        EventBroker.StopPlayingObjectSfx += StopPlaying;
    }
}
