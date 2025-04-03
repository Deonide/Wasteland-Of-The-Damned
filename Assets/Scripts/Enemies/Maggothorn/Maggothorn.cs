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


public class Maggothorn : EnemyBase
{
    public enemystate m_enemyState;
    [SerializeField]
    private float m_attackingDistance = 5f;
    [SerializeField]
    private bool m_coroutineStarted;

    private MaggothornCoroutine m_coroutine;

    protected override void Start()
    {
        base.Start();
        m_coroutineStarted = false;
        m_coroutine = GetComponentInChildren<MaggothornCoroutine>();
    }

    protected override void FixedUpdate()
    {
        if (m_enemyState == enemystate.moving)
        {
            if (Vector3.Distance(transform.position, m_playerPos.transform.position) >= m_attackingDistance)
            {
                base.FixedUpdate();
            }
            else
            {
                if (!m_coroutineStarted)
                {
                    StartCoroutine(m_coroutine.NeedleSpray());
                    m_enemyState = enemystate.attacking;
                    m_coroutineStarted = true;
                }
            }
        }
    }


}
