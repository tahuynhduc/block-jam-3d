using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] float _countTime;
    [SerializeField] LifeData _lifeData;
    [SerializeField] LifeUI _lifeUI;
    private void Awake()
    {
        _lifeData.SaveData();
        _lifeUI = FindObjectOfType<LifeUI>();
    }
    private void OnEnable()
    {
        _lifeData.LoadData();
    }
    private void Update()
    {
        _lifeUI.ShowLifeText(_lifeData._lifeUser);
        if (_lifeData.maxLife == _lifeData._lifeUser)
            return;
        _countTime -= Time.deltaTime;
        if (_countTime <= 0 && _lifeData.maxLife > _lifeData._lifeUser)
        {
            var life = _lifeData.RewardLifeToUser();
            _lifeUI.ShowLifeText(life);
            _countTime = _lifeData.time;
            _lifeData.SaveData();
        }
    }
    public void LoadGamePlay()
    {
        _lifeData.MinusHeart();
    }

}
[System.Serializable]
public class LifeData
{
    public int _lifeUser;
    public int time;
    public int maxLife;
    public int minLife;
    public int RewardLifeToUser()
    {
        return _lifeUser += 1;
    }
    public void MinusHeart()
    {
        _lifeUser -= 1;
        SaveData();
    }
    public void SaveData()
    {
        //QuickSaveWriter writer = QuickSaveWriter.Create("LifeData");
        //writer.Write("LifeUser", _lifeUser);
        //writer.Write("time", time);
        //writer.Write("maxLife", maxLife);
        //writer.Write("minLife", minLife);
        //writer.Commit();
    }
    public void LoadData()
    {
        //QuickSaveReader reader = QuickSaveReader.Create("LifeData");
        //_lifeUser = reader.Read<int>("LifeUser");
        //time = reader.Read<int>("time");
        //maxLife = reader.Read<int>("maxLife");
        //minLife = reader.Read<int>("minLife");
    }
}