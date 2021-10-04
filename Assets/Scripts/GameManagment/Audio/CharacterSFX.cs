using System.Collections.Generic;
using UnityEngine;
public class CharacterSFX : AudioManager
{
    [SerializeField] protected AudioClip charClimb, charDeath, charEffort, charGunShot, charRunning, charCoverOn, charCoverOff;
    protected override void Awake()
    {
        musicDictionary.Add("climb", charClimb);
        musicDictionary.Add("death", charDeath);
        musicDictionary.Add("effort", charEffort);
        musicDictionary.Add("shoot", charGunShot);
        musicDictionary.Add("run", charRunning);
        musicDictionary.Add("coveron", charCoverOn);
        musicDictionary.Add("coveroff", charCoverOff);
        base.Awake();
        EventBroker.PlayCharacterSfx += PlaySound;
        EventBroker.StopPlayingCharacterSfx += StopPlaying;
    }
}
