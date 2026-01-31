using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
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



}
