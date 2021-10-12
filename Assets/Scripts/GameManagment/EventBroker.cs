
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroker : MonoBehaviour
{
    #region GameLogic
    public static event Action PlayerDeath;
    public static void CallPlayerDeath()
    {
        if (PlayerDeath != null)
        {
            PlayerDeath();
        }
    }

    public static event Action InteractWithObject;
    public static void CallInteractWithObject()
    {
        if (InteractWithObject != null)
        {
            InteractWithObject();
        }
    }

    public static event Action RespawnToCheckPoint;
    public static void CallRespawntoCheckPoint()
    {
        if (RespawnToCheckPoint != null)
        {
            RespawnToCheckPoint();
        }
    }

    public static event Action<GameObject> CheckPointReached;
    public static void CallCheckPointReached(GameObject value)
    {
        if (CheckPointReached != null)
        {
            CheckPointReached(value);
        }
    }

    public static event Action<PoolingObject> GiveAllEnemyesOnSceneBulletPoolReference;
    public static void CallGiveAllEnemyesOnSceneBulletPoolReference(PoolingObject pool)
    {
        if (GiveAllEnemyesOnSceneBulletPoolReference != null)
        {
            GiveAllEnemyesOnSceneBulletPoolReference(pool);
        }
    }

    public static event Action<PoolingObject> GiveAllTurretsOnSceneBulletPoolRefenece;
    public static void CallGiveAllTurretsOnSceneBulletPoolRefenece(PoolingObject pool)
    {
        if (GiveAllTurretsOnSceneBulletPoolRefenece != null)
        {
            GiveAllTurretsOnSceneBulletPoolRefenece(pool);
        }
    }

    public static event Action<PlayerController> GiveToAllPlayerCharacterRef;
    public static void CallGiveToAllPlayerCharacterRef(PlayerController player)
    {
        if (GiveToAllPlayerCharacterRef != null)
        {
            GiveToAllPlayerCharacterRef(player);
        }
    }

    public static event Action<bool> ChangeOnElevatorBoolOnPlayer;
    public static void CallChangeElevatorBoolOnPlayer(bool value)
    { 
        if (ChangeOnElevatorBoolOnPlayer != null)
        {
            ChangeOnElevatorBoolOnPlayer(value);
        }
    }

    #endregion
    #region UI
    public static event Action<String> UpdateTipText;
    public static void CallUpdateTipText(String value)
    {
        if (UpdateTipText != null)
        {
            UpdateTipText(value);
        }
    }

    public static event Action<String, String, String> UpdateStoryText;
    public static void CallUpdateStoryText(String value, String value2, String value3)
    {
        if (UpdateStoryText != null)
        {
            UpdateStoryText(value, value2, value3);
        }
    }
    public static event Action SwitchOnOffStoryPanel;
    public static void CallSwithcOnOffStoryPanel()
    {
        if (SwitchOnOffStoryPanel != null)
        {
            SwitchOnOffStoryPanel();
        }
    }

    public static event Action SwitchOffStoryPanel;
    public static void CallSwitchOffStoryPanel()
    {
        if (SwitchOffStoryPanel != null)
        {
            SwitchOffStoryPanel();
        }
    }

    public static event Action SwitchOnOutroPanel;
    public static void CallSwitchOnOutroPanel()
    {
        if (SwitchOnOutroPanel != null)
        {
            SwitchOnOutroPanel();
        }
    }
    #endregion
    #region SFX
    public static Action<string> PlayCharacterSfx;
    public static void CallCharacterPlaySfx(string key)
    {
        if (PlayCharacterSfx != null)
        {
            PlayCharacterSfx(key);
        }
    }
    public static Action<string> PlayCharacterSfxLayer2;
    public static void CallCharacterPlaySfxLayer2(string key)
    {
        if (PlayCharacterSfxLayer2 != null)
        {
            PlayCharacterSfxLayer2(key);
        }
    }
    public static Action<string> PlayEnemySfx;
    public static void CallEnemyPlaySfx(string key)
    {
        if (PlayEnemySfx != null)
        {
            PlayEnemySfx(key);
        }
    }
    public static Action<string> PlayObjectSfx;
    public static void CallObjectPlaySfx(string key)
    {
        if (PlayObjectSfx != null)
        {
            PlayObjectSfx(key);
        }
    }
    public static Action<string> PlayObjectSfxLayer2;
    public static void CallObjectPlaySfxLayer2(string key)
    {
        if (PlayObjectSfxLayer2 != null)
        {
            PlayObjectSfxLayer2(key);
        }
    }
    public static Action<string> PlayThameSfx;
    public static void CallPlayThemeSfx(string key)
    {
        if (PlayThameSfx != null)
        {
            PlayThameSfx(key);
        }
    }

    public static Action StopPlayingCharacterSfx;
    public static void CallStopPlayingCharacterSfx()
    {
        if (StopPlayingCharacterSfx != null)
        {
            StopPlayingCharacterSfx();
        }
    }
    public static Action StopPlayingCharacterSfxLayer2;
    public static void CallStopPlayingCharacterSfxLayer2()
    {
        if (StopPlayingCharacterSfxLayer2 != null)
        {
            StopPlayingCharacterSfxLayer2();
        }
    }
    public static Action StopPlayingEnemySfx;
    public static void CallStopPlayingEnemySfx()
    {
        if (StopPlayingEnemySfx != null)
        {
            StopPlayingEnemySfx();
        }
    }
    public static Action StopPlayingObjectSfx;
    public static void CallStopPlayingObjectSfx()
    {
        if (StopPlayingObjectSfx != null)
        {
            StopPlayingObjectSfx();
        }
    }
    public static Action StopPlayingObjectSfxLayer2;
    public static void CallStopPlayingObjectSfxLayer2()
    {
        if (StopPlayingObjectSfxLayer2 != null)
        {
            StopPlayingObjectSfxLayer2();
        }
    }
    public static Action StopPlayingThameSfx;
    public static void CallStopPlayingThameSfx()
    {
        if (StopPlayingThameSfx != null)
        {
            StopPlayingThameSfx();
        }
    }
    #endregion
}
