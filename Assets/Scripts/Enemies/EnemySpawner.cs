using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<EnemyProbability> m_enemyProbabilitys = new List<EnemyProbability>();

    [SerializeField]
    private int m_spawningTime;

    [SerializeField]
    private int m_spawningDistance = 5;

    [SerializeField]
    private int m_minimumRange, m_amountOfEnemies, m_wave, m_groups;

    [SerializeField]
    private Player m_Player;

    private int m_minMax = 5;
    private int m_enemyToSpawn;

    private Vector3 m_Randompos;
    private Vector3 m_spawnPos;


    void Start()
    {
        m_minimumRange = m_spawningDistance - m_minMax;
        StartCoroutine("Spawn");
    }

    private GameObject ChooseEnemy()
    {
        //Calculates the total amount of probabilitys of all enemies
        var total_probs = 0f;
        foreach (var prob in m_enemyProbabilitys)
        {
            total_probs += prob.m_probability;
        }

        //Gives a random number from 0 to the total probs variabel
        var random_num = Random.Range(0, total_probs);
        var running_sum = 0f;
        foreach (var prob in m_enemyProbabilitys)
        {
            running_sum += prob.m_probability;
            if (running_sum > random_num)
            {
                return prob.m_enemyPrefab;
            }
        }

        //returns the enemy in which the number is of probabilitys
        return m_enemyProbabilitys[0].m_enemyPrefab;
    }

    private IEnumerator Spawn()
    {
        while (!GameManager.Instance.m_bossSpawned)
        {
            for (int i = 0; i < m_amountOfEnemies; i++)
            {
                //Chooses a random position within a sphere around the player
                m_Randompos = Random.onUnitSphere * m_spawningDistance;
                m_spawnPos = m_Player.transform.position;
                m_spawnPos.x += m_Randompos.x;
                m_spawnPos.z += m_Randompos.z;

                //Checks if there are no colliders on the place where the enemy wants to spawn
                Collider[] colliderOverlap = Physics.OverlapSphere(m_spawnPos, 2);

                Debug.Log(m_Randompos);

                //if the random position is outside the minimum spawning range and there are no colliders on that spot then enemy can proceed to spawn
                if (Vector3.Distance(m_Player.transform.position, m_spawnPos) < m_minimumRange && colliderOverlap.Length == 0)
                {
                    continue;
                }

                GameObject spawnmedEnemy = Instantiate(ChooseEnemy(), m_spawnPos, Quaternion.identity);
                spawnmedEnemy.GetComponent<EnemyBase>().m_playerPos = m_Player;
                spawnmedEnemy.GetComponent<EnemyBase>().m_wave = m_wave;
                i++;
                yield return new WaitForSeconds(m_spawningTime);
            }


            m_amountOfEnemies = Mathf.RoundToInt(m_wave * 0.5f);
            if (m_amountOfEnemies < 1)
            {
                m_amountOfEnemies = 1;
            }

            m_groups++;
            if (m_groups > 10)
            {
                m_wave++;
                m_groups = 0;
            }
            yield return new WaitForSeconds(m_spawningTime);
        }
    }

    public void EnemySpawnerLoop()
    {
        //Use for the Boss.
        for (int i = 0; i < m_amountOfEnemies; i++)
        {
            m_Randompos = Random.onUnitSphere * m_spawningDistance;
            m_spawnPos = m_Player.transform.position;
            m_spawnPos.x += m_Randompos.x;
            m_spawnPos.z += m_Randompos.z;
            Debug.Log(m_Randompos);

            if (Vector3.Distance(m_Player.transform.position, m_spawnPos) < m_minimumRange)
            {
                continue;
            }
            GameObject spawnmedEnemy = Instantiate(ChooseEnemy(), m_spawnPos, Quaternion.identity);
            spawnmedEnemy.GetComponent<EnemyBase>().m_playerPos = m_Player;
            i++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Shows the place where enemies can spawn.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_spawnPos, 10);
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(m_Player.transform.position, 50);
        }
    }
}
