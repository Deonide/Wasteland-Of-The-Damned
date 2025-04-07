using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] 
    private Player Upgrades_script;

    public void Upgrade()
    {
        string Upgrade_chosen = gameObject.GetComponentInChildren<TMP_Text>().text;
        Upgrades_script.ChosenUpgrade(Upgrade_chosen);
        Time.timeScale = 1.0f;
        GameManager.Instance.m_levelUpScreen.SetActive(false);
    }
}
