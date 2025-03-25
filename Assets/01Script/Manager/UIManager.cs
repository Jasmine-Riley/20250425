using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> 
{
    private GameObject mail;
    private GameObject chapter;
    private GameObject chapterContent;
    private GameObject mission;

    private Camera uiCamera;

    public bool touchBlocking = false;
    public void Init()
    {
        var canvas = GameObject.Find("Canvas");

        GameObject.Find("UI Camera").TryGetComponent<Camera>(out uiCamera);

        int num = 0;

        mail = canvas.transform.GetChild(num++).gameObject;
        chapter = canvas.transform.GetChild(num++).gameObject;
        mission = canvas.transform.GetChild(num++).gameObject;

        chapterContent = chapter.GetComponentInChildren<ContentSizeFitter>().gameObject;
    }

    public void OpenMail()
    {
        TouchBlock(true);

        GameManager.Instance.Blur(true);
        mail.SetActive(true);
        uiCamera.enabled = true;
    }

    public void CloseMail()
    {
        TouchBlock(false);

        //GameManager.Instance.Blur(false);
        mail.SetActive(false);
        //uiCamera.enabled = false;

        //OpenMission();
    }

    public void OpenChapter()
    {
        TouchBlock(true);

        chapter.GetComponentInChildren<ScrollRect>().horizontal = chapterContent.transform.childCount > 3;

        GameManager.Instance.Blur(true);
        chapter.SetActive(true);
        uiCamera.enabled = true;
    }

    public void CloseChapter()
    {
        TouchBlock(false);

        GameManager.Instance.Blur(false);
        chapter.SetActive(false);
        uiCamera.enabled = false;
    }

    public void OpenMission()
    {
        TouchBlock(true);

        GameManager.Instance.Blur(true);
        mission.SetActive(true);
        uiCamera.enabled = true;
    }

    public void CloseMission()
    {
        TouchBlock(false);

        GameManager.Instance.Blur(false);
        mission.SetActive(false);
        uiCamera.enabled = false;
    }

    private void TouchBlock(bool tf)
    {
        touchBlocking = tf;
    }
}
