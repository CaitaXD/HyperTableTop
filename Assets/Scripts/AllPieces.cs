using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPieces : MonoBehaviour
{
    public Board board;
    int length => board.Dimentions.x * board.Dimentions.y;
    public readonly static Dictionary<string, int[]> Moves_Table; // Dicion�rio com todos os movimentos possiveis
    public int[] EspacosOcupados; // Um array que representa as posi��es na mesa, e o n�mero que el guarda � o index do objeto no PieceList;
    PieceLogic[] PieceList; // Uma lista de todas as pe�as na mesa em ordem de cria��o
    [SerializeField]
    GameObject PrefabPiece;
    private int pieceCounter;

    static AllPieces()
    {
        Moves_Table = new Dictionary<string, int[]>
        {
            {"dama", new int[4]{-9,-7,7,9} },
            {"peao", new int[4]{-8,-1,1,8} }
        };
    }

    public void Awake()
    {
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

    private void Update()
    {
    }

    public void AddPiece(int index, string type)
    {
        if(index < 0 || index >= length)
        {
            Debug.Log("Index invalido");
            return;
        }
        else if (EspacosOcupados[index] != -1)
        {
            Debug.Log("Lugar j� ocupado");
            return;
        }

        PieceLogic piece = new PieceLogic(index, type, length);
        PieceList[pieceCounter] = piece;
        EspacosOcupados[index] = pieceCounter;
        pieceCounter++;

        int x = piece.getIndex() % board.Dimentions.x;
        int y = piece.getIndex() / board.Dimentions.y;
        var position = new Vector2(x - board.Dimentions.x / 2, y - board.Dimentions.y / 2);
        var rotation = Quaternion.Euler(90, 0, 00);
        var item = Instantiate(PrefabPiece, position, rotation, board.Grid.transform);

        item.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.70f, 0.13f, 0.13f));
        item.transform.position -= new Vector3(0, 0, (float)0.25);
    }

    public void AddBasic()
    {
        AddPiece(pieceCounter, "dama");
    }


    public void imprimeEspacoOcupados()
    {
        Debug.Log("Espacos Ocupados: <");
        for (int i = 0; i < length; i++)
            Debug.Log(EspacosOcupados[i]);
        Debug.Log(">");
    }

}
