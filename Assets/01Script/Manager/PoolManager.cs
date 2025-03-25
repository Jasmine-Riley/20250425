using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonDestroy<PoolManager>
{
    public Pool enemyPool;

    public void Init()
    {
        transform.GetChild(0).TryGetComponent<Pool>(out enemyPool);
    }
}
