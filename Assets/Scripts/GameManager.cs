using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public GameObject m_pauseScreen;


    public override void Awake()
    {
        Time.timeScale = 1f;
        base.Awake();
    }

    void Start()
    {
        m_pauseScreen.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        m_pauseScreen.SetActive(false);
    }

    public void GoToMainMenu()
    {
       
    }

    void Update()
    {
        
    }
}
