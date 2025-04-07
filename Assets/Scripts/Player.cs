using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using System.ComponentModel;
using UnityEngine.UI;
using static Player;

public class Player : MonoBehaviour
{
    #region Variables
    //<-- Movement -->
    public float m_movementSpeed = 5;
    public float m_maxSpeed = 5;
    
    private float m_debuffTimer = 2;
    public bool m_rooted;

    private Vector2 m_movementInput;
    //<- End Movement ->

    //<-- Pointer -->
    [SerializeField] 
    private LayerMask lasermask;
    [SerializeField]
    private LayerMask groundMask;

    [SerializeField] 
    private float laserLength;
    [SerializeField] 
    private Transform aimPoint;
    [SerializeField] 
    private Transform laserPoint;

    [SerializeField]
    public weaponUsed m_weaponUsed;
    private Camera MainCamera;

    //Rigidbody
    Rigidbody m_rb;

    //<-- Health -->
    public int m_currentHealth;
    
    private int m_startHealth = 40;
    private int m_maxHealth = 40;
    //<- End Health ->

    //<-- Level Up System -->
    Upgrade[] m_upgrades = new Upgrade[]
    {
        new Upgrade { Name = "Arrow Pierce", Description = "Arrow Pierces X more enemies", Increase = 1 },
        new Upgrade { Name = "Arrow Damage", Description = "Increases Arrow damage by X", Increase = 2 },
        new Upgrade { Name = "Sword Damage", Description = "Increases Sword damage by X", Increase = 3 },
        new Upgrade { Name = "AOE Damage", Description = "Increases AOE Attack's damage by X", Increase = 1 },
        new Upgrade { Name = "Health Restore", Description = "Increases health gained from pickups by X", Increase = 5},
        new Upgrade { Name = "Max Health", Description = "Increase max health by X", Increase = 15 },
        new Upgrade { Name = "Speed", Description = "Increase movementspeed by X", Increase = 1},
        new Upgrade { Name = "Attack Speed", Description = "Increase your attack speed by X Seconds", Increase = 0.25f},
    };
    
    [SerializeField]
    private GameObject[] m_upgradeButtons;
    [SerializeField]
    private TMP_Text[] m_upgradeText;

    public int m_expNeededToLevelUp;
    public int m_currentExp;
    //<- End Level Up System ->
    #endregion

    void Start()
    {
/*        Cursor.lockState = CursorLockMode.Locked;*/
        m_expNeededToLevelUp = 50;
        m_currentHealth = m_startHealth;
        m_rb = gameObject.GetComponent<Rigidbody>();
        MainCamera = Camera.main;
    }

    #region Inputs
    public void OnMove(InputAction.CallbackContext ctx)
    {
        m_movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnWeaponSwitch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if(m_weaponUsed == weaponUsed.Dagger)
            {
                if (PlayerStatsManager.Instance.AOEAttackUnlocked)
                {
                    m_weaponUsed = weaponUsed.AOE;
                }
                else if (PlayerStatsManager.Instance.ArrowUnlocked)
                {
                    m_weaponUsed = weaponUsed.Arrow;
                }
            }
            else if(m_weaponUsed == weaponUsed.AOE)
            {
                if (PlayerStatsManager.Instance.ArrowUnlocked)
                {
                    m_weaponUsed = weaponUsed.Arrow;
                }
            }
            else
            {
                m_weaponUsed = weaponUsed.Dagger;
            }

            Debug.Log(m_weaponUsed);
        }
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Time.timeScale = 0f;
            GameManager.Instance.m_pauseScreen.SetActive(true);
        }
    }
    #endregion
    private void Update()
    {
        Aim();
        if(m_currentExp >= m_expNeededToLevelUp)
        {
            LevelUpStats();
        }
    }

    #region Level Up System
    private void LevelUpStats()
    {
        m_currentExp -= m_expNeededToLevelUp;
        m_expNeededToLevelUp = Mathf.RoundToInt(m_expNeededToLevelUp * 1.5f);
        GameManager.Instance.m_levelUpScreen.SetActive(true);
        SetButtons();
    }

    //https://www.youtube.com/watch?v=GvWFXHZ7V7E
    public void SetButtons()
    {
        List<int> availableUpgrades = new List<int>();
        for (int i = 0; i < m_upgrades.Length; i++)
        {
            availableUpgrades.Add(i);
        }

        ShuffleList(availableUpgrades);

        Upgrade[] usedUpgrades = new Upgrade[3];
        for (int upgrades = 0; upgrades < m_upgradeButtons.Length; upgrades++)
        {
            usedUpgrades[upgrades] = m_upgrades[availableUpgrades[upgrades]];
        }

        for (int buttonText = 0; buttonText < m_upgradeButtons.Length; buttonText++)
        {
            m_upgradeButtons[buttonText].GetComponentInChildren<TMP_Text>().text = usedUpgrades[buttonText].Name;
        }

        for (int descriptionText = 0; descriptionText < m_upgradeText.Length; descriptionText++)
        {
            m_upgradeText[descriptionText].text = usedUpgrades[descriptionText].Description.Replace("X", usedUpgrades[descriptionText].Increase.ToString());
        }
        Time.timeScale = 0;
    }

    public void ChosenUpgrade(string chosenUpgrade)
    {
        switch (chosenUpgrade)
        {
            case "Arrow Pierce":
                PlayerStatsManager.Instance.m_arrowPierce +=  1;
                break;
            case "Arrow Damage":
                PlayerStatsManager.Instance.m_arrowDamage += 2;
                break;
            case "Sword Damage":
                PlayerStatsManager.Instance.m_swordDamage += 3;
                break;
            case "AOE Damage":
                PlayerStatsManager.Instance.m_AOEDamage += 1;
                break;
            case "Health Restore":
                PlayerStatsManager.Instance.m_healAmount += 5;
                break;
            case "Max Health":
                m_maxHealth += 15;
                break;
            case "Speed":
                m_maxSpeed += 1;
                m_movementSpeed = m_maxSpeed;
                break;
            case "Attack Speed":
                PlayerStatsManager.Instance.m_attackTimer -= 0.25f;
                break;
        }
        m_currentHealth = m_maxHealth;
    }

    // SHUFFLE LIST
    public void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Increase { get; set; }
    }
    #endregion

    void FixedUpdate()
    {
        m_rb.linearVelocity = new Vector3(m_movementInput.x * m_movementSpeed, 0, m_movementInput.y * m_movementSpeed);

        if (m_rooted)
        {
            m_movementSpeed = 0;
        }

        if(m_movementSpeed < m_maxSpeed)
        {
            m_debuffTimer -= Time.deltaTime;
            if(m_debuffTimer <= 0)
            {
                m_rooted = false;
                m_movementSpeed = m_maxSpeed;
                m_debuffTimer = 2;
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            m_currentHealth -= col.gameObject.GetComponent<EnemyBase>().m_damage;

            if (m_currentHealth <= 0)
            {
                SceneManager.LoadScene("Death Screen");
            }
        }
    }

    #region Aim On Screen
    private (bool succes, Vector3 position) GetMousePosition()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
/*        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);*/
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundMask))
        {
            return (succes: true, position: hitInfo.point);
        }
        else
        {
            return (succes: false, position: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (sucesses, position) = GetMousePosition();
        if (sucesses)
        {
            var direction = position - transform.position;
            aimPoint.forward = direction;

            direction.y = 0;

            transform.forward = direction;
            transform.Rotate(0, 0, 0);
        }
    }
    #endregion
}
