using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzleFormGrid : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;



    public EPrimaryColor GetCellState(Vector3Int cellPos)
    {
        PuzzleFormCell tile = tilemap.GetTile<PuzzleFormCell>(cellPos);

        if (tile == null)
            return EPrimaryColor.VOID;

        return tile.RequestedColor;
    }



}
