using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //<-- Movement -->
    public float m_movementSpeed = 5;
    private float m_maxSpeed = 5;
    private float m_debuffTimer = 2;

    private Vector2 m_movementInput;

    //<- End Movement ->

    //<-- Pointer -->
    [SerializeField] 
    private LayerMask lasermask;
    [SerializeField] 
    private float laserLength;
    [SerializeField] 
    Transform aimPoint;
    [SerializeField] 
    Transform laserPoint;
    [SerializeField] 
    private LayerMask groundMask;

    private Camera MainCamera;

    //Rigidbody
    Rigidbody m_rb;

    public int m_currentHealth;
    private int m_maxHealth = 40;
    private int m_playerLevel;
    private int m_expNeededToLevelUp;


    [SerializeField]
    public weaponUsed m_weaponUsed;

    void Start()
    {
/*        Cursor.lockState = CursorLockMode.Locked;*/
        m_playerLevel = 0;
        m_expNeededToLevelUp = 50;
        m_currentHealth = m_maxHealth;
        m_rb = gameObject.GetComponent<Rigidbody>();
        MainCamera = Camera.main;
    }

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
                if (PlayerStats.Instance.m_AOEAttackUnlocked)
                {
                    m_weaponUsed = weaponUsed.AOE;
                }
                else if (PlayerStats.Instance.m_arrowUnlocked)
                {
                    m_weaponUsed = weaponUsed.Arrow;
                }
            }
            else if(m_weaponUsed == weaponUsed.AOE)
            {
                if (PlayerStats.Instance.m_arrowUnlocked)
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

    private void Update()
    {
        Aim();
    }

    void FixedUpdate()
    {
        m_rb.linearVelocity = new Vector3(m_movementInput.x * m_movementSpeed, 0, m_movementInput.y * m_movementSpeed);

        if(m_movementSpeed < m_maxSpeed)
        {
            m_debuffTimer -= Time.deltaTime;
            if(m_debuffTimer <= 0)
            {
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
/*
            lineRenderer.SetPosition(0, laserPoint.position);

            lineRenderer.SetPosition(1, position);*/
        }

    }
}
