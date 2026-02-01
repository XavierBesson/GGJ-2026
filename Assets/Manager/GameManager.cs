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
    [SerializeField] private Texture2D _mouseCursorHover = null;
    [SerializeField] private Texture2D _mouseCursorUnhover = null;

    public static GameManager Instance { get => _instance; set => _instance = value; }
    private PuzzleFormGrid _actualFormGrid;
    private bool _inTransition = false;
    private MainMenuFormGrid _mainMenuFormGrid;
    

    private Dictionary<EColor, int> _colorValueDict = new Dictionary<EColor, int>();
    public Dictionary<EColor, int> ColorValueDict { get => _colorValueDict; }
    public PuzzleFormGrid ActualFormGrid { get => _actualFormGrid; set => _actualFormGrid = value; }
    public bool InTransition { get => _inTransition; set => _inTransition = value; }
    public MainMenuFormGrid MainMenuFormGrid { get => _mainMenuFormGrid; set => _mainMenuFormGrid = value; }
    public Texture2D MouseCursorHover { get => _mouseCursorHover; set => _mouseCursorHover = value; }
    public Texture2D MouseCursorUnhover { get => _mouseCursorUnhover; set => _mouseCursorUnhover = value; }

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
        InTransition = true;
        AudioManager.Instance.PlaySFXOneShot("NEW_LEVEL", false);
        yield return new WaitForSeconds(1.5f);
        _sceneTransitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        
        int currentIndexScene = SceneManager.GetActiveScene().buildIndex;

        if (currentIndexScene < 11)
        {

            SceneManager.LoadScene(currentIndexScene + 1);
        }

        else if (currentIndexScene == 11)
        {
            SceneManager.LoadScene("MainMenu");
        }
            _sceneTransitionAnimator.SetTrigger("Start");
        InTransition = false;
    }



}
