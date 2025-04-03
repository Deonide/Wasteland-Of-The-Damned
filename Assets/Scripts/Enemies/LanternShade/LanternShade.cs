using UnityEngine;

public class LanternShade : EnemyBase
{
    public enemystate m_enemyState;
    [SerializeField]
    private float m_attackingDistance = 5f;
    [SerializeField]
    private bool m_coroutineStarted;

    private LanternShadeCoroutine m_coroutine;
    private CapsuleCollider m_collider;


    protected override void Start()
    {
        base.Start();
        m_coroutineStarted = false;
        m_coroutine = GetComponentInChildren<LanternShadeCoroutine>();
        m_collider = GetComponent<CapsuleCollider>();
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
                    StartCoroutine(m_coroutine.DrainLife());
                    m_enemyState = enemystate.attacking;
                    m_coroutineStarted = true;
                }
            }
        }
/*        else if (m_enemyState != enemystate.moving && Vector3.Distance(transform.position, m_playerPos.transform.position) >= m_attackingDistance)
        {
            m_coroutineStarted = false;
            StopCoroutine(m_coroutine.DrainLife());
            m_enemyState = enemystate.moving;
        }*/
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            m_collider.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_collider.enabled = true;
    }
}

