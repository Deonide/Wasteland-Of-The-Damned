using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int m_damage; 
     
    [SerializeField]
    protected int m_pierce = 1;
}
