using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceLogic
{
    public int Index { get; private set; }
    private readonly Type type;
    private readonly int length;
    public enum Type
    {
        Checker,
        Pawn,
    }
    public PieceLogic()
    {
    }
    public PieceLogic(int index, Type type, int length)
    {
        this.Index = index;
        this.type = type;
        this.length = length;
    }

    public void Move(int destIndex)
    {
        int offset = destIndex - Index;
        bool destValido = destIndex >= 0 && destIndex < length;

        if (destValido && PieceCollection.Moves_Table[type].Contains(offset))
        {
            Index = destIndex;
        } else
        {
            Debug.Log("Movimento nao sucedido");
        }
    }
}
