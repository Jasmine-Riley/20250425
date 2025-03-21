using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : Singleton<GameManager>
{
    private UIManager uiManager;
    private Volume volume;

    protected override void DoAwake()
    {
        AssignManagers();
        InitManagers();
    }

    private void AssignManagers()
    {
        GameObject.Find("UIManager").TryGetComponent<UIManager>(out uiManager);

        GameObject.Find("Global Volume").TryGetComponent<Volume>(out volume);
    }

    private void InitManagers()
    {
        uiManager?.Init();
    }

    public void Blur(bool tf)
    {
        volume.enabled = tf;
    }
}
