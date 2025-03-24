using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected Player m_playerPos;
    [SerializeField]
    protected NavMeshAgent m_agent;
    
    public int m_damage;
    public int m_health;

    protected void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_playerPos = FindFirstObjectByType<Player>();
    }

    protected virtual void Update()
    {
        m_agent.destination = m_playerPos.transform.position;
    }

    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Weapon"))
        {
            m_health -= col.gameObject.GetComponent<WeaponBase>().m_damage;

            if (m_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
