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
        var player = GameManager.Instance.Player;
        while(true)
        {
            enemy = PoolManager.Instance.enemyPool.GetPoolObject();
            enemy.transform.position = player.transform.position + new Vector3(Random.Range(3f, 5f), 0, Random.Range(3f, 5f));
            if (enemy.TryGetComponent<Enemy>(out var enemyComponent))
                enemyComponent.Init();
            yield return YieldInstructionCache.WaitForSeconds(5f);
        }
    }
}
