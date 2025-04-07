using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance {get; private set;}

    private PlayerStats m_playerStats;

    public int m_soulsToDrop;
    public int m_healAmount = 20;
    public int m_swordDamage = 5;
    public int m_arrowDamage = 2;
    public int m_arrowPierce = 1;
    public int m_AOEDamage;
    public float m_attackTimer;

    //=> is the game as {}
    public bool ArrowUnlocked { 
        get => m_playerStats.m_arrowUnlocked;
        set => m_playerStats.m_arrowUnlocked = value;
    }
    public bool AOEAttackUnlocked
    {
        get => m_playerStats.m_arrowUnlocked;
        set => m_playerStats.m_arrowUnlocked = value;
    }

    public int AmountOfPickUpsLevel
    {
        get => m_playerStats.m_amountOfPickUpsLevel;
        set => m_playerStats.m_amountOfPickUpsLevel = value;
    }
    public int HealthLevel
    {
        get => m_playerStats.m_healthLevel;
        set => m_playerStats.m_healthLevel = value;
    }
    public int AmountOfSoulsLevel
    {
        get => m_playerStats.m_amountOfSoulsLevel;
        set => m_playerStats.m_amountOfSoulsLevel = value;
    }
    public int Souls
    {
        get => m_playerStats.m_Souls;
        set => m_playerStats.m_Souls = value;
    }

    public void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            m_playerStats = SaveSystem.Deserialize();
            if (m_playerStats == null)
            {
                m_playerStats = ScriptableObject.CreateInstance<PlayerStats>();
                Save();
            }
            DontDestroyOnLoad(gameObject);
            Application.quitting += OnQuit;
        }

        else
        {
            Destroy(gameObject);
        } 
    }

    private void OnQuit()
    {
        Save();
    }

    public void Save()
    {
        SaveSystem.SerializeData(m_playerStats);
    }
}