using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceLogic
{
    private int index;
    private string type;
    private int length;

    public PieceLogic()
    {
    }
    public PieceLogic(int index, string type, int length)
    {
        this.index = index;
        this.type = type;
        this.length = length;
    }

    public void Move(int destIndex)
    {
        int offset = destIndex - index;
        bool destValido = destIndex >= 0 && destIndex < length;

        if (destValido && AllPieces.Moves_Table[type].Contains(offset))
        {
            index = destIndex;
        } else
        {
            Debug.Log("Movimento nao sucedido");
        }
    }

    public int getIndex()
    {
        return index;
    }
}
