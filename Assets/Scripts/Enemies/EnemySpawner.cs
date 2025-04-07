using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_enemyPrefabs;

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

    private IEnumerator Spawn()
    {
        while (!GameManager.Instance.m_bossSpawned)
        {
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
                m_enemyToSpawn = Random.Range(0, m_enemyPrefabs.Length);
                GameObject spawnmedEnemy = Instantiate(m_enemyPrefabs[m_enemyToSpawn], m_spawnPos, Quaternion.identity);
                spawnmedEnemy.GetComponent<EnemyBase>().m_playerPos = m_Player;
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
            m_enemyToSpawn = Random.Range(0, m_enemyPrefabs.Length);
            GameObject spawnmedEnemy = Instantiate(m_enemyPrefabs[m_enemyToSpawn], m_spawnPos, Quaternion.identity);
            spawnmedEnemy.GetComponent<EnemyBase>().m_playerPos = m_Player;
            i++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_spawnPos, 10);
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(m_Player.transform.position, 50);
        }
    }
}
