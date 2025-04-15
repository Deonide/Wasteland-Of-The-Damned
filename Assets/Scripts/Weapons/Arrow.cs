using UnityEngine;

public class Arrow : WeaponBase
{
    private Rigidbody m_rb;

    protected void Start()
    {
        m_damage = PlayerStatsManager.Instance.m_arrowDamage;
        m_pierce = PlayerStatsManager.Instance.m_arrowPierce;
        m_rb = GetComponent<Rigidbody>();
    }

    protected void FixedUpdate()
    {
        //Goes after every frame
        m_rb.AddForce(transform.forward * 10);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        m_pierce--;
        if (m_pierce <= 0)
        {
            Destroy(gameObject);
        }
    }


}
