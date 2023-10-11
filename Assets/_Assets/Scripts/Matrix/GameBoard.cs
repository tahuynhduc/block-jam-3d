using System;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard<TTransform, TElementType, TElement> : MonoBehaviour where TElement : MonoBehaviour, IIndex
{
    [SerializeField] private Matrix<TTransform> _transformMatrix;

    [SerializeField] private Matrix<TElementType> _elementTypeMatrix;

    [SerializeField] private Matrix<TElement> _elementMatrix;

    [SerializeField] private List<GameObject> _elementPfbs;

    private Dictionary<Type, object> _dictionary;


    /// <summary>
    /// Max Row
    /// </summary>
    public int Row => ElementTypeMatrix.Row;
    /// <summary>
    /// Max column
    /// </summary>
    public int Column => ElementTypeMatrix.Column;

    public Matrix<TTransform> TransformMatrix
    {
        get => _transformMatrix;
        set => _transformMatrix = value;
    }

    public Matrix<TElementType> ElementTypeMatrix
    {
        get => _elementTypeMatrix;
        set => _elementTypeMatrix = value;
    }

    public Matrix<TElement> ElementMatrix
    {
        get => _elementMatrix;
        set => _elementMatrix = value;
    }
    public void CreateDic()
    {
        _dictionary = new Dictionary<Type, object>();
        _dictionary.Add(typeof(TTransform), TransformMatrix);
        _dictionary.Add(typeof(TElementType), ElementTypeMatrix);
        CreateElementMatrix();
        _dictionary.Add(typeof(TElement), ElementMatrix);
    }
    public void CreateUI()
    {
        _dictionary = new Dictionary<Type, object>();
        _dictionary.Add(typeof(TTransform), TransformMatrix);
        _dictionary.Add(typeof(TElementType), ElementTypeMatrix);
        CreateUIMatrix();
        _dictionary.Add(typeof(TElement), ElementMatrix);
    }
    public void LoadTypeElementMatrix(TElementType[,] elementTypes)
    {
        var row = elementTypes.GetUpperBound(0);
        var column = elementTypes.GetUpperBound(1);
        ElementTypeMatrix = new Matrix<TElementType>(row, column);

        for (var i = 0; i < row; i++)
            for (var j = 0; j < column; j++)
                ElementTypeMatrix.Set(i, j, elementTypes[i, j]);
    }
    private void CreateUIMatrix()
    {
        ElementMatrix = new Matrix<TElement>(Row, Column);
        for (var i = 0; i < Row; i++)
            for (var j = 0; j < Column; j++)
            {
                var index = new MatrixIndex(i, j);
                var elementType = At<UIType>(index);
                if (elementType == UIType.None)
                    continue;
                var elementPfb = GetElementPfb(elementType);
                var elementTransform = At<Transform>(index);
                var element = Instantiate(elementPfb, elementTransform).GetComponent<TElement>();
                element.Index = index;
                ElementMatrix.Set(index, element);
            }
    }
    private void CreateElementMatrix()
    {
        ElementMatrix = new Matrix<TElement>(Row, Column);
        for (var i = 0; i < Row; i++)
            for (var j = 0; j < Column; j++)
            {
                var index = new MatrixIndex(i, j);
                var elementType = At<ElementType>(index);
                if (elementType == ElementType.None)
                    continue;
                var elementPfb = GetElementPfb(elementType);
                var elementTransform = At<Transform>(index);
                var element = Instantiate(elementPfb, elementTransform).GetComponent<TElement>();
                element.Index = index;
                ElementMatrix.Set(index, element);
            }
    }
    public void CreateMaxtrix()
    {
        _dictionary = new Dictionary<Type, object>();
        _dictionary.Add(typeof(TTransform), TransformMatrix);
        _dictionary.Add(typeof(TElementType), ElementTypeMatrix);
        _dictionary.Add(typeof(TElement), ElementMatrix);
    }
    private GameObject GetElementPfb(ElementType elementType)
    {
        return _elementPfbs[(int)elementType];
    }
    private GameObject GetElementPfb(UIType elementType)
    {
        return _elementPfbs[(int)elementType];
    }
    public void Set<T>(int row, int column, T element)
    {
        GetMatrix<T>().Set(row, column, element);
    }

    private Matrix<T> GetMatrix<T>()
    {
        return (Matrix<T>)_dictionary[typeof(T)];
    }
    public T At<T>(int row, int column)
    {
        return GetMatrix<T>().At(row, column);
    }

    public T LeftOf<T>(int row, int column)
    {
        return GetMatrix<T>().LeftOf(row, column);
    }

    public T RightOf<T>(int row, int column)
    {
        return GetMatrix<T>().RightOf(row, column);
    }

    public T UpOf<T>(int row, int column)
    {
        return GetMatrix<T>().UpOf(row, column);
    }

    public T DownOf<T>(int row, int column)
    {
        return GetMatrix<T>().DownOf(row, column);
    }

    public T UpLeftOf<T>(int row, int column)
    {
        return GetMatrix<T>().UpLeftOf(row, column);
    }

    public T UpRightOf<T>(int row, int column)
    {
        return GetMatrix<T>().UpRightOf(row, column);
    }

    public T DownLeftOf<T>(int row, int column)
    {
        return GetMatrix<T>().DownLeftOf(row, column);
    }

    public T DownRightOf<T>(int row, int column)
    {
        return GetMatrix<T>().DownRightOf(row, column);
    }
    public T At<T>(MatrixIndex index)
    {
        return At<T>(index.Row, index.Column);
    }

    public T LeftOf<T>(MatrixIndex index)
    {
        return LeftOf<T>(index.Row, index.Column);
    }

    public T RightOf<T>(MatrixIndex index)
    {
        return RightOf<T>(index.Row, index.Column);
    }

    public T UpOf<T>(MatrixIndex index)
    {
        return UpOf<T>(index.Row, index.Column);
    }

    public T DownOf<T>(MatrixIndex index)
    {
        return DownOf<T>(index.Row, index.Column);
    }

    public T UpLeftOf<T>(MatrixIndex index)
    {
        return UpLeftOf<T>(index.Row, index.Column);
    }

    public T UpRightOf<T>(MatrixIndex index)
    {
        return UpRightOf<T>(index.Row, index.Column);
    }

    public T DownLeftOf<T>(MatrixIndex index)
    {
        return DownLeftOf<T>(index.Row, index.Column);
    }

    public T DownRightOf<T>(MatrixIndex index)
    {
        return DownRightOf<T>(index.Row, index.Column);
    }
}
