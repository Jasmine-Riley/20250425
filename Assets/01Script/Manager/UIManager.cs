using System;
using UnityEngine;
using UnityEngine.UI;


public enum ButtonType
{
    Slot1,
    Slot2,
}

public class UIManager : Singleton<UIManager> 
{
    private GameObject mail;
    private GameObject chapter;
    private GameObject chapterContent;
    private GameObject mission;

    private Camera uiCamera;

    public bool touchBlocking = false;

    public static event Action OnPressBtnSlot1;
    public static event Action OnPressBtnSlot2;

    public void Init()
    {
        var ui = GameObject.Find("UI");

        GameObject.Find("UI Camera").TryGetComponent<Camera>(out uiCamera);

        int num = 0;

        mail = ui.transform.GetChild(num++).gameObject;
        chapter = ui.transform.GetChild(num++).gameObject;
        chapterContent = chapter.GetComponentInChildren<ContentSizeFitter>().gameObject;
        
        mission = ui.transform.GetChild(num++).gameObject;

        GameObject.Find("BtnSlot1").TryGetComponent<Button>(out var btn);
        btn.onClick.AddListener(()=>HandleButtonClick(ButtonType.Slot1));

        GameObject.Find("BtnSlot2").TryGetComponent<Button>(out btn);
        btn.onClick.AddListener(() => HandleButtonClick(ButtonType.Slot2));

    }

    private void HandleButtonClick(ButtonType type)
    {
        Debug.Log("��ưŬ����" + type.ToString());
        switch(type)
        {
            case ButtonType.Slot1:
                OnPressBtnSlot1?.Invoke();
                break;
            case ButtonType.Slot2:
                OnPressBtnSlot2?.Invoke();
                break;
        }
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
