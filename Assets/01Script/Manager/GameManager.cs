using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private DataManager dataManager;
    private UIManager uiManager;
    private PoolManager poolManager;
    private Volume volume;

    private GameObject player;
    public GameObject Player { get => player; }

    public Action OnTimerStop;

    protected override void DoAwake()
    {
        //AssignManagers();
        //InitManagers();

        //player = GameObject.Find("Player");

        ////if(player.TryGetComponent<PlayerController>(out var p))
        ////    p.Init();

        SceneManager.sceneLoaded += (scene, mode) => {
            AssignManagers();
            InitManagers();
            player = GameObject.Find("Player");
            if (!Player) return;
            if(player.TryGetComponent<PlayerController>(out var p))
                p.Init();
        };
    }

    private void AssignManagers()
    {
        GameObject.Find("DataManager")?.TryGetComponent<DataManager>(out dataManager);
        GameObject.Find("UIManager")?.TryGetComponent<UIManager>(out uiManager);
        GameObject.Find("PoolManager")?.TryGetComponent<PoolManager>(out poolManager);
        GameObject.Find("Global Volume")?.TryGetComponent<Volume>(out volume);
    }

    private void InitManagers()
    {
        dataManager?.Init();
        uiManager?.Init();
        poolManager?.Init();
    }

    public void Blur(bool tf)
    {
        volume.enabled = tf;
    }

    public void GameOver()
    {
        StopTimer();
        UIManager.Instance.OpenClearPopup(false);
    }

    public void SetTimer(float second, Action action = null)
    {
        if(action != null)
            OnTimerStop += action;
        StartCoroutine("Timer", second);
    }

    public void StopTimer()
    {
        OnTimerStop = null;
        StopCoroutine("Timer");
        uiManager.SetTimer(-1);
    }

    private IEnumerator Timer(float second)
    {
        var timer = second;

        while(timer > 0)
        {
            yield return YieldInstructionCache.WaitForSeconds(1f);
            timer -= 1f;
            uiManager.SetTimer(timer);
        }
        OnTimerStop?.Invoke();
    }

    public void MusicOnOFF(bool tf)
    {

    }

    public void StopResume(bool tf)
    {
        if(!tf)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        LoadingSceneManager.SetNextScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("LoadingScene");
    }

    public void ReturnToLobby()
    {
        LoadingSceneManager.SetNextScene("LobbyScene");
        SceneManager.LoadScene("LoadingScene");
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
