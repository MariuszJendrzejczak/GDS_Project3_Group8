using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    protected Dictionary<string, AudioClip> musicDictionary = new Dictionary<string, AudioClip>();
    protected AudioSource audio;

    protected virtual void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    protected void PlaySound(string key)
    {
        if(musicDictionary[key] != null)
        {
            audio.Stop();
            audio.clip = musicDictionary[key];
            audio.Play();
        }
    }
    protected void StopPlaying()
    {
        audio.Stop();
    }
}
