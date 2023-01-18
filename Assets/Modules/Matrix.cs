using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct MatrixView<T>
{
    readonly IList<T> _innerArray;
    readonly MatrixBound _bounds;
    public int Width => _bounds.Width;
    public int Height => _bounds.Height;
    public int Count => _innerArray.Count;
    public MatrixView(IList<T> innerArray, int width, int height)
    {
        _innerArray = innerArray;
        _bounds = new MatrixBound((short)width, (short)height);
    }
    public T this[int x, int y]
    {
        get => _innerArray[_bounds[x, y]];
        set => _innerArray[_bounds[x, y]] = value;
    }
    public T this[int index]
    {
        get => _innerArray[index];
        set => _innerArray[index] = value;
    }
    public T this[Index x, Index y]
    {
        get => _innerArray[_bounds[x, y]];
        set => _innerArray[_bounds[x, y]] = value;
    }
    public T this[Index index]
    {
        get => _innerArray[index];
        set => _innerArray[index] = value;
    }
}
public readonly struct MatrixBound
{
    public int Lenght => Width * Height;
    public readonly short Width;
    public readonly short Height;
    public MatrixBound(short width, short height)
    {
        Width = width;
        Height = height;
    }
    public int this[int x, int y]
    {
        get
        {
            Debug.Assert(x >= 0 && x < Width, $"x: {x} is out of bounds (0, {Width})");
            Debug.Assert(y >= 0 && y < Height, $"y: {y} is out of bounds (0, {Height})");
            return x + y * Width;
        }
    }
    public (int x, int y) this[int index]
    {
        get
        {
            Debug.Assert(index >= 0 && index < Lenght, $"index: {index} is out of bounds (0, {Lenght})");
            return (index % Width, index / Width);
        }
    }
    public int this[Index x, Index y]
    {
        get
        {
            var x_offset = x.GetOffset(Width);
            var y_offset = y.GetOffset(Height);

            Debug.Assert(x_offset >= 0 && x_offset < Width, $"x: {x_offset} is out of bounds (0, {Width})");
            Debug.Assert(y_offset >= 0 && y_offset < Height, $"y: {y_offset} is out of bounds (0, {Height})");

            return y_offset * Width + x_offset;
        }
    }
    public (int x, int y) this[Index index]
    {
        get
        {
            var offset = index.GetOffset(Height * Width);

            Debug.Assert(offset >= 0 && offset < Lenght, $"index: {offset} is out of bounds (0, {Lenght})");

            return (offset % Width, offset / Width);
        }
    }
    public override string ToString() => $"({Width}, {Height})";
}