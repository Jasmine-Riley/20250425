using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : Singleton<GameManager>
{
    private UIManager uiManager;
    private PoolManager poolManager;
    private Volume volume;

    private GameObject player;
    public GameObject Player { get => player; }

    protected override void DoAwake()
    {
        AssignManagers();
        InitManagers();

        player = GameObject.Find("Player");

        player.TryGetComponent<PlayerController>(out var p);
        p.Init();
    }

    private void AssignManagers()
    {
        GameObject.Find("UIManager")?.TryGetComponent<UIManager>(out uiManager);
        GameObject.Find("PoolManager")?.TryGetComponent<PoolManager>(out poolManager);
        GameObject.Find("Global Volume")?.TryGetComponent<Volume>(out volume);
    }

    private void InitManagers()
    {
        uiManager?.Init();
        poolManager?.Init();
    }

    public void Blur(bool tf)
    {
        volume.enabled = tf;
    }
}
