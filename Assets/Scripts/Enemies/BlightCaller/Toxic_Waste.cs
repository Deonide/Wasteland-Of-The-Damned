using UnityEngine;

public class Toxic_Waste : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.m_currentHealth--;
        }

    }
}
