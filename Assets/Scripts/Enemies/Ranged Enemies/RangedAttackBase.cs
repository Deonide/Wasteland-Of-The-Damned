using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.AI;

public enum enemystate
{
    moving,
    attacking
}


public class RangedAttackBase : EnemyBase
{
    public enemystate m_enemyState;
    [SerializeField]
    private float m_attackingDistance = 5f;
    [SerializeField]
    private bool m_coroutineStarted;

    private RangedAttackHandler m_maggoThornAttackComponent;
    private Coroutine m_attackingCoroutine;

    protected override void Start()
    {
        base.Start();
        m_coroutineStarted = false;
        m_maggoThornAttackComponent = GetComponentInChildren<RangedAttackHandler>();
    }

    protected override void HandleCurrentState()
    {
        float distance = Vector3.Distance(transform.position, m_playerPos.transform.position);
        bool inMoveDistance = distance >= m_attackingDistance;
        m_enemyState = inMoveDistance ? enemystate.moving : enemystate.attacking;

        if (m_enemyState == enemystate.moving)
        {
            MoveToPlayer();
            StopAttacking();
        }
        else if(m_enemyState == enemystate.attacking)
        {
            StartAttacking();
        }
    }

    private void StartAttacking()
    {
        if (!m_coroutineStarted)
        {
            m_attackingCoroutine = StartCoroutine(m_maggoThornAttackComponent.NeedleSpray());
            m_enemyState = enemystate.attacking;
            m_coroutineStarted = true;
        }
    }

    private void StopAttacking()
    {
        if (m_coroutineStarted)
        {
            StopCoroutine(m_attackingCoroutine);
            m_coroutineStarted = false;
        }
    }
}
