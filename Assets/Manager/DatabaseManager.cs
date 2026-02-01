using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    #region Singleton
    private static DatabaseManager _instance = null;

    public static DatabaseManager Instance
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

    #region Fields
    [SerializeField] private SoundData[] _soundsArray = null;
    [SerializeField] private SoundData[] _musicsArray = null;

    private Dictionary<string, SoundData> _sounds = null;
    private Dictionary<string, SoundData> _musics = null;
    #endregion Fields

    public void Init()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _sounds = new Dictionary<string, SoundData>();
        _musics = new Dictionary<string, SoundData>();
        for (int i = 0; i < _soundsArray.Length; i++)
        {
            _sounds.Add(_soundsArray[i].ID, _soundsArray[i]);
        }
        for (int i = 0; i < _musicsArray.Length; i++)
        {
            _musics.Add(_musicsArray[i].ID, _musicsArray[i]);
        }
    }

    public SoundData GetMusicByID(string iD)
    {
        if (_musics[iD] != null)
            return _musics[iD];
        return null;
    }

    public SoundData GetSFXByID(string iD)
    {
        if (_sounds[iD] != null)
            return _sounds[iD];
        return null;
    }
}
