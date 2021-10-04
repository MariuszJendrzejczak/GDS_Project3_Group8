using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : AudioManager
{
    [SerializeField] protected AudioClip enemyGunshot, enemyDeath;
    protected override void Awake()
    {
        musicDictionary.Add("enemyshoot", enemyGunshot);
        musicDictionary.Add("ememydeath", enemyDeath);
        base.Awake();
        EventBroker.PlayEnemySfx += PlaySound;
        EventBroker.StopPlayingEnemySfx += StopPlaying;
    }
}
