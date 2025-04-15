using UnityEngine;

public class ExpPickUp : MonoBehaviour
{
    public int m_expGained;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>();
            Player getPlayerScript = collision.gameObject.GetComponent<Player>();
            getPlayerScript.m_currentExp += m_expGained;
            UIManager.Instance.UpdateExpBar(m_expGained);
            Destroy(gameObject);
        }
    }
}
