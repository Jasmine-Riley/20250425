using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chase,
}

public class Enemy : PoolObject, IMove
{
    private NavMeshAgent agent;
    private EnemyState enemyState;

    private Vector3 direction;
    private bool movable = false;
    private float speed = 5f;
    private GameObject target;

    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
    }

    public void Init()
    {
        enemyState = EnemyState.Idle;
        movable = true;
        agent.isStopped = false;

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
            yield return YieldInstructionCache.WaitForSeconds(1f);
        }
        ChangeState(EnemyState.Idle);
    }

    public void Move(Vector3 direction)
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void SetMoveTarget(Vector3 newPos)
    {
        // ���ο� �������� ���� �޽��� �ִ� �������� ���� �Ŀ� �̵�
        // �ش� ��ǥ�� �޽��� �ִ� ��ġ�ΰ�?
        var pos = newPos;
        if (NavMesh.SamplePosition(pos, out NavMeshHit hitResult, 10f, NavMesh.AllAreas))
        {
            pos = hitResult.position;
            agent.SetDestination(pos);
        }
        else
            Debug.Log("���� �Ұ� ����");
    }

    public void SetTarget(GameObject newTarget)
    {
        if (enemyState == EnemyState.Idle)
        {
            target = newTarget;
            ChangeState(EnemyState.Chase);
        }
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }
}
