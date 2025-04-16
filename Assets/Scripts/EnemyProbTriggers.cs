using UnityEngine;

public class EnemyProbTriggers : MonoBehaviour
{
    [SerializeField]
    private GameObject m_enemySpawnerGameObject;

    private EnemySpawner m_enemySpawnerScript;


    private void Start()
    {
        m_enemySpawnerScript =  m_enemySpawnerGameObject.GetComponent<EnemySpawner>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("City"))
            {
                m_enemySpawnerScript.m_enemyProbabilitys[0].m_probability = 50;
                m_enemySpawnerScript.m_enemyProbabilitys[1].m_probability = 50;
                m_enemySpawnerScript.m_enemyProbabilitys[2].m_probability = 200;
                m_enemySpawnerScript.m_enemyProbabilitys[3].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[4].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[5].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[6].m_probability = 20;
            }
            else if (gameObject.CompareTag("WasteZone"))
            {
                m_enemySpawnerScript.m_enemyProbabilitys[0].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[1].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[2].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[3].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[4].m_probability = 5;
                m_enemySpawnerScript.m_enemyProbabilitys[5].m_probability = 5;
                m_enemySpawnerScript.m_enemyProbabilitys[6].m_probability = 200;
            }
            else if (gameObject.CompareTag("Graveyard"))
            {
                m_enemySpawnerScript.m_enemyProbabilitys[0].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[1].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[2].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[3].m_probability = 200;
                m_enemySpawnerScript.m_enemyProbabilitys[4].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[5].m_probability = 20;
                m_enemySpawnerScript.m_enemyProbabilitys[6].m_probability = 0;
            }
            else if (gameObject.CompareTag("HauntedForest"))
            {
                m_enemySpawnerScript.m_enemyProbabilitys[0].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[1].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[2].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[3].m_probability = 10;
                m_enemySpawnerScript.m_enemyProbabilitys[4].m_probability = 200;
                m_enemySpawnerScript.m_enemyProbabilitys[5].m_probability = 200;
                m_enemySpawnerScript.m_enemyProbabilitys[6].m_probability = 10;
            }
            else
            {
                m_enemySpawnerScript.m_enemyProbabilitys[0].m_probability = 100;
                m_enemySpawnerScript.m_enemyProbabilitys[1].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[2].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[3].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[4].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[5].m_probability = 0;
                m_enemySpawnerScript.m_enemyProbabilitys[6].m_probability = 0;
            }
        }
    }


}
