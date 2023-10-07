using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    //Assets/_Assets/CSVFolder/Level1/Map1.txt
    public Dictionary<string, List<List<string>>> _mapData = new Dictionary<string, List<List<string>>>();
    [SerializeField] int[] _mapCurrent;
    public int level;
    public LevelMatrix levelMatrix;
    public bool _finishLevel;
    public void AddLevel()
    {
        var log = new StringBuilder();
        for (var i = 0; i < _mapCurrent.Length; i++)
        {
            string path = GetPath(i);
            if (!_mapData.ContainsKey(path))
            {
                //Debug.Log(path);
                var mapMatrix = levelMatrix.GetMatrixOnTextFile(path);
                _mapData.Add(path, mapMatrix);
            }
        }
        //Debug.Log(log);
    }

    private string GetPath(int i)
    {
        return $"Assets/_Assets/CSVFolder/Level{level}/Map{_mapCurrent[i]}.txt";
    }
    public List<List<string>> GetCurrentLevel(int currentMap)
    {
        List<List<string>> Matrix = new List<List<string>>();
        for (int i = 0; i < _mapCurrent.Length; i++)
        {
            if (_mapCurrent[i] == currentMap)
            {
                var path = GetPath(i);
                Matrix = GetCurrentMap(path);
            }
        }
        return Matrix;
    }
    public List<List<string>> GetCurrentMap(string key)
    {
        List<List<string>> matrix = _mapData[key];

        return matrix;
    }
}
[System.Serializable]
public class LevelMatrix
{
    public List<List<string>> levelMatrix;
    public List<List<string>> GetMatrixOnTextFile(string path)
    {
        levelMatrix = new List<List<string>>();
        var loadFile = File.ReadAllText(path);
        var splitFile = loadFile.ToCharArray();
        var columMaxtrix = new List<string>();
        for (int i = 0; i < splitFile.Length; i++)
        {
            if (char.IsNumber(splitFile[i]))
            {
                columMaxtrix.Add(splitFile[i].ToString());
            }
            else if (splitFile[i] == ',')
            {
                levelMatrix.Add(columMaxtrix);
                columMaxtrix = new List<string>();
            }
        }
        return levelMatrix;
    }
    public void LogMatrix()
    {
        var log = new StringBuilder();
        foreach (var test1 in levelMatrix)
        {
            foreach (var test2 in test1)
            {
                log.AppendLine(test2);
            }
        }
        //Debug.Log(log);
    }
}