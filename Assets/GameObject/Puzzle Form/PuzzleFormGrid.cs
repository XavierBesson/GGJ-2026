using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzleFormGrid : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private PixelCell[] _pixelCellList = FindObjectsOfType<PixelCell>();




    public bool VerifieFormIsCompleted()
    {
        foreach (var cell in _pixelCellList)
            if (cell.CheckColorCondition() == false)
                return false;
        return true;
    }
}
