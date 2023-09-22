using System;
using UnityEngine;

[Serializable]
public class Row<T>
{
    [SerializeField] private T[] _elements;

    public int Column => _elements == null ? 0 : _elements.Length;

    public Row(int column)
    {
        _elements = new T[column];
    }

    public T At(int column)
    {
        if (column < 0 || column > _elements.Length - 1)
        {
            LogException(column);
            return default;
        }

        return _elements[column];
    }

    public void Set(int column, T value)
    {
        if (column < 0 || column > _elements.Length - 1) LogException(column);

        _elements[column] = value;
    }

    public T LeftOf(int column)
    {
        if (column - 1 < 0 || column > _elements.Length - 1)
        {
            LogException(column);
            return default;
        }

        return _elements[column - 1];
    }

    public T RightOf(int column)
    {
        if (column < 0 || column + 1 > _elements.Length - 1)
        {
            LogException(column);
            return default;
        }

        return _elements[column + 1];
    }

    private void LogException(int column)
    {
        //Debug.LogError($"out of length exception: length:{_elements.Length} - column: {column}");
    }
}