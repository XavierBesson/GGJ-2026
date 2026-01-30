using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzleFormGrid : MonoBehaviour
{
    private PixelCell[] _pixelCellList = null;

    private void Start()
    {
        _pixelCellList = FindObjectsOfType<PixelCell>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            print(VerifieFormIsCompleted());
    }


    public bool VerifieFormIsCompleted()
    {
        foreach (var cell in _pixelCellList)
            if (cell.CheckColorCondition() == false)
                return false;
        return true;
    }
}
