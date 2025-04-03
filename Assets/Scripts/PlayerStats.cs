using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance {get; private set;}

    public bool m_arrowUnlocked;
    public bool m_AOEAttackUnlocked;

    public int m_amountOfPickUpsLevel;
    public int m_healthLevel;
    public int m_amountOfSoulsLevel;
    public int m_soulsToDrop;
    public int m_Souls;
    public float m_attackTimer;



    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}