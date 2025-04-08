using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
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



    private ChatTable storyDatas;
    private Dictionary<int, ChatData> Story = new Dictionary<int, ChatData>();
    private Dictionary<int, ChatData> FindDevil = new Dictionary<int, ChatData>();



    public void Init()
    {
        // '일반' 계정 불러오기

        if (testSkip)
            if (!loadData)
                LoadData();

        if(Story.Count < 1)
            LoadStoryData();
    }


    #region ___LoadPlayer___

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

    #endregion

    #region ___LoadStorys___

    private void LoadStoryData()
    {
        storyDatas = Resources.Load<ChatTable>("ChatData/ChatTable");

        Debug.Log("스토리불러오기");
        for (int i = 0; i < storyDatas.Story.Count; i++)
            Story.Add(storyDatas.Story[i].id, storyDatas.Story[i]);

        for (int i = 0; i < storyDatas.FindDevil.Count; i++)
            FindDevil.Add(storyDatas.FindDevil[i].id, storyDatas.FindDevil[i]);
    }


    #endregion

    #region ___ReturnData___

    public Dictionary<int, ChatData> GetStoryData(StoryType type)
    {
        switch (type)
        {
            case StoryType.Story:
                return Story;
            case StoryType.FindDevil:
                return FindDevil;
        }

        return null;
    }

    #endregion
}
