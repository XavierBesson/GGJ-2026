using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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

    private AudioMixerSnapshot _inMenuSnapshot = null;
    private AudioMixerSnapshot _inGameSnapshot = null;

    public void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _inMenuSnapshot = GetComponent<AudioMixerSnapshot>();
        _inGameSnapshot = GetComponent<AudioMixerSnapshot>();   
    }

    private void Start()
    {
        PlayMusic("FOCUS_AMBIENT");
    }


    public void PlaySFXOneShot(string iD, bool changePitch)
    {
        SoundData sound = DatabaseManager.Instance.GetSFXByID(iD);
        _SFXSource.PlayOneShot(sound.AudioClip, sound.Volume);

        if (changePitch)
        {
            _SFXSource.pitch = Random.Range(0.9f, 1.4f);
        }
    }

    public void PlayMusic(string iD)
    {
        SoundData music = DatabaseManager.Instance.GetMusicByID(iD);
        _musicSource.Stop();
        _musicSource.clip = music.AudioClip;
        _musicSource.volume = music.Volume; 
        _musicSource.Play();
    }

    public void SetLowPassChange(bool canChange)
    {
        if (canChange)
        {
            _inGameSnapshot.TransitionTo(0.01f);
        }

        else if (!canChange)
        {
            _inMenuSnapshot.TransitionTo(0.01f);
        }
    }

}
