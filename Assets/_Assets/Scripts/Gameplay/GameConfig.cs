using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Level Data")]
public class GameConfig : ScriptableObject
{
    [SerializeField] List<LevelData> _levelDatas;
    public void SetLevel()
    {
        foreach (var level in _levelDatas)
        {
            level.AddLevel();
        }
    }
    public List<List<string>> GetLevelCurrent(int levelst, int mapst)
    {
        List<List<string>> levelcurrent = new List<List<string>>();
        var i = 0;
        foreach (var level in _levelDatas)
        {
            if (levelst == _levelDatas[i].level)
            {
                levelcurrent = level.GetCurrentLevel(mapst);
                break;
            }
            i++;
        }
        var log = new StringBuilder();
        foreach (var test1 in levelcurrent)
        {
            foreach (var test2 in test1)
            {
                log.AppendLine(test2);
            }
        }
        Debug.Log(log);
        return levelcurrent;
    }
}