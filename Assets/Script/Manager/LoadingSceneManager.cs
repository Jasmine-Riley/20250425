using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private Image tornado;


    private static string nextSceneName;
    private AsyncOperation asyncScene;

    private void Awake()
    {
        //tornado.gameObject.transform.localScale = Vector3.one;
        StartCoroutine("LoadSceneAsync");
    }

    private void Update()
    {
        tornado.transform.Rotate(0, 0, 1);
    }

    public static void SetNextScene(string sceneName)
    {
        nextSceneName = sceneName;
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(2.5f);
        asyncScene = SceneManager.LoadSceneAsync(nextSceneName);
        asyncScene.allowSceneActivation = false;

        var timeC = 0.0f;
        while(!asyncScene.isDone)
        {
            timeC += Time.deltaTime;

            if(asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                if (loadingBar.fillAmount > 0.99f)
                    SceneLoadEnd();
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if (loadingBar.fillAmount >= asyncScene.progress)
                    timeC = 0.0f;
            }
            yield return null;
        }
    }

    private void SceneLoadEnd()
    {
        StopCoroutine("LoadSceneAsync");
        StartCoroutine("Effect");
    }

    private IEnumerator Effect()
    {
        LeanTween.scale(tornado.gameObject, Vector3.zero, 1.5f);
        yield return YieldInstructionCache.WaitForSeconds(1.5f);
        asyncScene.allowSceneActivation = true;
    }
}
