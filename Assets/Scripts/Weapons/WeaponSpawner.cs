using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
public enum weaponUsed
{
    Dagger,
    AOE,
    Arrow
}

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_prefabs = new List<GameObject>();
    private GameObject m_weaponPrefab;

    [SerializeField]
    protected float  m_maxAttackTimer;

    private Player m_player;

    private void Start()
    {
        //At the start of the game, set the attackTimer to the right cooldown.
        PlayerStats.Instance.m_attackTimer = m_maxAttackTimer;
        m_player = FindFirstObjectByType<Player>();
        StartCoroutine(Attack());
    }

    protected IEnumerator Attack()
    {
        while (true)
        {
            PlayerStats.Instance.m_attackTimer = m_maxAttackTimer;

            switch (m_player.m_weaponUsed)
            {
                case weaponUsed.Dagger:
                    m_weaponPrefab = m_prefabs[0];
                    break;
                case weaponUsed.AOE:
                    m_weaponPrefab = m_prefabs[1];
                    break;
                case weaponUsed.Arrow:
                    m_weaponPrefab = m_prefabs[1];
                    break;
            }

            if (m_player.m_weaponUsed == weaponUsed.Dagger)
            {
                GameObject spawnedWeapon = Instantiate(m_weaponPrefab, transform.position, Quaternion.Euler(90, m_player.transform.rotation.eulerAngles.y /*- 90*/, 0));
            }
            else if (m_player.m_weaponUsed == weaponUsed.AOE)
            {

            }
            else
            {
                GameObject spawnedWeapon = Instantiate(m_weaponPrefab, transform.position, Quaternion.Euler(0, m_player.transform.rotation.eulerAngles.y, 0));
            }

            yield return new WaitForSeconds(PlayerStats.Instance.m_attackTimer);
        }
    }
}
