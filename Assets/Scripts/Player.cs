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
    //Direction Check
    private float x, y;

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

    public float m_healthLossTimer;
    //<- End Health ->

    //<-- Level Up System -->
    //Array of possible upgrades
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

    [SerializeField]
    private Animator m_anim;

    [SerializeField]
    private GameObject[] m_usedWeapon;

    [SerializeField]
    private GameObject m_arrowPrefab, m_weaponSpawner;

    #endregion

    void Start()
    {
        m_expNeededToLevelUp = 50;
        m_currentHealth = m_startHealth;
        m_rb = gameObject.GetComponent<Rigidbody>();
        MainCamera = Camera.main;
        UIManager.Instance.UpdateExpValues(m_expNeededToLevelUp);
        WeaponChange();
    }

    #region Inputs
    public void OnMove(InputAction.CallbackContext ctx)
    {
        //Checks which way the player needs to move through the input of the player.
        m_movementInput = ctx.ReadValue<Vector2>();
        x = m_movementInput.x;
        y = m_movementInput.y;
    }

    public void OnWeaponSwitch(InputAction.CallbackContext ctx)
    {
        //Switches the enum weaponUsed.
        if (ctx.performed)
        {
            if (m_weaponUsed == weaponUsed.Dagger)
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
            else if (m_weaponUsed == weaponUsed.AOE)
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
            WeaponChange();
        }
    }

    private void WeaponChange()
    {
        //Checks which weapon the player needs to be holding.
        if (m_weaponUsed == weaponUsed.Dagger)
        {
            m_usedWeapon[0].SetActive(true);
            m_usedWeapon[1].SetActive(false);
            m_usedWeapon[2].SetActive(false);
        }
        else if (m_weaponUsed == weaponUsed.AOE)
        {
            m_usedWeapon[0].SetActive(false);
            m_usedWeapon[1].SetActive(true);
            m_usedWeapon[2].SetActive(false);

        }
        else
        {
            m_usedWeapon[0].SetActive(false);
            m_usedWeapon[1].SetActive(false);
            m_usedWeapon[2].SetActive(true);
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
        //Makes sure that the character is always looking at the cursor.
        Aim();

        //If the player has enough exp they level up.
        if(m_currentExp >= m_expNeededToLevelUp)
        {
            LevelUpStats();
        }
    }

    #region Level Up System
    private void LevelUpStats()
    {
        //The amount of exp get taken off the current exp.
        m_currentExp -= m_expNeededToLevelUp;
        m_expNeededToLevelUp = Mathf.RoundToInt(m_expNeededToLevelUp * 1.5f);
        GameManager.Instance.m_levelUpScreen.SetActive(true);
        UIManager.Instance.UpdateExpValues(m_expNeededToLevelUp);
        SetButtons();
    }

    //https://www.youtube.com/watch?v=GvWFXHZ7V7E
    public void SetButtons()
    {
        //Goes through the list of possible upgrades and turns them into ints.
        List<int> availableUpgrades = new List<int>();
        for (int i = 0; i < m_upgrades.Length; i++)
        {
            availableUpgrades.Add(i);
        }

        //Shuffles the list of possible upgrades.
        ShuffleList(availableUpgrades);

        //Makes sure that an upgrade doesnt gets used twice.
        Upgrade[] usedUpgrades = new Upgrade[3];
        for (int upgrades = 0; upgrades < m_upgradeButtons.Length; upgrades++)
        {
            usedUpgrades[upgrades] = m_upgrades[availableUpgrades[upgrades]];
        }

        //Gives the right name to te corresponding button.
        for (int buttonText = 0; buttonText < m_upgradeButtons.Length; buttonText++)
        {
            m_upgradeButtons[buttonText].GetComponentInChildren<TMP_Text>().text = usedUpgrades[buttonText].Name;
        }

        //Updates the description text.
        for (int descriptionText = 0; descriptionText < m_upgradeText.Length; descriptionText++)
        {
            m_upgradeText[descriptionText].text = usedUpgrades[descriptionText].Description.Replace("X", usedUpgrades[descriptionText].Increase.ToString());
        }

        //To make sure the player doesnt get attacked while choosing an upgrade.
        Time.timeScale = 0;
    }

    public void ChosenUpgrade(string chosenUpgrade)
    {
        //Effects of the upgrades.
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

        //Makes the player heal to full hp when they level up.
        m_currentHealth = m_maxHealth;
    }

    // SHUFFLE LIST
    public void ShuffleList(List<int> list)
    {
        //Shuffles a list of numbers to a random order
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
        //When a new upgrade is made it needs to have the following values
        public string Name { get; set; }
        public string Description { get; set; }
        public float Increase { get; set; }
    }
    #endregion

    void FixedUpdate()
    {
        m_rb.linearVelocity = new Vector3(m_movementInput.x * m_movementSpeed, 0, m_movementInput.y * m_movementSpeed);

        m_anim.SetFloat("X-Coordinat", x);
        m_anim.SetFloat("Y-Coordinat", y);


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
            AudioManager.Instance.PlaySFX(AudioManager.Instance.m_takeDamageSound);
            m_anim.SetBool("TakingDamage", true);
            if (m_currentHealth <= 0)
            {
                m_anim.SetTrigger("Death");
                AudioManager.Instance.PlaySFX(AudioManager.Instance.m_deathSound);
            }
        }
    }

    public void ShootArrow()
    {
        Instantiate(m_arrowPrefab, m_weaponSpawner.transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        AudioManager.Instance.PlaySFX(AudioManager.Instance.m_shootArrowSound);
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
    #region Animations
    public void StopTakingDamage()
    {
        m_anim.SetBool("TakingDamage", false);
    }

    public void ToLossScreen()
    {
        SceneManager.LoadScene("Death Screen");
    }

    public void DisableAttack(int attackType)
    {
        if (attackType == 1)
        {
            m_anim.SetBool("Stab_Attack", false);
        }
        else if (attackType == 2)
        {
            m_anim.SetBool("AOE_Attack", false);
        }
        else if (attackType == 3)
        {
            m_anim.SetBool("Bow_Attack", false);
        }

        Debug.Log("DisableStabAttack");
    }
    #endregion
}
