using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected Player m_playerPos;
    [SerializeField]
    private GameObject[] m_PickUpS;
    [SerializeField]
    private NavMeshAgent m_agent;

    
    
    private int m_soulsToDrop;
    public int m_damage;
    public int m_health;


    protected virtual void Start()
    {
        PlayerStats.Instance.m_soulsToDrop = PlayerStats.Instance.m_amountOfSoulsLevel;
        m_soulsToDrop = PlayerStats.Instance.m_soulsToDrop;
        m_agent = GetComponent<NavMeshAgent>();
        m_playerPos = FindFirstObjectByType<Player>();
    }

    protected virtual void FixedUpdate()
    {
        if(m_playerPos != null)
        {
            m_agent.destination = m_playerPos.transform.position;
        }
    }

    #region Colission + OnDeath
    protected virtual void OnCollisionEnter(Collision col)
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

    private void OnDestroy()
    {
        if (m_health <= 0)
        {
            PlayerStats.Instance.m_Souls += m_soulsToDrop;
            switch (PlayerStats.Instance.m_amountOfPickUpsLevel)
            {
                case 0:
                    int randomPickUpLevel0 = Random.Range(0, 101);
                    if (randomPickUpLevel0 <= 25)
                    {
                        Instantiate(m_PickUpS[0], transform.position, Quaternion.identity);
                    }
                    else if (randomPickUpLevel0 <= 37)
                    {
                        Instantiate(m_PickUpS[1], transform.position, Quaternion.identity);
                    }
                    else if (randomPickUpLevel0 <= 50)
                    {
                        Instantiate(m_PickUpS[2], transform.position, Quaternion.identity);
                    }
                    break;
                case 1:
                    int randomPickUpLevel1 = Random.Range(0, 76);
                    if (randomPickUpLevel1 <= 25)
                    {
                        Instantiate(m_PickUpS[0], transform.position, Quaternion.identity);
                    }
                    else if (randomPickUpLevel1 <= 37)
                    {
                        Instantiate(m_PickUpS[1], transform.position, Quaternion.identity);
                    }
                    else if (randomPickUpLevel1 <= 50)
                    {
                        Instantiate(m_PickUpS[2], transform.position, Quaternion.identity);
                    }
                    break;
                case 2:
                    int randomPickUpLevel2 = Random.Range(0, 51);
                    if (randomPickUpLevel2 <= 20)
                    {
                        Instantiate(m_PickUpS[0], transform.position, Quaternion.identity);
                    }
                    else if (randomPickUpLevel2 <= 30)
                    {
                        Instantiate(m_PickUpS[1], transform.position, Quaternion.identity);
                    }
                    else if(randomPickUpLevel2 <= 40)
                    {
                        Instantiate(m_PickUpS[2], transform.position, Quaternion.identity);
                    }
                    break;
            }

        }
    }
    #endregion
}
