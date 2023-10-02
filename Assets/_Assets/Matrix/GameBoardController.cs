using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class GameBoardController : GameBoard<Transform, ElementType, ObjectData>
{
    #region private
    private const int NONE_EDGE_WEIGHT = 10000;
    private const int NEIGHBOR_EDGE_WEIGHT = 1;
    DijkstraAlgorithm _graph;
    WaitLineController _waitQueue;
    List<int> lastLine;
    int _countObj;
    [SerializeField] int nearestDestination;
    [SerializeField] UnityEvent _unityEvent;
    [SerializeField] List<ObjectData> _restoreObjPosition;
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
        _graph = new DijkstraAlgorithm(LenghtGraph());
        CreateDic();
        InstanLastLine();
        InitGraph();
    }
    public void SetStateObjOnMatrix(ObjectData obj, bool objInQueueSecond)
    {
        obj.ClickObjOnEnable(false);
        obj.SetIndexElementType(ElementType.None);
        if (objInQueueSecond)
        {
            _unityEvent?.Invoke();
            WaitQueue.Add(obj);
            return;
        }
        _restoreObjPosition.Add(obj);
        CheckWinGame();
        StartCoroutine(FindPaths(obj));
    }

    #region Logic Game
    private void CheckWinGame()
    {
        _countObj++;
        if (_countObj >= 18)
        {
            Debug.Log("you win");
        }
    }
    #endregion
    #region Item gameplay
    [SerializeField] List<ObjectData> lengthObj;
    public void ResetObjPosition()
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {
                var obj = At<ObjectData>(i, j);
                lengthObj.Add(obj);
            }
        }
    }
    public void RevertPositionObj()
    {
        IsMathchingObj();
        var state = WaitQueue.WaitQueueSeconIsOnEnable();
        if (state)
            return;
        _graph = new DijkstraAlgorithm(LenghtGraph());
        var objectData = _restoreObjPosition[_restoreObjPosition.Count - 1];
        objectData.SetIndexElementType(objectData.Type);
        var transMatrix = At<Transform>(objectData.Index);
        objectData.SetTranform(transMatrix);
        objectData.ClickObjOnEnable(true);
        _restoreObjPosition.Remove(objectData);
        WaitQueue.UpdatePositionInWaitQueue(objectData);
        InitGraph();
    }
    public void IsMathchingObj()
    {
        for (int i = 0; i < _restoreObjPosition.Count; i++)
        {
            if (_restoreObjPosition[i].isMatching)
            {
                _restoreObjPosition.Remove(_restoreObjPosition[i]);
            }
        }
    }
    public void CreateMoreQueue(bool state)
    {
        WaitQueue.MoveObjToSecondQueue(state);
    }
    #endregion 
    bool IsAbleToGetToTheLastLine(List<int> reachableDestinations)
    {
        if (reachableDestinations == null || reachableDestinations.Count == 0) return false;
        foreach (var destination in reachableDestinations)
            if (lastLine.Contains(destination))
                return true;
        return false;
    }
    [SerializeField] int[] distance;
    public List<int> FindShortestPath(int source)
    {
        var distances = _graph.FindShortestPath(source);
        distance = distances;
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
                    if (distance <= min)
                    {
                        nearestDestination = label;
                        min = distance;
                    }
            }
            label++;
        }
        return reachableDestinations;
    }
    public List<int> GetPrevious(int source, int destination)
    {
        var GetPrevious = _graph.GetShortestPath(source, destination);
        return GetPrevious;
    }

    private IEnumerator FindPaths(ObjectData obj)
    {
        InitGraph();
        var label = GetVerticeLabel(obj.Index);
        FindShortestPath(label);
        var path = GetPrevious(label, nearestDestination);
        for (int i = 0; i < path.Count; i++)
        {
            var getIndex = GetMatrixIndex(path[i]);
            var trans = At<Transform>(getIndex);
            obj.transform.position = trans.position;
            yield return new WaitForSeconds(0.1f);
        }
        WaitQueue.Add(obj);
    }
    private void ShowStateObj()
    {
        for (int source = 0; source < LenghtGraph() - lastLine.Count; source++)
        {
            var reachableDestinations = FindShortestPath(source);
            var selectable = IsAbleToGetToTheLastLine(reachableDestinations);
            var objectData = At<ObjectData>(source);
            objectData.SetState(selectable);
        }
    }
    #region instan Graph
    private void InstanLastLine()
    {
        lastLine = new List<int>(Column);
        for (int i = 0; i < Column; i++)
            lastLine.Add(LenghtGraph() - 1 - i);
        lastLine.Reverse();
    }

    private DijkstraAlgorithm InitGraph()
    {
        for (var i = 0; i < Row; i++)
            for (var j = 0; j < Column; j++)
                UpdateGraph(i, j);
        ShowStateObj();
        return _graph;
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
            AddEdge(source, destination, elementTypeUp, _graph);
        }
        if (element.Down)
        {
            var destination = GetVerticeLabel(element.Down.Index);
            AddEdge(source, destination, elementTypeDown, _graph);
        }
        if (element.Left)
        {
            var destination = GetVerticeLabel(element.Left.Index);
            AddEdge(source, destination, elementTypeLeft, _graph);
        }
        if (element.Right)
        {
            var destination = GetVerticeLabel(element.Right.Index);
            AddEdge(source, destination, elementTypeRight, _graph);
        }
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
        if (destinationType == ElementType.None || destinationType == ElementType.GROUND)
            graph.AddEdge(source, destination, NEIGHBOR_EDGE_WEIGHT);
        else
            graph.AddEdge(source, destination, NONE_EDGE_WEIGHT);
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

    #endregion
    private int GetVerticeLabel(MatrixIndex matrixIndex)
    {
        return matrixIndex.Row * Column + matrixIndex.Column;
    }
    private int LenghtGraph()
    {
        return Row * Column;
    }
    public MatrixIndex GetMatrixIndex(int verticeLabel)
    {
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
