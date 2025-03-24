using UnityEngine;

public class Arrow : WeaponBase
{
    private Rigidbody m_rb;

    protected void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        m_rb.AddForce(transform.forward * 10);
    }
}
