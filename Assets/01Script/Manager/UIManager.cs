using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ButtonType
{
    Slot1,
    //Slot2,
}

public class UIManager : Singleton<UIManager> 
{

    private GameObject mail;

    private GameObject chapter;
    private GameObject chapterContent;

    private GameObject mission;

    private GameObject chat;
    private Image chatterImage;
    private TextMeshProUGUI chatterNameText;
    private TextMeshProUGUI chatText;


    private Camera uiCamera;

    public bool touchBlocking = false;

    public static event Action OnPressBtnSlot1;

    private GameObject obj;


    public bool useTouchPad = false;

    public void Init()
    {
        var ui = GameObject.Find("UI");
        var uiCam = GameObject.Find("UI Camera");

        if (!ui) return;

        uiCam.TryGetComponent<Camera>(out uiCamera);

        int num = 0;

        mail = ui.transform.GetChild(num++).gameObject;
        chapter = ui.transform.GetChild(num++).gameObject;
        if (chapter.transform.GetChild(1).TryGetComponent<Button>(out var closeBtn))
            closeBtn.onClick.AddListener(CloseChapter);

        chapterContent = chapter.GetComponentInChildren<ContentSizeFitter>().gameObject;
        
        mission = ui.transform.GetChild(num++).gameObject;

        chat = ui.transform.GetChild(num++).gameObject;
        chat.transform.GetChild(0).TryGetComponent<Image>(out  chatterImage);
        chat.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out chatterNameText);
        chat.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out chatText);
        //if (chat.transform.GetChild(3).TryGetComponent<Button>(out var skipBtn))
        //    skipBtn.onClick.AddListener(ScenarioManager.Instance.StopStory);

        if (!useTouchPad) return;

        obj = GameObject.Find("BtnSlot1");
        obj.TryGetComponent<Button>(out var btn);
        btn.onClick.AddListener(()=>HandleButtonClick(ButtonType.Slot1));

        //obj = GameObject.Find("BtnSlot2");
        //obj.TryGetComponent<Button>(out btn);
        //btn.onClick.AddListener(() => HandleButtonClick(ButtonType.Slot2));

    }

    private void HandleButtonClick(ButtonType type)
    {
        switch(type)
        {
            case ButtonType.Slot1:
                OnPressBtnSlot1?.Invoke();
                break;
            //case ButtonType.Slot2:
            //    OnPressBtnSlot2?.Invoke();
            //    break;
        }
    }

    public void OpenMail()
    {
        TouchBlock(true);

        mail.transform.localScale = Vector3.zero;
        LeanTween.moveLocal(mail, new Vector3(394f, 173f, 0), 0f);


        LeanTween.moveLocal(mail, new Vector3(0, 0, 0), 0.3f);
        LeanTween.scale(mail, new Vector3(1, 1, 1), 0.3f);

        GameManager.Instance.Blur(true);
        mail.SetActive(true);
        uiCamera.enabled = true;
    }

    public void CloseMail()
    {
        TouchBlock(false);

        LeanTween.moveLocal(mail, new Vector3(394f, 173f, 0), 0.3f);
        LeanTween.scale(mail, new Vector3(0, 0, 0), 0.3f);
        //mail.SetActive(false);

        //GameManager.Instance.Blur(false);
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

        LeanTween.scale(mission, Vector3.zero, 0);
        LeanTween.scale(mission, Vector3.one, 0.2f);
        //LeanTween.move(mission, Vector3.one, 1);
        uiCamera.enabled = true;
    }

    public void CloseMission()
    {
        TouchBlock(false);

        GameManager.Instance.Blur(false);
        LeanTween.scale(mission, Vector3.zero, 0.2f);

        //mission.SetActive(false);
        Invoke("UICameraShutDown", 0.2f);
    }

    public void OpenScenarioPannel()
    {
        TouchBlock(true);

        chat.SetActive(true);
    }

    public void CloseScenarioPannel()
    {
        TouchBlock(false);

        chat.SetActive(false);
    }

    public void SetScenarioPannel(Sprite sprite, string name, string chat)
    {
        if (sprite != null) 
        {
            chatterImage.sprite = sprite;

            chatterImage.transform.localScale = new Vector3(sprite.bounds.size.x * 0.3f, sprite.bounds.size.y * 0.3f, 0);
            chatterImage.enabled = true;
        }
        else
            chatterImage.enabled = false;

        chatterNameText.text = name;
        chatText.text = chat;
    }

    private void TouchBlock(bool tf)
    {
        touchBlocking = tf;
    }

    private void UICameraShutDown()
    {
        uiCamera.enabled = false;
    }
}
