using UnityEngine;

public class HuskRoot : MonoBehaviour
{
    [SerializeField]
    private int m_damage;

    private Rigidbody m_rb;

    protected void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 10f);
    }

    protected void FixedUpdate()
    {
        m_rb.AddForce(transform.forward * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.ToString());
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.m_currentHealth -= m_damage;
            if(!player.m_rooted)
            {
                player.m_rooted = true;
            }
        }

        Destroy(gameObject);
    }
}
