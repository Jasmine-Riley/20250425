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
        Debug.Log("Å»¶ô");
    }
}
