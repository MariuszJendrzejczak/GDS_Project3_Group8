using UnityEngine;

public class CharacterSFX2Layer : AudioManager
{
    [SerializeField] protected AudioClip charJump, charCooldown;
    protected override void Awake()
    {
        musicDictionary.Add("jump", charJump);
        musicDictionary.Add("cooldown", charCooldown);

        base.Awake();
        EventBroker.PlayCharacterSfxLayer2 += PlaySound;
        EventBroker.StopPlayingCharacterSfxLayer2 += StopPlaying;
    }
}