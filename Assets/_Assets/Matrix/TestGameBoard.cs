using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestGameBoard : GameBoard<Transform, ElementType, ObjectData>
{
    #region private
    private const int NONE_EDGE_WEIGHT = 10000;
    private const int NEIGHBOR_EDGE_WEIGHT = 1;
    DijkstraAlgorithm graph;
    WaitLineController _waitQueue;
    [SerializeField]
    private List<int> lastLine = new List<int> { 63, 64, 65, 66, 67, 68, 69, 70, 71 };
    #endregion
    #region property
    public WaitLineController WaitQueue
    {
        get
        {
            if (!_waitQueue)
                _waitQueue = FindObjectOfType<WaitLineController>();
            return _waitQueue;
        }
    }
    #endregion
    private void Awake()
    {
        graph = new DijkstraAlgorithm(LenghtGraph());
        CreateDic();
    }
    private void Start()
    {
        InitGraph();
    }
    bool IsAbleToGetToTheLastLine(List<int> reachableDestinations)
    {
        if (reachableDestinations == null || reachableDestinations.Count == 0) return false;
        foreach (var destination in reachableDestinations)
        {
            if (lastLine.Contains(destination))
                return true;
        }
        return false;
    }
    [SerializeField]
    List<int> _path;
    [SerializeField]
    List<int> _reachableDestinations;
    [SerializeField]
    int nearestDestination;

    public List<int> FindShortestPath(int source)
    {
        var distances = graph.FindShortestPath(source);
        var reachableDestinations = new List<int>();
        var label = 0;
        nearestDestination = 0;
        var min = int.MaxValue;
        foreach (var distance in distances)
        {
            if (!(distance == 0 || distance >= NONE_EDGE_WEIGHT))
            {
                reachableDestinations.Add(label);
                if (lastLine.Contains(label))
                {
                    if (distance <= min)
                    {
                        nearestDestination = label;
                        min = distance;
                    }
                }
            }
            label++;
        }
        //Debug.Log($"source:{source} minlabel: {nearestDestination}");
        //Log(path);
        _reachableDestinations = reachableDestinations;
        return reachableDestinations;
    }
    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(1f);
    }
    public List<int> GetPrevious(int source, int destination)
    {
        var GetPrevious = graph.GetShortestPath(source, destination);
        _path = GetPrevious;
        return GetPrevious;
    }

    public void SetStateObjOnMatrix(ObjectData obj)
    {
        obj.SetIndexElementType(ElementType.None, null);
        StartCoroutine(FindPaths(obj));
    }

    private IEnumerator FindPaths(ObjectData obj)
    {
        InitGraph();
        var label = GetVerticeLabel(obj.Index);
        FindShortestPath(label);
        GetPrevious(label, nearestDestination);
        for (int i = 0; i < _path.Count; i++)
        {
            var getIndex = GetMatrixIndex(_path[i]);
            var trans = At<Transform>(getIndex);
            obj.transform.position = trans.position;
            ElementMatrix.Set(getIndex, obj);
            yield return new WaitForSeconds(0.5f);
            if (_path[i] > 63)
            {
                WaitQueue.Add(obj);
            }
        }
    }
    private void ShowStateObj()
    {
        for (int source = 0; source < LenghtGraph() - lastLine.Count; source++)
        {
            var selectable = IsAbleToGetToTheLastLine(FindShortestPath(source));
            var objectData = At<ObjectData>(source);
            objectData.SetState(selectable);
        }
    }

    private void UpdateGraph(int row, int column)
    {
        ElementType elementTypeUp, elementTypeDown, elementTypeLeft, elementTypeRight;
        GetTypeVerticeIndex(row, column, out elementTypeUp, out elementTypeDown, out elementTypeLeft, out elementTypeRight);
        var element = At<ObjectData>(row, column);
        int source = GetVerticeLabel(element.Index);
        if (element.Up)
        {
            var destination = GetVerticeLabel(element.Up.Index);
            AddEdge(source, destination, elementTypeUp, graph);
        }
        if (element.Down)
        {
            var destination = GetVerticeLabel(element.Down.Index);
            AddEdge(source, destination, elementTypeDown, graph);
        }
        if (element.Left)
        {
            var destination = GetVerticeLabel(element.Left.Index);
            AddEdge(source, destination, elementTypeLeft, graph);
        }
        if (element.Right)
        {
            var destination = GetVerticeLabel(element.Right.Index);
            AddEdge(source, destination, elementTypeRight, graph);
        }
    }
    private DijkstraAlgorithm InitGraph()
    {
        for (var i = 0; i < Row; i++)
            for (var j = 0; j < Column; j++)
            {
                UpdateGraph(i, j);
            }
        ShowStateObj();
        return graph;
    }
    public void Log(IEnumerable<int> list)
    {
        var log = new StringBuilder();
        for (var i = 0; i < list.Count(); i++)
        {
            log.AppendLine($"{list.ElementAt(i)}");
        }
        Debug.Log(log);
    }
    private void GetTypeVerticeIndex(int i, int j, out ElementType elementTypeUp, out ElementType elementTypeDown, out ElementType elementTypeLeft, out ElementType elementTypeRight)
    {
        elementTypeUp = UpOf<ElementType>(i, j);
        elementTypeDown = DownOf<ElementType>(i, j);
        elementTypeLeft = LeftOf<ElementType>(i, j);
        elementTypeRight = RightOf<ElementType>(i, j);
    }
    private void AddEdge(int source, int destination, ElementType destinationType, DijkstraAlgorithm graph)
    {
        if (destinationType == ElementType.None || destinationType == ElementType.Bottom)
            graph.AddEdge(source, destination, NEIGHBOR_EDGE_WEIGHT);
        else
            graph.AddEdge(source, destination, NONE_EDGE_WEIGHT);
    }
    private int GetVerticeLabel(MatrixIndex matrixIndex)
    {
        return matrixIndex.Row * Column + matrixIndex.Column;
    }
    private int LenghtGraph()
    {
        return Row * Column;
    }
    private MatrixIndex GetMatrixIndex(int verticeLabel)
    {
        //Debug.Log($"{Column}...{Row}");
        int row = verticeLabel / Column;
        int column = verticeLabel % Column;
        return new MatrixIndex(row, column);
    }
    private T At<T>(int lable)
    {
        var Index = GetMatrixIndex(lable);
        return At<T>(Index);
    }
}
