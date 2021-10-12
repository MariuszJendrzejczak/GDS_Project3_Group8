using UnityEngine;

public class ObjectSFXLayer2 : AudioManager
{
    [SerializeField] protected AudioClip objElevetorRunning, objDoor;

    protected override void Awake()
    {
        musicDictionary.Add("door", objDoor);
        musicDictionary.Add("elevator", objElevetorRunning);
        base.Awake();
        EventBroker.PlayObjectSfxLayer2 += PlaySound;
        EventBroker.StopPlayingObjectSfxLayer2 += StopPlaying;
    }
}
