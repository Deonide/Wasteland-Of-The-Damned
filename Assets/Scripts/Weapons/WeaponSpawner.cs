using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
public enum weaponUsed
{
    Dagger,
    AOE,
    Arrow,
}

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField]
    private Animator m_playerAnimator;

    [SerializeField]
    protected float  m_maxAttackTimer;

    private Player m_player;

    private void Start()
    {
        //At the start of the game, set the attackTimer to the right cooldown.
        if (PlayerStatsManager.Instance != null)
        {

            PlayerStatsManager.Instance.m_attackTimer = m_maxAttackTimer;
        }
        m_player = FindFirstObjectByType<Player>();
        StartCoroutine(Attack());
    }

    protected IEnumerator Attack()
    {
        while (true)
        { 
            switch (m_player.m_weaponUsed)
            {
                case weaponUsed.Dagger:
                    m_playerAnimator.SetBool("Stab_Attack", true);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.m_stabAttackSound);
                    break;
                case weaponUsed.AOE:
                    m_playerAnimator.SetBool("AOE_Attack", true);
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.m_AoeAttackSound);
                    break;
                case weaponUsed.Arrow:
                    m_playerAnimator.SetBool("Bow_Attack", true);

                    break;
            }

            if (PlayerStatsManager.Instance != null)
            {
                PlayerStatsManager.Instance.m_attackTimer = m_maxAttackTimer;
                yield return new WaitForSeconds(PlayerStatsManager.Instance.m_attackTimer);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }

        }
    }


}
