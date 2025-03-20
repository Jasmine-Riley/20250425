using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private GameObject loginPopup;
    [SerializeField] private GameObject createAccountPopup;
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI popupText;

    private bool hasPlayerInformation = false;

    private string nickName;

    private bool touchEnable = true;

    private Action func;

    private void Start()
    {
        SetPopupsScaleZero();

        DataManager.Instance.InitManager();

        InitTitleScene();
    }

    private void SetPopupsScaleZero()
    {
        var vectorZero = Vector3.zero;
        loginPopup.transform.localScale = vectorZero;
        createAccountPopup.transform.localScale = vectorZero;
        popup.transform.localScale = vectorZero;

        loginPopup.SetActive(false);
        createAccountPopup.SetActive(false);
        popup.SetActive(false);
    }

    private void InitTitleScene()
    {
        // 데이터 유무 확인
        hasPlayerInformation = DataManager.Instance.isDataLoad;

        var vectorZero = Vector3.zero;

        LeanTween.scale(loginPopup, vectorZero, 0.45f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(createAccountPopup, vectorZero, 0.45f).setEase(LeanTweenType.easeOutElastic);
        //LeanTween.scale(popup, vectorZero, 0.7f).setEase(LeanTweenType.easeOutElastic);


        welcomeText.text = hasPlayerInformation ? "시작하려면 터치하세요" : "로그인하시려면 터치하세요";
        welcomeText.enabled = true;

    }

    public void TouchBtn()
    {
        if (!touchEnable) return;


        if (hasPlayerInformation) // 로그인되어있으면
        {
            LoadingSceneManager.SetNextScene("LobbyScene");
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            if (loginPopup.transform.localScale == Vector3.zero)
            {
                loginPopup.SetActive(true);
                welcomeText.enabled = false;
                LeanTween.scale(loginPopup, Vector3.one, 0.45f).setEase(LeanTweenType.easeOutElastic);
            }
            else
            {
                LeanTween.scale(loginPopup, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutElastic);
                LeanTween.scale(createAccountPopup, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutElastic);
                welcomeText.enabled = true;

                Invoke("SetPopupsScaleZero", 0.3f);
            }
        }
    }


    #region _LoginPopup_ 

    public void GoogleLoginBtn()
    {
        GoogleLoginProcess();
    }

    public void CreateAccountBtn()
    {
        createAccountPopup.SetActive(true);
        LeanTween.scale(createAccountPopup, Vector3.one, 0.45f).setEase(LeanTweenType.easeOutElastic);
    }

    private void GoogleLoginProcess()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    private void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            hasPlayerInformation = true;
            func += InitTitleScene;
            Popup($"구글 로그인 성공" + name);
        }
        else
        {
            hasPlayerInformation = false;
            func += InitTitleScene;
            Popup($"구글 로그인 실패");
        }
    }

    #endregion

    #region _CreateAccountPopup_ 

    public void ApplyAccountBtn()
    {
        if (!CheckAccountCondition()) return;

        DataManager.Instance.CreateData(nickName);
        DataManager.Instance.SaveData();

        Popup($"계정 생성에 성공하였습니다!\n{nickName}님!");

        func += InitTitleScene;
    }

    private bool CheckAccountCondition()
    {
        bool canApply = true;
        string popupText = "";
        if (nickName == null || nickName.Length < 2 || nickName.Length > 10)
        {
            popupText += "닉네임은 2글자에서 \n10자 이내여야만 합니다!\n";
            canApply = false;
        }

        if(!canApply) Popup(popupText);

        return canApply;
    }

    public void OnChangeNickNameInputField(string input)
    {
        nickName = input;
    }

    #endregion


    #region _Popup_ 


    private void Popup(string txt)
    {
        touchEnable = false;
        popupText.text = txt;
        popup.SetActive(true);
        LeanTween.scale(popup, Vector3.one, 0.7f).setEase(LeanTweenType.easeOutElastic);
    }

    public void OkayBtn()
    {
        touchEnable = true;
        LeanTween.scale(popup, Vector3.zero, 0.3f).setEase(LeanTweenType.easeOutElastic);

        func?.Invoke();
        func = null;
    }

    #endregion

}
