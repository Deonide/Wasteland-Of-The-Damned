using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
public class LanternShadeCoroutine : MonoBehaviour
{
    [SerializeField, CanBeNull]
    private GameObject m_thorn, m_weaponSpawnPoint;
    [SerializeField]
    private float m_attackTimer;

    private float m_rotationSpeed = 0.5f;
    private Player m_player;
    private LanternShade m_parentScript;
    private CapsuleCollider m_collider;
    private int m_maxHealth = 10;

    private void Start()
    {
        m_player = FindFirstObjectByType<Player>();
        m_parentScript = FindFirstObjectByType<LanternShade>();
        m_collider = GetComponent<CapsuleCollider>();
    }

    public IEnumerator DrainLife()
    {
        while (m_parentScript.m_enemyState == enemystate.attacking)
        {
            Vector3 lastPos = transform.forward;
            float turnTime = 0;
            while ((turnTime / m_rotationSpeed) <= 1f)
            {
                turnTime += Time.deltaTime;
                transform.forward = Vector3.Lerp(lastPos, m_player.transform.position - transform.position, turnTime / m_rotationSpeed);
                yield return new WaitForEndOfFrame();
            }

            m_player.m_currentHealth -= m_parentScript.m_damage;
            if(m_parentScript.m_health < m_maxHealth)
            {
                m_parentScript.m_health++;
            }
            
            /*Instantiate(m_thorn, m_weaponSpawnPoint.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));*/
            yield return new WaitForSeconds(m_attackTimer);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            m_collider.enabled = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            m_collider.enabled = true;
        }
    }
}
