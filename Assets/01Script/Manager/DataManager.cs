using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 데이터 형태는 SaveDatas.cs 참고

public class DataManager : Singleton<DataManager>, IManager
{
    private bool loadData = false;
    public bool isDataLoad => loadData;

    private string dataPath;


    private PlayerData playerData;
    public PlayerData PlayerData => playerData;

    public bool testSkip = false;

    public void InitManager()
    {
        // '일반' 계정 불러오기

        if(testSkip)
            if (!loadData) LoadData();
    }

    public bool LoadData()
    {
        loadData = false;

        // To do : 구글 로드


        // 없으면 일반 로드

        dataPath = Application.persistentDataPath + "/Save";
        if (File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath);
            playerData = JsonUtility.FromJson<PlayerData>(data);
            loadData = true;
            return loadData;
        }

        return loadData;
    }

    public void SaveData()
    {
        dataPath = Application.persistentDataPath + "/Save";

        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(dataPath, data);
        loadData = true;
    }

    public void DeleteData()
    {
        File.Delete(dataPath);
    }

    public void CreateData(string nickName)
    {
        playerData = new PlayerData();
        playerData.nickName = nickName;
    }
}
