using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    private GameObject log;
    [SerializeField] private Vector3 spawnPosition;
    private float endTime;
    private float logSpeed = 2.5f;


    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        StartCoroutine("Spawn");
    }

    private void StopSpawn()
    {
        StopCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        endTime = Time.time + 60;
        GameManager.Instance.SetTimer(60, () => UIManager.Instance.OpenClearPopup(true));
        //var player = GameManager.Instance.Player;
        while (Time.time < endTime)
        {
            log = PoolManager.Instance.enemyPool.GetPoolObject();
            log.transform.rotation = Quaternion.Euler(0, 0, -90);
            log.transform.position = spawnPosition;
            if (log.TryGetComponent<LogMove>(out var logComponent))
                logComponent.Init(logSpeed);

            logSpeed += Random.Range(1, 4f);

            var after = 2.5f - logSpeed * 0.05f;
            after = after < 1.5f ? 1.5f : after;

            //Debug.Log(after + "초 뒤");
            yield return YieldInstructionCache.WaitForSeconds(after);
        }

        //Debug.Log("소환 끝");
    }
}
