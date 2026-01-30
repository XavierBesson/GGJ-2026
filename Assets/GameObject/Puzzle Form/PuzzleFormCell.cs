using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "Tiles/PuzzleFormCell")]
public class PuzzleFormCell : Tile
{
    [SerializeField] private Vector3Int _position;
    [SerializeField] private EPrimaryColor _requestedColor;

    public Vector3Int Position { get => _position; }
    public EPrimaryColor RequestedColor { get => _requestedColor; }
}
