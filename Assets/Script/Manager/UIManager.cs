using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> 
{ 
    private GameObject mail;
    private GameObject chapter;
    private GameObject chapterContent;

    private Camera uiCamera;
    public void Init()
    {
        var canvas = GameObject.Find("Canvas");

        GameObject.Find("UI Camera").TryGetComponent<Camera>(out uiCamera);

        mail = canvas.transform.GetChild(0).gameObject;
        chapter = canvas.transform.GetChild(1).gameObject;

        chapterContent = chapter.GetComponentInChildren<ContentSizeFitter>().gameObject;
    }

    public void OpenMail()
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

    public void OpenChapter()
    {
        chapter.GetComponentInChildren<ScrollRect>().horizontal = chapterContent.transform.childCount > 3;

        GameManager.Instance.Blur(true);
        chapter.SetActive(true);
        uiCamera.enabled = true;

    }

    public void CloseChapter()
    {
        GameManager.Instance.Blur(false);
        chapter.SetActive(false);
        uiCamera.enabled = false;
    }
}
