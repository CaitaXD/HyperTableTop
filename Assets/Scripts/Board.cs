using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnumeratorExtention;
public class Board : MonoBehaviour
{
    [SerializeField]
    public Vector2Int Dimentions;
    [SerializeField]
    GameObject PrefabTile;
    [SerializeField]
    public Grid Grid;

    MatrixView<GameObject> _tiles;
    
    void Awake()
    {
        var arr_names = CoordinateNames(Dimentions.x, Dimentions.y).ToArray();
        var arr_tiles = new GameObject[Dimentions.x * Dimentions.y];
        
        _tiles = new MatrixView<GameObject>(arr_tiles, Dimentions.x, Dimentions.y);
        var names = new MatrixView<string>(arr_names, Dimentions.x, Dimentions.y);
        
        Grid = Grid == null ? GetComponent<Grid>() : Grid;
        PrefabTile = PrefabTile == null ? Resources.Load<GameObject>("Prefabs/Tile") : PrefabTile;

        
        foreach (var (x, y) in Cartesian(Dimentions.x, Dimentions.y))
        {
            var position = new Vector2(x - Dimentions.x/2, y - Dimentions.y/2);
            var rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            var tile = Instantiate(PrefabTile, position, rotation, Grid.transform);

            if(MathI.IsEven(x + y))
            {
                tile.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.black);
            }
            tile.name = names[x,y];
            _tiles[x, y] = tile;
        }
    }
}
