using System.Collections;
using UnityEngine;

public class CarrionKing : EnemyBase
{
    private Animator m_bossAnimator;
    public EnemySpawner m_enemySpawner;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(BossAttack());
        m_enemySpawner = FindFirstObjectByType<EnemySpawner>();
    }

    private IEnumerator BossAttack()
    {
        while (true)
        {
            int attack = Random.Range(0, 3);
            switch (attack)
            {
                case 0:
                    m_bossAnimator.SetBool("Spawning", true);
                    break;
                case 1:
                    m_bossAnimator.SetBool("Jump", true);
                    break;
                case 2:
                    m_bossAnimator.SetBool("Slash", true);
                    break;
            }

            yield return new WaitForSeconds(2);
        }
    }

    
    public void Spawner()
    {
        m_enemySpawner.EnemySpawnerLoop();
        m_bossAnimator.SetBool("Spawning", false);
    }

    public void Jump()
    {
        // Do something with a particle effect to deal damage

        m_bossAnimator.SetBool("Jump", false);
    }

    public void SlashAttack()
    {
        m_bossAnimator.SetBool("Slash", false);
    }

}
