using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Matrix<T>
{
    [SerializeField] private Row<T>[] _rows;

    public Matrix(int row, int column)
    {
        _rows = new Row<T>[row];
        //Row = row;
        //Column = column;
        for (var i = 0; i < row; i++) _rows[i] = new Row<T>(column);
    }

    public int Row => _rows == null ? 0 : _rows.Length;
    public int Column => _rows == null ? 0 : (_rows.Length != 0 ? _rows[0].Column : 0);

    public T At(int row, int column)
    {
        if (row < 0 || row > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row].At(column);
    }

    public void Set(int row, int column, T value)
    {
        if (row < 0 || row > _rows.Length - 1) LogException(row);

        _rows[row].Set(column, value);
    }
    public void Set(MatrixIndex index, T value)
    {
        if (index.Row < 0 || index.Row > _rows.Length - 1) LogException(index.Row);
        _rows[index.Row].Set(index.Column, value);
    }
    public T LeftOf(int row, int column)
    {
        if (row < 0 || row > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row].LeftOf(column);
    }

    public T RightOf(int row, int column)
    {
        if (row < 0 || row > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row].RightOf(column);
    }

    public T UpOf(int row, int column)
    {
        if (row - 1 < 0 || row > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row - 1].At(column);
    }

    public T DownOf(int row, int column)
    {
        if (row < 0 || row + 1 > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row + 1].At(column);
    }

    public T UpLeftOf(int row, int column)
    {
        if (row < 0 || row + 1 > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row + 1].LeftOf(column);
    }

    public T UpRightOf(int row, int column)
    {
        if (row < 0 || row + 1 > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row + 1].RightOf(column);
    }

    public T DownLeftOf(int row, int column)
    {
        if (row - 1 < 0 || row > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row - 1].LeftOf(column);
    }

    public T DownRightOf(int row, int column)
    {
        if (row - 1 < 0 || row > _rows.Length - 1)
        {
            LogException(row);
            return default;
        }

        return _rows[row - 1].RightOf(column);
    }

    private void LogException(int row)
    {
        //Debug.LogError($"out of length exception: length:{_rows.Length} - row: {row}\nT type: {typeof(T)}");
    }
}