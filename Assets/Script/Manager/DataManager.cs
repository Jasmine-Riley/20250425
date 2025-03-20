using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ������ ���´� SaveDatas.cs ����

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
        // '�Ϲ�' ���� �ҷ�����

        if(testSkip)
            if (!loadData) LoadData();
    }

    public bool LoadData()
    {
        loadData = false;

        // To do : ���� �ε�


        // ������ �Ϲ� �ε�

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
