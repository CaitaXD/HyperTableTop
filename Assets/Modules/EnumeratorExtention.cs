using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class EnumeratorExtention
{
    public static RangeEnumerator GetEnumerator(this Range range)
    {
        return new RangeEnumerator(range);
    }
    public static CartesianPairEnumerator Cartesian(int width, int height)
    {
        return new CartesianPairEnumerator(..width, ..height);
    }
    
    public static IEnumerable<string> CoordinateNames(int width, int height)
    {
        foreach (var (x, y) in Cartesian(width, height))
        {
            var line = $"{(x / 26 == 0 ? "" : (char)('A' + (y - 1) / 26))}{(char)('A' + x % 26)}";
            var column = y + 1;
            yield return $"{line}{column}";
        }
    }

    public static IEnumerable<(T Value, int Index)> Index<T>(this IEnumerable<T> enumerable) => enumerable.Select((value, index) => (value, index));
}
public struct CartesianPairEnumerator : IEnumerable<(int X, int Y)>
{
    public (int X, int Y) _current;
    public (int X, int Y) Current => _current;
    private readonly System.Range _rangeX;
    private readonly System.Range _rangeY;
    public CartesianPairEnumerator(System.Range rangeX, System.Range rangeY)
    {
        Debug.Assert(rangeX.Start.IsFromEnd is false, "What do you even thing is going to happen?");
        Debug.Assert(rangeY.Start.IsFromEnd is false, "What do you even thing is going to happen?");

        _rangeX = rangeX;
        _rangeY = rangeY;
        _current = (rangeX.Start.Value -1, rangeY.Start.Value);
    }  
    public bool MoveNext()
    {
        if (Current.X < _rangeX.End.Value - 1)
        {
            _current.X++;
            return true;
        }
        else if (Current.Y < _rangeY.End.Value - 1)
        {
            _current.X = _rangeX.Start.Value;
            _current.Y++;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Reset()
    {
        _current = (_rangeX.Start.Value, _rangeY.Start.Value);
    }
    public CartesianPairEnumerator GetEnumerator()
    {
        return this;
    }

    IEnumerator<(int X, int Y)> IEnumerable<(int X, int Y)>.GetEnumerator()
    {
        foreach (var (x, y) in this)
        {
            yield return (x, y);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<(int X, int Y)>)this).GetEnumerator();
    }
}
public struct RangeEnumerator
{
    private readonly int _start;
    private readonly int _end;
    private int _current;
    public RangeEnumerator(int start, int end)
    {
        _start = start;
        _end = end;
        _current = start - 1;
    }
    public RangeEnumerator(System.Range range)
    {
        Debug.Assert(range.Start.IsFromEnd is false, "What do you even thing is going to happen?");

        _start = range.Start.Value;
        _end = range.End.Value;
        _current = range.Start.Value - 1;
    }
    public bool MoveNext()
    {
        if (_current < _end)
        {
            _current++;
            return true;
        }
        return false;
    }
    public void Reset()
    {
        _current = _start - 1;
    }
    public int Current => _current;
    public RangeEnumerator GetEnumerator()
    {
        return this;
    }
}
