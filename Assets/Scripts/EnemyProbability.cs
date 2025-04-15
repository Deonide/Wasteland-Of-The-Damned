using UnityEngine;

[System.Serializable]
public class EnemyProbability
{
    //Different places need different enemies
    //probability gets changed depending on the area.
    public float m_probability =  1;
    public GameObject m_enemyPrefab;
}
