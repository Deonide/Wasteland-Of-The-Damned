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
    private int m_minimumRange;
    private int m_minMax = 5;
    private int m_enemyToSpawn;

    private Vector3 m_Randompos;
    private Vector3 m_spawnPos;
    private Player m_Player;

    void Start()
    {
        m_Player = FindAnyObjectByType<Player>();
        m_minimumRange = m_spawningDistance - m_minMax;
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            m_Randompos = Random.onUnitSphere * m_spawningDistance;
            m_spawnPos = m_Player.transform.position;
            m_spawnPos.x += m_Randompos.x;
            m_spawnPos.z += m_Randompos.z;
            Debug.Log(m_Randompos);

            if(Vector3.Distance(m_Player.transform.position, m_spawnPos) < m_minimumRange)
            {
                continue;
            }
            m_enemyToSpawn = Random.Range(0, m_enemyPrefabs.Length);
            Instantiate(m_enemyPrefabs[m_enemyToSpawn], m_spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(m_spawningTime);
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
