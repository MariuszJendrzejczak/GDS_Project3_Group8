using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControler : MonoBehaviour
{
    public AudioSource CharacterSFXLayer1, CharacterSFXLayer2, EnemySFX, ObjSFXLayer1, ObjSFXLayer2, Thames;
    private List<AudioSource> sfxSourcesList = new List<AudioSource>();
    private float musicVolumeBeforeMute = 1, sfxVolumeBeforeMute = 1 ;
    void Start()
    {
        sfxSourcesList.Add(CharacterSFXLayer1);
        sfxSourcesList.Add(CharacterSFXLayer2);
        sfxSourcesList.Add(EnemySFX);
        sfxSourcesList.Add(ObjSFXLayer1);
        sfxSourcesList.Add(ObjSFXLayer2);
    }

    // Update is called once per frame
    void Update()
    {
        AudioControlByButtons();
    }

    private void AudioControlByButtons()
    {
        //mute music
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(Thames.volume > 0)
            {
                Thames.volume = 0;
            }
            else if(Thames.volume == 0)
            {
                Thames.volume = 0.7f;
            }
        }

        //mute sfx
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(CharacterSFXLayer1.volume > 0)
            {
                foreach (AudioSource source in sfxSourcesList)
                {
                    source.volume = 0;
                }
            }
            else if (CharacterSFXLayer1.volume == 0)
            {
                foreach (AudioSource source in sfxSourcesList)
                {
                    source.volume = 0.7f;
                }
            }
        }

        // tune up and down music and sfx

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            foreach (AudioSource source in sfxSourcesList)
            {
                if (source.volume < 0.7f)
                {
                    source.volume += 0.1f;
                }
            }
            if (Thames.volume < 0.7f)
            {
                Thames.volume += 0.1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            foreach (AudioSource source in sfxSourcesList)
            {
                if (source.volume > 0)
                {
                    source.volume -= 0.1f;
                }
            }
            if (Thames.volume > 0)
            {
                Thames.volume -= 0.1f;
            }
        }
    }
}
