using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Data/SoundData")]
public class SoundData : ScriptableObject
{
    [SerializeField] private string _iD = "EMPTY";
    [SerializeField] private AudioClip _audioClip = null;
    [Range(0, 1.5f)][SerializeField] private float _volume = 1.0f;

    public string ID { get => _iD; }
    public AudioClip AudioClip { get => _audioClip; }
    public float Volume { get => _volume; }
}
