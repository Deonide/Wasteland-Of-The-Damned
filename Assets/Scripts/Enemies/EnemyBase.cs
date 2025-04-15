using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_PickUpS;
    [SerializeField]
    protected NavMeshAgent m_agent;
    [SerializeField]
    private int m_experiencePoints;

    public int m_wave;
    private int m_soulsToDrop;
    public int m_damage;
    public int m_health;

    public Player m_playerPos;

    protected virtual void Start()
    {
        m_health = m_health * m_wave;
        m_experiencePoints = m_experiencePoints * m_wave;
        m_soulsToDrop = PlayerStatsManager.Instance.m_soulsToDrop;
        m_agent = GetComponent<NavMeshAgent>();
        m_playerPos = FindFirstObjectByType<Player>();
    }

    protected virtual void FixedUpdate()
    {
        HandleCurrentState();
    }

    protected virtual void HandleCurrentState()
    {
        MoveToPlayer();
    }

    //Sets the destination of the nav mesh
    protected virtual void MoveToPlayer()
    {
        if (m_playerPos != null)
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
        //Gives a random number to choose which pick up needs to drop (using drop chances)
        if (m_health <= 0)
        {
            PlayerStatsManager.Instance.Souls += m_soulsToDrop;
            switch (PlayerStatsManager.Instance.AmountOfPickUpsLevel)
            {
                case 0:
                    int randomPickUpLevel0 = Random.Range(0, 101);
                    if (randomPickUpLevel0 <= 25)
                    {
                        GameObject ExpPickUp = Instantiate(m_PickUpS[0], transform.position, Quaternion.identity);
                        ExpPickUp.GetComponent<ExpPickUp>().m_expGained = m_experiencePoints;
                        
                    }
                    else if (randomPickUpLevel0 >= 26 && randomPickUpLevel0 <= 37)
                    {
                        Instantiate(m_PickUpS[1], transform.position, Quaternion.identity);
                    }
                    else if (randomPickUpLevel0 >= 38 && randomPickUpLevel0 <= 50)
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
