using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject m_pauseScreen;
    public GameObject m_levelUpScreen;

    public bool m_bossSpawned;
    public float m_timeAlive;

    public override void Awake()
    {
        Time.timeScale = 1f;
        
        base.Awake();
    }

    void Start()
    {
        m_pauseScreen.SetActive(false);
        m_levelUpScreen.SetActive(false);
        if(PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.m_soulsToDrop = PlayerStatsManager.Instance.AmountOfSoulsLevel;
        }

    }

    private void Update()
    {
        m_timeAlive += Time.deltaTime;
        UIManager.Instance.m_timeSurvived.text = m_timeAlive.ToString("F2");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        m_pauseScreen.SetActive(false);
    }
}
