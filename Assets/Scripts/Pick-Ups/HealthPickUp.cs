using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player getPlayerScript = collision.gameObject.GetComponent<Player>();
            getPlayerScript.m_currentHealth += PlayerStatsManager.Instance.m_healAmount;
            Destroy(gameObject);
        }
    }
}
