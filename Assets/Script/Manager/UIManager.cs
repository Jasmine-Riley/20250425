using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> 
{ 
    private GameObject mail;
    private Camera uiCamera;
    public void Init()
    {
        var canvas = GameObject.Find("Canvas");

        GameObject.Find("UI Camera").TryGetComponent<Camera>(out uiCamera);

        mail = canvas.transform.GetChild(0).gameObject;
    }

    public void PopUpMail()
    {
        GameManager.Instance.Blur(true);
        mail.SetActive(true);
        uiCamera.enabled = true;
    }

    public void CloseMail()
    {
        GameManager.Instance.Blur(false);
        mail.SetActive(false);
        uiCamera.enabled = false;
    }
}
