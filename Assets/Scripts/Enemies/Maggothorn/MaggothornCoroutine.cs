using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;


public class MaggothornCoroutine : MonoBehaviour
{
    [SerializeField]
    private GameObject m_thorn, m_weaponSpawnPoint;
    [SerializeField]
    private float m_attackTimer;

    private float m_rotationSpeed = 0.5f;
    private Player m_player;

    private void Start()
    {
        m_player = FindFirstObjectByType<Player>();
    }

    public IEnumerator NeedleSpray()
    {
        while (true)
        {
            Vector3 lastPos = transform.forward;
            float turnTime = 0;
            while ((turnTime / m_rotationSpeed) <= 1f)
            {
                turnTime += Time.deltaTime;
                transform.forward = Vector3.Lerp(lastPos, m_player.transform.position - transform.position, turnTime / m_rotationSpeed);
                yield return new WaitForEndOfFrame();
            }

            Instantiate(m_thorn, m_weaponSpawnPoint.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
            yield return new WaitForSeconds(m_attackTimer);

        }
    }
}
