using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static EnumeratorExtention;
public class Board : MonoBehaviour
{
    [SerializeField]
    [SerializeAs("Dimentions")]
    internal Vector2Int _dimentions;
    [SerializeField]
    [SerializeAs("PrefabTile")]
    internal GameObject _prefabTile;
    [SerializeField]
    [SerializeAs("Grid")]
    internal Grid _grid;
    
    [SerializeField]
    [SerializeAs("Tiles")]
    MatrixView<GameObject> _tiles;
    public Type BoardType = Type.ChessBoard;
    public Transform SelectedPiece { get; private set; }
    void Awake()
    {
        var arr_names = CoordinateNames(_dimentions.x, _dimentions.y).ToArray();
        var arr_tiles = new GameObject[_dimentions.x * _dimentions.y];

        _tiles = new MatrixView<GameObject>(arr_tiles, _dimentions.x, _dimentions.y);
        var names = new MatrixView<string>(arr_names, _dimentions.x, _dimentions.y);

        _grid = _grid == null ? GetComponent<Grid>() : _grid;
        _prefabTile = _prefabTile == null ? Resources.Load<GameObject>("Prefabs/Tile") : _prefabTile;

        switch (BoardType)
        {

        case Type.ChessBoard: 
            ChessBoard(names);
            break;

        default: throw new NotImplementedException();
        }

    }

    private void ChessBoard(MatrixView<string> names)
    {
        foreach (var (x, y) in Cartesian(_dimentions.x, _dimentions.y))
        {
            var position = new Vector2(x - _dimentions.x / 2, y - _dimentions.y / 2);
            var rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            var tile = Instantiate(_prefabTile, position, rotation, _grid.transform);

            if (MathI.IsEven(x + y))
            {
                tile.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
            }
            tile.name = names[x, y];
            _tiles[x, y] = tile;
        }
    }

    public enum Type
    {
        ChessBoard
    }
}
