using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemy;

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
        GameManager.Instance.SetTimer(60);

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                enemy = PoolManager.Instance.enemyPool.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                    enemyComponent.Init(4.5f);
            }
            yield return YieldInstructionCache.WaitForSeconds(5f);
        }

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                enemy = PoolManager.Instance.enemyPool.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                    enemyComponent.Init(4.5f);
            }
            for (int i = 0; i < 1; i++)
            {
                enemy = PoolManager.Instance.enemyPool.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                    enemyComponent.Init(13f);
            }
            yield return YieldInstructionCache.WaitForSeconds(5f);
        }


        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                enemy = PoolManager.Instance.enemyPool.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                    enemyComponent.Init(4.5f);
            }
            for (int i = 0; i < 2; i++)
            {
                enemy = PoolManager.Instance.enemyPool.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                    enemyComponent.Init(13f);
            }
            yield return YieldInstructionCache.WaitForSeconds(5f);
        }

    }

    private Vector3 GetSpawnPosition()
    {
        var player = GameManager.Instance.Player;

        var n = Mathf.Pow(-1, Random.Range(0, 2));

        Vector3 spawnPos = player.transform.position;
        spawnPos.y = 1;

        if(n == -1)
        { // horizon
            spawnPos.x += Random.Range(-18f, 18f);
            spawnPos.z += 15f * Mathf.Pow(-1, Random.Range(0, 2));
        }
        if(n == 1)
        { // vertical
            spawnPos.x += 18f * Mathf.Pow(-1, Random.Range(0, 2));
            spawnPos.z += Random.Range(-15f, 15f);
        }
        return spawnPos;
    }
}
