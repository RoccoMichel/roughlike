using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public float health;
    public float maxHealth;
    [Range(0, 100)]
    public float battery;
    public float batteryDrainRate;
    public float dekurenzi;

    [Header("Status")]
    public bool usingFlashlight;
    public float flashLightSize;
    public float damage;
    public int piercing;
    public int luck;
    public float fireRate;

    [Header("Movement")]
    public bool rawInput;
    public float speed;
    public Vector2 input;

    [Header("References")]
    [SerializeField] KeyCode FlashlightKey;
    [SerializeField] GameObject FlashLight;
    Rigidbody2D rigidbody;
    public Animator ani;

    [Header("Punch")]
    public float knockback = 10;
    public float reach = 1;
    public KeyCode punchKey;
    public LayerMask crate;

    private void Start()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
        health = maxHealth;

        // Apply Jubs Upgrades:
        speed = PlayerPrefs.GetFloat("jubsSpeed", speed);
        luck = PlayerPrefs.GetInt("jubsLuck", luck);
        flashLightSize = PlayerPrefs.GetFloat("jubsFlash", flashLightSize);

        FlashLight.transform.localScale = new Vector3(flashLightSize, flashLightSize, flashLightSize);
    }

    private void Update()
    {
        // Battery
        if (usingFlashlight)
        {
            if (Input.GetKeyDown(FlashlightKey)) usingFlashlight = false;
            if (battery <= 0) usingFlashlight = false;

            battery -= Time.deltaTime * batteryDrainRate;
        }
        else
        {
            if (Input.GetKeyDown(FlashlightKey) && battery > 10) usingFlashlight = true;
            battery += Time.deltaTime * 16;
        }
        FlashLight.SetActive(usingFlashlight);

        battery = Mathf.Clamp(battery, 0, 100);

        // Health
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0) Die();

        Punch();
    }
    private void FixedUpdate()
    {
        // Movement
        TopDownMovement();
    }

    public virtual void TopDownMovement()
    {
        // GET PLAYER INPUTS
        if (rawInput) input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        else input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Vector2 movement = input * speed;

        ani.SetFloat("Vertical", input.y);
        ani.SetFloat("Horizontal", input.x);

        // APPLY
        rigidbody.linearVelocity = movement;
    }

    public void Punch()
    {
        if (Input.GetKeyDown(punchKey))
        {
            Vector3 dir = -(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
            dir.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, reach, crate);

            //Debug.DrawRay(transform.position, dir * 100, Color.red);
            //Debug.Break();

            if (hit)
            {
                hit.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                hit.transform.GetComponent<Rigidbody2D>().AddForce(dir * knockback);
                hit.transform.GetComponent<Crate>().hit = true;
            }
        }
    }
    public void Die()
    {
        PlayerPrefs.SetInt("death", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("DEATH");
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        // Blood Effect
        if (Camera.main.GetComponent<CameraEffects>() != null)
            Camera.main.GetComponent<CameraEffects>().BloodOnScreen(Mathf.CeilToInt(amount / 10));
    }
    public void Heal(float amount)
    {
        health += amount;
    }
    public void HealFull()
    {
        health = maxHealth;
    }
    // Upgrades
    public void HealthUpgrade()
    {
        maxHealth += 10;
    }
    public void DamageUpgrade()
    {
        damage += 10;
    }
    public void BatteryUpgrade()
    {
        batteryDrainRate = Mathf.Clamp(batteryDrainRate - 1, 0.1f, int.MaxValue);
    }
    public void PiercingUpgrade()
    {
        piercing++;
    }
    public void FireRateUpgrade()
    {
        fireRate *= 0.8f;
    }
    // Jubs Upgrades
    public void SpeedUpgrade()
    {
        PlayerPrefs.SetFloat("jubsSpeed", PlayerPrefs.GetFloat("jubsSpeed", speed) + 0.5f);
        speed = PlayerPrefs.GetFloat("jubsSpeed");
    }    
    public void LuckUpgrade()
    {
        PlayerPrefs.SetInt("jubsLuck", PlayerPrefs.GetInt("jubsLuck", luck) + 1);
        luck = PlayerPrefs.GetInt("jubsLuck");
    }
    public void FlashLightSizeUpgrade()
    {
        PlayerPrefs.SetFloat("jubsFlash", PlayerPrefs.GetFloat("jubsFlash", flashLightSize) + 0.1f);
        flashLightSize = PlayerPrefs.GetFloat("jubsFlash");
        FlashLight.transform.localScale = new Vector3(flashLightSize, flashLightSize, flashLightSize);
    }
    public void IncreaseEnemiesUpgrade()
    {
        PlayerPrefs.SetFloat("jubsEnemies", PlayerPrefs.GetFloat("jubsEnemies", 20) + 1.5f);
        PlayerPrefs.SetInt("jubsEnemiesCount", PlayerPrefs.GetInt("jubsEnemiesCount", 0) + 1);
    }
    public void DecreaseEnemiesUpgrade()
    {
        PlayerPrefs.SetFloat("jubsEnemies", PlayerPrefs.GetFloat("jubsEnemies", 20) - 1.5f);
        PlayerPrefs.SetInt("jubsEnemiesCount", PlayerPrefs.GetInt("jubsEnemiesCount", 0) + 1);
    }
}
