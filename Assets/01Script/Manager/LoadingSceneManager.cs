using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private GameObject runner;
    private GameObject postBox;

    private Vector3 startPos;
    private Vector3 endPos;

    private RectTransform runnerRect;

    private static string nextSceneName;
    private AsyncOperation asyncScene;

    private void Awake()
    {
        runner = loadingBar.transform.GetChild(0).gameObject;
        postBox = loadingBar.transform.GetChild(1).gameObject;

        runnerRect = runner.GetComponent<RectTransform>();

        startPos = runnerRect.position;

        endPos = postBox.GetComponent<RectTransform>().position;
        startPos.y = endPos.y;

        StartCoroutine("LoadSceneAsync");
    }


    public static void SetNextScene(string sceneName)
    {
        nextSceneName = sceneName;
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(0.3f);
        asyncScene = SceneManager.LoadSceneAsync(nextSceneName);
        asyncScene.allowSceneActivation = false;
        var realTime = 0f;

        var timeC = 0.0f;
        while(!asyncScene.isDone)
        {
            timeC += Time.deltaTime * 0.01f;
            realTime += Time.deltaTime;

            if(asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);

                runnerRect.position = new Vector3(Mathf.Lerp(runnerRect.position.x, endPos.x, timeC), runnerRect.position.y, runnerRect.position.z);
                if (loadingBar.fillAmount > 0.99f)
                    SceneLoadEnd();
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                runnerRect.position = new Vector3(Mathf.Lerp(runnerRect.position.x, endPos.x, timeC), runnerRect.position.y, runnerRect.position.z);

                if (loadingBar.fillAmount >= asyncScene.progress)
                    timeC = 0.0f;
            }
            yield return null;
        }
    }

    private void SceneLoadEnd()
    {
        runnerRect.position = new Vector3(endPos.x + 10, runnerRect.position.y, runnerRect.position.z);
        StopCoroutine("LoadSceneAsync");
        StartCoroutine("Effect");
    }

    private IEnumerator Effect()
    {
        yield return YieldInstructionCache.WaitForSeconds(1f);
        asyncScene.allowSceneActivation = true;
    }
}
