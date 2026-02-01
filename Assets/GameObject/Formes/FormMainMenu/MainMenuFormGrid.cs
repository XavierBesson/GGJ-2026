using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainMenuFormGrid : MonoBehaviour
{
    [SerializeField] private PixelCell _pixelCellStart = null;
    [SerializeField] private PixelCell _pixelCellQuit = null;

    private void Start()
    {
        GameManager.Instance.MainMenuFormGrid = this;
    }


    private void Update()
    {

    }


    public void VerifieFormIsCompleted()
    {
        print("Verified");

        if (_pixelCellStart.CheckColorCondition())
        {
            GameManager.Instance.MainMenuFormGrid = null;
            GameManager.Instance.GoToNextLevel();
        }
        else if (_pixelCellQuit.CheckColorCondition())
        {
            Application.Quit();
        }

    }
}
