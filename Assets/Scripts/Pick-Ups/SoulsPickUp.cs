using UnityEngine;

public class SoulsPickUp : MonoBehaviour
{
    private int m_soulsToDrop;

    private void Start()
    {
        m_soulsToDrop = PlayerStatsManager.Instance.m_soulsToDrop;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player getPlayerScript = collision.gameObject.GetComponent<Player>();
            PlayerStatsManager.Instance.Souls += m_soulsToDrop;
            Destroy(gameObject);
        }
    }
}
