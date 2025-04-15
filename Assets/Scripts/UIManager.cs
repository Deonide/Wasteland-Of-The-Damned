using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }
    
    [SerializeField]
    private TMP_Text m_healthText, m_soulsText;
   
    [SerializeField]
    private Slider m_expSlider;

    private Player m_player;
    public TMP_Text m_timeSurvived;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        m_player = FindFirstObjectByType<Player>();
        UpdateText();
    }

    public void UpdateText()
    {
        m_healthText.text = m_player.m_currentHealth.ToString();
        if (PlayerStatsManager.Instance != null)
        {
            m_soulsText.text = PlayerStatsManager.Instance.Souls.ToString();
        }
    }

    public void UpdateExpBar(int amount)
    {
        m_expSlider.value += ConvertIntIntoFloat(amount);

    }

    public void UpdateExpValues(int expNeededtoLevelUp)
    {
        m_expSlider.maxValue = expNeededtoLevelUp;
    }

    private float ConvertIntIntoFloat(int amount)
    {
        return(float) amount;
    }
}
