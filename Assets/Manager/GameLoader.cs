using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private DatabaseManager _databaseManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField, SceneInBuild] private int _sceneInBuild;   

    private void Start()
    {
        _inputManager.Init();
        _gameManager.Init();
        _databaseManager.Init();
        _audioManager.Init();
        SceneManager.LoadScene(_sceneInBuild); 
    }
}
