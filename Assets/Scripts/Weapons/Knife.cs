using UnityEngine;

public class Knife : WeaponBase
{
    protected virtual void Start()
    {
        Destroy(gameObject, .1f);
    }
}
