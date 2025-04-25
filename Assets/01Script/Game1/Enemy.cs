using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chase,
}

public class Enemy : PoolObject
{
    private EnemyState enemyState;

    private Vector3 direction;
    private bool movable = false;
    private float speed = 2f;
    private GameObject target;
    private Rigidbody rigid;
    private Animator anim;

    public void Init(float speed)
    {
        TryGetComponent<Rigidbody>(out rigid);
        anim = GetComponentInChildren<Animator>();
        anim.speed = speed;

        enemyState = EnemyState.Idle;
        movable = true;
        this.speed = speed;

        target = GameManager.Instance.Player;
        SetTarget(target);
    }

    private void ChangeState(EnemyState newState)
    {
        StopCoroutine(enemyState.ToString());
        enemyState = newState;
        StartCoroutine(enemyState.ToString());
    }

    private IEnumerator Idle()
    {
        while (true)
        {
            yield return null;
        }
    }

    private IEnumerator Chase()
    {
        while (target != null)
        {
            SetMoveTarget(target.transform.position);
            yield return YieldInstructionCache.WaitForSeconds(0.8f);
        }
        ChangeState(EnemyState.Idle);
    }

    private IEnumerator ChangeDirection(Vector3 before, Vector3 after)
    {
        float timer = 0f;

        while(timer < 0.4f)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.Lerp(before, after, timer / 0.4f));
            //transform.rotation = Quaternion.LookRotation(direction);

            timer += Time.deltaTime;
            yield return null;
        }
    }


    private void SetMoveTarget(Vector3 newPos)
    {
        var origin = direction;
        direction = newPos - transform.position;
        direction.y = 0;
        direction.Normalize();
        //transform.rotation = Quaternion.LookRotation(direction);
        StartCoroutine(ChangeDirection(origin, direction));

        rigid.velocity = direction * speed;
    }

    public void SetTarget(GameObject newTarget)
    {
        if (enemyState == EnemyState.Idle)
        {
            target = newTarget;
            ChangeState(EnemyState.Chase);
        }
    }
}
