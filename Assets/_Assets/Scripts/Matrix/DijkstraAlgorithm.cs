using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class DijkstraAlgorithm
{
    private readonly int _verticesCount;
    private readonly List<List<Tuple<int, int>>> _vertices;

    public DijkstraAlgorithm(int verticesCount)
    {
        _verticesCount = verticesCount;
        _vertices = new List<List<Tuple<int, int>>>();

        for (int i = 0; i < verticesCount; ++i)
        {
            _vertices.Add(new List<Tuple<int, int>>());
        }
    }

    public void AddEdge(int source, int destination, int weight)
    {
        _vertices[source].Add(Tuple.Create(destination, weight));
        //_vertices[destination].Add(Tuple.Create(source, weight)); // For undirected graphs
    }
    public void LogVertices()
    {
        var log = new StringBuilder();
        for (int i = 0; i < _vertices.Count; i++)
        {
            log.AppendLine($"\nsource: {i}:\nedges: {_vertices[i].Count}\n");
            foreach (var edge in _vertices[i])
            {
                log.AppendLine($"-> destination {edge.Item1} - weight: {edge.Item2}");
            }
        }
        Debug.Log(log.ToString());
    }
    public int[] Previous;
    public List<int> GetShortestPath(int source, int destination)
    {
        var path = new List<int>();
        int current = destination;
        while (current != source)
        {
            path.Add(current);
            current = Previous[current];
        }
        path.Add(source);
        path.Reverse(); // Bởi vì chúng ta đã thêm vào từ điểm đến trở về nguồn, cần đảo ngược lại để có đúng thứ tự
        return path;
    }
    public int[] FindShortestPath(int source)
    {

        PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();
        int[] distances = Enumerable.Repeat(int.MaxValue, _verticesCount).ToArray();
        int[] previous = Enumerable.Repeat(source, _verticesCount).ToArray();

        priorityQueue.Enqueue(source, 0);
        distances[source] = 0;

        while (!priorityQueue.IsEmpty)
        {
            int currentVertex = priorityQueue.Dequeue();
            foreach (var edge in _vertices[currentVertex])
            {
                int destination = edge.Item1;
                int weight = edge.Item2;
                if (distances[destination] > distances[currentVertex] + weight)
                {
                    distances[destination] = distances[currentVertex] + weight;
                    previous[destination] = currentVertex;
                    priorityQueue.Enqueue(destination, distances[destination]);
                }
            }
        }
        Previous = previous;
        return distances; // Returns shortest distances from source to all other vertices
    }
}