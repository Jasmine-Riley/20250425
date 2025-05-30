using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemy;
    private List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        StartCoroutine("Spawn");
        StartCoroutine("Upgrade");
    }

    private void StopSpawn()
    {
        StopEnemys();
        StopCoroutine("Spawn");
        StopCoroutine("Upgrade");
    }

    private void StopEnemys()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Stop();
            Destroy(enemies[i].gameObject);
        }
    }

    private IEnumerator Spawn()
    {
        GameManager.Instance.SetTimer(60, () => UIManager.Instance.OpenClearPopup(true));

        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                enemy = PoolManager.Instance.enemyPool.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                {
                    enemyComponent.Init(this, Random.Range(4.5f, 5.5f));
                    enemies.Add(enemyComponent);
                }
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
                {
                    enemyComponent.Init(this, Random.Range(5f, 6f));
                    enemies.Add(enemyComponent);
                }
            }
            for (int i = 0; i < 1; i++)
            {
                enemy = PoolManager.Instance.enemyPool2.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                {
                    enemyComponent.Init(this, 9f);
                    enemies.Add(enemyComponent);

                }
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
                {
                    enemyComponent.Init(this, Random.Range(5.5f, 6.5f));
                    enemies.Add(enemyComponent);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                enemy = PoolManager.Instance.enemyPool2.GetPoolObject();
                enemy.transform.position = GetSpawnPosition();
                if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                {
                    enemyComponent.Init(this, 12f);
                    enemies.Add(enemyComponent);
                }
            }
            yield return YieldInstructionCache.WaitForSeconds(5f);
        }
        yield return YieldInstructionCache.WaitForSeconds(1f);
        StopSpawn();
    }

    private IEnumerator Upgrade()
    {
        int count = 10;
        int n = 0;
        while (n < count)
        {
            yield return YieldInstructionCache.WaitForSeconds(5f);

            n++;
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].UpgradeSpeed(0.2f);
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

    public void RePosition(Enemy obje)
    {
        enemy = obje.gameObject;
        enemy.transform.position = GetSpawnPosition();
    }
}
