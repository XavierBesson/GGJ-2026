using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    [SerializeField] private Animator _sceneTransitionAnimator;

    public static GameManager Instance { get => _instance; set => _instance = value; }
    private PuzzleFormGrid _actualFormGrid;
    

    private Dictionary<EColor, int> _colorValueDict = new Dictionary<EColor, int>();
    public Dictionary<EColor, int> ColorValueDict { get => _colorValueDict; }
    public PuzzleFormGrid ActualFormGrid { get => _actualFormGrid; set => _actualFormGrid = value; }

    void Awake()
    {
        Init();
        ColorValueDict.Add(EColor.VOID, 0);
        ColorValueDict.Add(EColor.RED, 2);
        ColorValueDict.Add(EColor.BLUE, 3);
        ColorValueDict.Add(EColor.GREEN, 4);
        ColorValueDict.Add(EColor.MAGENTA, 5);
        ColorValueDict.Add(EColor.CYAN, 7);
        ColorValueDict.Add(EColor.YELLOW, 6);
        ColorValueDict.Add(EColor.WHITE, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void GoToNextLevel()
    {
        StartCoroutine(LoadNextLevel());
    }


    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        _sceneTransitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndexScene + 1);
        _sceneTransitionAnimator.SetTrigger("Start");
    }



}
