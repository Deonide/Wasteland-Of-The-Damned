using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int m_damage; 
     
    [SerializeField]
    protected int m_pierce;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        m_pierce--;

        if (m_pierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
