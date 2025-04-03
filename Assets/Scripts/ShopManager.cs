using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private GameObject m_AOEButton;
    [SerializeField]
    private GameObject m_arrowButton;

    [Header("TMP Text")]
    [SerializeField]
    private TMP_Text m_unlockAOEText;
    [SerializeField]
    private TMP_Text m_unlockArrowText;
    [SerializeField]
    private TMP_Text m_pickUpText;
    [SerializeField]
    private TMP_Text m_healthText;
    [SerializeField]
    private TMP_Text m_soulsUpgradeText;
    [SerializeField]
    private TMP_Text m_soulsText;

    private int m_unlockArrowCost = 500;
    private int m_unlockAOECost = 250;

    private int m_PickUpsUpgradeCost;
    private int m_healthUpgradeCost;
    private int m_amountOfSoulsUpgradeCost;

    public void Start()
    {
        UpdateCosts();
    }

    #region Upgrade/Unlock Functions
    public void UpgradePickUpLevel()
    {
        if (PlayerStats.Instance.m_Souls >= m_PickUpsUpgradeCost)
        {
            PlayerStats.Instance.m_Souls -= m_PickUpsUpgradeCost;
            PlayerStats.Instance.m_amountOfPickUpsLevel++;
            UpdateCosts();
        }
    }

    public void UpgradeHealthLevel()
    {
        if (PlayerStats.Instance.m_Souls >= m_healthUpgradeCost)
        {
            PlayerStats.Instance.m_Souls -= m_healthUpgradeCost;
            PlayerStats.Instance.m_healthLevel++;
            UpdateCosts();
        }
    }

    public void UpgradeAmountOfSoulsLevel()
    {
        if (PlayerStats.Instance.m_Souls >= m_amountOfSoulsUpgradeCost)
        {
            PlayerStats.Instance.m_Souls -= m_amountOfSoulsUpgradeCost;
            PlayerStats.Instance.m_amountOfSoulsLevel++;
            UpdateCosts();
        }
    }

    public void UnlockAOEAttack()
    {
        if(PlayerStats.Instance.m_Souls >= m_unlockAOECost)
        {
            PlayerStats.Instance.m_Souls -= m_unlockAOECost;
            PlayerStats.Instance.m_AOEAttackUnlocked = true;
            UpdateCosts();
        }
    }

    public void UnlockArrow()
    {
        if (PlayerStats.Instance.m_Souls >= m_unlockArrowCost)
        {
            PlayerStats.Instance.m_Souls -= m_unlockArrowCost;
            PlayerStats.Instance.m_arrowUnlocked = true;
            UpdateCosts();
        }
    }

    private void UpdateCosts()
    {
        switch (PlayerStats.Instance.m_amountOfPickUpsLevel)
        {
            case 0:
                m_PickUpsUpgradeCost = 250;
                break;
            case 1:
                m_PickUpsUpgradeCost = 500;
                break;
            case 2:
                m_PickUpsUpgradeCost = 1000;
                break;
        }

        switch (PlayerStats.Instance.m_healthLevel)
        {
            case 0:
                m_healthUpgradeCost = 250;
                break;
            case 1:
                m_healthUpgradeCost = 500;
                break;
            case 2:
                m_healthUpgradeCost = 1000;
                break;
        }

        switch (PlayerStats.Instance.m_amountOfSoulsLevel)
        {
            case 0:
                m_amountOfSoulsUpgradeCost = 250;
                break;
            case 1:
                m_amountOfSoulsUpgradeCost = 500;
                break;
            case 2:
                m_amountOfSoulsUpgradeCost = 1000;
                break;
        }

        if (PlayerStats.Instance.m_AOEAttackUnlocked)
        {
            m_AOEButton.SetActive(false);
        }

        if (PlayerStats.Instance.m_arrowUnlocked)
        {
            m_arrowButton.SetActive(false);
        }
        UpdateText();
    }

    private void UpdateText()
    {
        m_unlockAOEText.text = m_unlockAOECost.ToString();
        m_unlockArrowText.text = m_unlockArrowCost.ToString();
        m_pickUpText.text = m_PickUpsUpgradeCost.ToString();
        m_healthText.text = m_healthUpgradeCost.ToString();
        m_soulsUpgradeText.text = m_amountOfSoulsUpgradeCost.ToString();
        m_soulsText.text = "Souls " + PlayerStats.Instance.m_Souls.ToString();
    }
    #endregion
}
