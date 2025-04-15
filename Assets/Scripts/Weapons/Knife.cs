using UnityEngine;

public class Knife : WeaponBase
{
    protected virtual void Start()
    {
        m_damage = PlayerStatsManager.Instance.m_swordDamage;
    }
}
