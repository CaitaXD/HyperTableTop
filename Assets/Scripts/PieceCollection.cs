using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCollection : MonoBehaviour
{
    public Board board;
    int length;
    // Dicionário com todos os movimentos possiveis
    public readonly static Dictionary<PieceLogic.Type, int[]> Moves_Table;
    // Um array que representa as posições na mesa, e o número que el guarda é o index do objeto no PieceList;
    public int[] EspacosOcupados;
    // Uma lista de todas as peças na mesa em ordem de criação
    PieceLogic[] PieceList;
    [SerializeField]
    GameObject PrefabPiece;
    private int pieceCounter;

    static PieceCollection()
    {
        Moves_Table = new Dictionary<PieceLogic.Type, int[]>
        {
            {PieceLogic.Type.Checker, new int[4]{-9,-7,7,9} },
            {PieceLogic.Type.Pawn, new int[4]{-8,-1,1,8} }
        };
    }
    public void Awake()
    {
        board = board == null ? GetComponent<Board>() : board;

        length = board._dimentions.x * board._dimentions.y;
        PieceList = new PieceLogic[length];
        EspacosOcupados = new int[length];
        for (int i = 0; i < length; i++)
        {
            PieceList[i] = new PieceLogic();
            EspacosOcupados[i] = -1;
        }

        pieceCounter = 0;

        PrefabPiece = PrefabPiece == null ? Resources.Load<GameObject>("Prefabs/Piece") : PrefabPiece;
    }
    public void AddPiece(int index, PieceLogic.Type type)
    {
        if (index < 0 || index >= length)
        {
            Debug.Log("Index invalido");
            return;
        }
        else if (EspacosOcupados[index] != -1)
        {
            Debug.Log("Lugar já ocupado");
            return;
        }

        PieceLogic piece = new(index, type, length);
        PieceList[pieceCounter] = piece;
        EspacosOcupados[index] = pieceCounter;
        pieceCounter++;

        int x = piece.Index % board._dimentions.x;
        int y = piece.Index / board._dimentions.y;
        var position = new Vector2(x - board._dimentions.x / 2, y - board._dimentions.y / 2);
        var rotation = Quaternion.Euler(90, 0, 00);
        var item = Instantiate(PrefabPiece, position, rotation, board._grid.transform);

        item.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.70f, 0.13f, 0.13f));
        item.transform.position -= new Vector3(0, 0, (float)0.25);
    }
    public void AddChecker()
    {
        AddPiece(pieceCounter, PieceLogic.Type.Checker);
    }
    public void PrintOccupiedSpaces()
    {
        Debug.Log("Espacos Ocupados: <");
        for (int i = 0; i < length; i++)
            Debug.Log(EspacosOcupados[i]);
        Debug.Log(">");
    }
}
