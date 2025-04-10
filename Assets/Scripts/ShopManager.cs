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
        if (PlayerStatsManager.Instance.Souls >= m_PickUpsUpgradeCost)
        {
            PlayerStatsManager.Instance.Souls -= m_PickUpsUpgradeCost;
            PlayerStatsManager.Instance.AmountOfPickUpsLevel++;
            UpdateCosts();
        }
    }

    public void UpgradeHealthLevel()
    {
        if (PlayerStatsManager.Instance.Souls >= m_healthUpgradeCost)
        {
            PlayerStatsManager.Instance.Souls -= m_healthUpgradeCost;
            PlayerStatsManager.Instance.HealthLevel++;
            UpdateCosts();
        }
    }

    public void UpgradeAmountOfSoulsLevel()
    {
        if (PlayerStatsManager.Instance.Souls >= m_amountOfSoulsUpgradeCost)
        {
            PlayerStatsManager.Instance.Souls -= m_amountOfSoulsUpgradeCost;
            PlayerStatsManager.Instance.AmountOfSoulsLevel++;
            UpdateCosts();
        }
    }

    public void UnlockAOEAttack()
    {
        if(PlayerStatsManager.Instance.Souls >= m_unlockAOECost)
        {
            PlayerStatsManager.Instance.Souls -= m_unlockAOECost;
            PlayerStatsManager.Instance.AOEAttackUnlocked = true;
            UpdateCosts();
        }
    }

    public void UnlockArrow()
    {
        if (PlayerStatsManager.Instance.Souls >= m_unlockArrowCost)
        {
            PlayerStatsManager.Instance.Souls -= m_unlockArrowCost;
            PlayerStatsManager.Instance.ArrowUnlocked = true;
            UpdateCosts();
        }
    }

    private void UpdateCosts()
    {
        switch (PlayerStatsManager.Instance.AmountOfPickUpsLevel)
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

        switch (PlayerStatsManager.Instance.HealthLevel)
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

        switch (PlayerStatsManager.Instance.AmountOfSoulsLevel)
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

        if (PlayerStatsManager.Instance.AOEAttackUnlocked)
        {
            m_AOEButton.SetActive(false);
        }

        if (PlayerStatsManager.Instance.ArrowUnlocked)
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
        m_soulsText.text = "Souls " + PlayerStatsManager.Instance.Souls.ToString();
    }

    public void SaveStats()
    {
        PlayerStatsManager.Instance.Save();
    }
    #endregion
}
