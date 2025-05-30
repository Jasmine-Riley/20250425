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
    private Image bloodScreen;

    private GameObject mail;

    private GameObject chapter;
    private GameObject chapterContent;

    private GameObject mission;

    private GameObject chat;
    private Image chatterImage;
    private TextMeshProUGUI chatterNameText;
    private TextMeshProUGUI chatText;

    private GameObject status;
    private GameObject hpStatus;
    private TextMeshProUGUI timer;

    private GameObject playMenu;

    private GameObject star;

    private GameObject menuPopup;

    private GameObject popup;
    private TextMeshProUGUI popupTitleText;
    private TextMeshProUGUI popupText;
    private TextMeshProUGUI popupSubText;
    private Button popupOKBtn;
    private Button popupCancelBtn;


    private GameObject clearPopup;
    private TextMeshProUGUI clearPopupTitle;
    private GameObject clearPopupStar;
    private GameObject clearPopupSlot;



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

        GameObject.Find("BloodScreen").TryGetComponent<Image>(out bloodScreen);

        uiCam.TryGetComponent<Camera>(out uiCamera);

        int num = 0;
        Button btn;

        mail = ui.transform.GetChild(num++).gameObject;
        if (mail.transform.GetChild(4).TryGetComponent<Button>(out btn))
        {
            btn.onClick.AddListener(CloseMail);
            btn.onClick.AddListener(OpenMission);
        }

        chapter = ui.transform.GetChild(num++).gameObject;
        if (chapter.transform.GetChild(1).TryGetComponent<Button>(out btn))
            btn.onClick.AddListener(CloseChapter);

        chapterContent = chapter.GetComponentInChildren<ContentSizeFitter>().gameObject;
        
        mission = ui.transform.GetChild(num++).gameObject;
        if(mission.transform.GetChild(3).TryGetComponent<Button>(out btn))
            btn.onClick.AddListener(CloseMission);

        chat = ui.transform.GetChild(num++).gameObject;
        chat.transform.GetChild(0).TryGetComponent<Image>(out  chatterImage);
        chat.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out chatterNameText);
        chat.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out chatText);

        if (ScenarioManager.Instance)
        {
            if (chat.transform.GetChild(3).TryGetComponent<Button>(out btn))
                btn.onClick.AddListener(ScenarioManager.Instance.NextPage);

            if (chat.transform.GetChild(4).TryGetComponent<Button>(out btn))
                btn.onClick.AddListener(ScenarioManager.Instance.StopStory);
        }


        status = ui.transform.GetChild(num++).gameObject;
        hpStatus = status.transform.GetChild(0).gameObject;
        status.transform.GetChild(1).GetChild(0).TryGetComponent<TextMeshProUGUI>(out timer);

        playMenu = ui.transform.GetChild(num++).gameObject;
        if(playMenu.transform.GetChild(0).TryGetComponent<Button>(out btn))
            btn.onClick.AddListener(() => OpenMenuPopup());
        TwoStateButton twobtn;
        if(playMenu.transform.GetChild(1).TryGetComponent<TwoStateButton>(out twobtn))
            twobtn.OnClick += () => GameManager.Instance.MusicOnOFF(twobtn.onT_offF);
        if(playMenu.transform.GetChild(2).TryGetComponent<TwoStateButton>(out twobtn))
            twobtn.OnClick += () => GameManager.Instance.StopResume(twobtn.onT_offF);


        star = ui.transform.GetChild(num++).gameObject;

        menuPopup = ui.transform.GetChild(num++).gameObject;

        menuPopup.transform.GetChild(0).TryGetComponent<Button>(out btn);
        btn.onClick.AddListener(() => { 
            OpenPopup("다시 시작", "해당 스테이지를 다시 시작합니다.", "(주의: 모든 진행 상황이 초기화 됩니다.)"); 
            menuPopup.SetActive(false);
            popupOKBtn.onClick.RemoveAllListeners();
            popupOKBtn.onClick.AddListener(GameManager.Instance.RestartGame);
            popupCancelBtn.onClick.RemoveAllListeners();
            popupCancelBtn.onClick.AddListener(ClosePopup);
        }
        );
        menuPopup.transform.GetChild(1).TryGetComponent<Button>(out btn);
        btn.onClick.AddListener(GameManager.Instance.ReturnToLobby);

        popup = ui.transform.GetChild(num++).gameObject;
        popup.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out popupTitleText);
        popup.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out popupText);
        popup.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out popupSubText);
        popup.transform.GetChild(3).TryGetComponent<Button>(out popupOKBtn);
        popup.transform.GetChild(4).TryGetComponent<Button>(out popupCancelBtn);

        clearPopup = ui.transform.GetChild(num++).gameObject;

        clearPopup.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out clearPopupTitle);
        clearPopupStar = clearPopup.transform.GetChild(1).gameObject;
        clearPopupSlot = clearPopup.transform.GetChild(2).gameObject;

        clearPopupSlot.transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(GameManager.Instance.ReturnToLobby);
        var btns = clearPopupSlot.transform.GetChild(1).GetComponentsInChildren<Button>();

        btns[0].onClick.AddListener(GameManager.Instance.RestartGame);
        btns[1].onClick.AddListener(GameManager.Instance.ReturnToLobby);


        if (!useTouchPad) return;

        obj = GameObject.Find("BtnSlot1");
        obj.TryGetComponent<Button>(out btn);
        btn.onClick.AddListener(()=>HandleButtonClick(ButtonType.Slot1));

        //obj = GameObject.Find("BtnSlot2");
        //obj.TryGetComponent<Button>(out btn);
        //btn.onClick.AddListener(() => HandleButtonClick(ButtonType.Slot2));

        SetStar(3);
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

    public void SetHpUI(int n)
    {
        int i;
        for(i = 0; i < n; i++)
            hpStatus.transform.GetChild(i).gameObject.SetActive(true);

        for(; i < hpStatus.transform.childCount; i++)
            hpStatus.transform.GetChild(i).gameObject.SetActive(false);



        Image image;
        Color color;
        for (i = 0; i < n; i++)
        {
            clearPopupStar.transform.GetChild(i).TryGetComponent<Image>(out image);
            color = image.color;
            color.a = 1;
            image.color = color;
        }

        for (i = n; i < clearPopupStar.transform.childCount; i++)
        {
            clearPopupStar.transform.GetChild(i).TryGetComponent<Image>(out image);
            color = image.color;
            color.a = 33f / 255f;
            image.color = color;
        }
    }

    public void SetTimer(float time)
    {
        status.SetActive(true);
        status.transform.GetChild(1).gameObject.SetActive(true);

        timer.text = "";

        if (time < 10f)
            timer.text += "<color=red>";

        timer.text += $"{(int)time / 60}:{time % 60}";

        if (time < 10f)
            timer.text += "</color>";


        if (time < 1) status.SetActive(false);
    }

    public void SetStar(int count)
    {
        int n;
        Image image;
        Color color;
        for(n = 0; n < count; n++)
        {
            star.transform.GetChild(n).TryGetComponent<Image>(out image);
            color = image.color;
            color.a = 1;
            image.color = color;
        }

        for(n = count; n < star.transform.childCount; n++)
        {
            star.transform.GetChild(n).TryGetComponent<Image>(out image);
            color = image.color;
            color.a = 33f/255f;
            image.color = color;
        }
    }


    private void OpenMenuPopup()
    {
        menuPopup.SetActive(!menuPopup.activeSelf);
    }

    private void OpenPopup(string titleText, string text, string subText)
    {
        popupTitleText.text = titleText;
        popupText.text = text;
        popupSubText.text = subText;

        popup.SetActive(true);
    }

    private void ClosePopup()
    {
        popup.SetActive(false);
    }

    public void OpenClearPopup(bool tf)
    {
        if (tf)
        {
            clearPopupTitle.text = "VICTORY";
            clearPopupSlot.transform.GetChild(0).gameObject.SetActive(true);
            clearPopupSlot.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            clearPopupTitle.text = "FAIL";
            clearPopupSlot.transform.GetChild(0).gameObject.SetActive(false);
            clearPopupSlot.transform.GetChild(1).gameObject.SetActive(true);
        }


        clearPopup.SetActive(true);
    }

    private void TouchBlock(bool tf)
    {
        touchBlocking = tf;
    }

    private void UICameraShutDown()
    {
        uiCamera.enabled = false;
    }

    public void BloodScreen(float value)
    {
        var color = bloodScreen.color;
        color.a = value;
        bloodScreen.color = color;
    }
}
