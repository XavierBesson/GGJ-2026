using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance = null;

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
            else
            {
                Debug.LogError("Multiple Singleton Detected.");
            }
        }
    }
    #endregion Singleton

    [SerializeField] private AudioSource _SFXSource = null;
    [SerializeField] private AudioSource _musicSource = null;

    public void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic("FOCUS_AMBIENT");
    }


    public void PlaySFXOneShot(string iD)
    {
        SoundData sound = DatabaseManager.Instance.GetSFXByID(iD);
        _SFXSource.PlayOneShot(sound.AudioClip, sound.Volume);
    }

    public void PlayMusic(string iD)
    {
        SoundData music = DatabaseManager.Instance.GetMusicByID(iD);
        _musicSource.Stop();
        _musicSource.clip = music.AudioClip;
        _musicSource.volume = music.Volume; 
        _musicSource.Play();
    }

}
