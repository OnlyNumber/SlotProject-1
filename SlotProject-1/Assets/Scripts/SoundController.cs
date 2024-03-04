using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    [SerializeField]
    private SwitchSprite _buttonMusic;

    [SerializeField]
    private SwitchSprite _buttonClip;

    [SerializeField]
    private AudioSource _backgroundMusic;

    public Action<bool> OnClipSoundChange;

    public Action<bool> OnMusicSoundChange;

    private void Start()
    {

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _backgroundMusic.Play();

        Instance.OnClipSoundChange += _buttonClip.SwitchSpriteMethod;

        Instance.OnMusicSoundChange += _buttonMusic.SwitchSpriteMethod;

        DataControl.Instance.OnDataLoaded += Initialize;
        

    }

    public void Initialize()
    {
        OnClipSoundChange?.Invoke(FromIntToBool(DataControl.Instance.CurrentPlayerData.VolumeClip));
        OnMusicSoundChange?.Invoke(FromIntToBool(DataControl.Instance.CurrentPlayerData.VolumeMusic));
        _backgroundMusic.volume = DataControl.Instance.CurrentPlayerData.VolumeMusic;
    }

    public void PlayAudioClip(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, DataControl.Instance.CurrentPlayerData.VolumeClip);
    }

    public void SetSound(int soundType)
    {
        switch((TypeSound)soundType)
        {
            case TypeSound.clip:
                {
                    SetMusicOpposite(ref DataControl.Instance.CurrentPlayerData.VolumeClip);
                    OnClipSoundChange?.Invoke(FromIntToBool(DataControl.Instance.CurrentPlayerData.VolumeClip));

                    break;
                }
            case TypeSound.music:
                {
                    SetMusicOpposite(ref DataControl.Instance.CurrentPlayerData.VolumeMusic);
                    _backgroundMusic.volume = DataControl.Instance.CurrentPlayerData.VolumeMusic;
                    OnMusicSoundChange?.Invoke(FromIntToBool(DataControl.Instance.CurrentPlayerData.VolumeMusic));
                    break;
                }
        }
    }
    
    public bool FromIntToBool(int i)
    {
        return i == 0 ? false : true;
    }


    public void SetMusicOpposite(ref int volume)
    {
        if (volume == 1)
        {
            volume = 0;
        }
        else
        {
            volume = 1;
        }

        //_defaultMusic.volume = IsMusic;

        //OnChangeVolume?.Invoke();
    }

}

public enum TypeSound
{
    clip,
    music
}