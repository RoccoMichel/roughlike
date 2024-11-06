using Unity.Mathematics;
using UnityEngine;

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
    public float damage;

    [Header("Movement")]
    public bool rawInput;
    public float speed;
    public Vector2 input;

    [Header("References")]
    [SerializeField] KeyCode FlashlightKey;
    Rigidbody2D rigidbody;

    [Header("Punch")]
    public float knockback = 10;
    public float reach = 1;
    public KeyCode punchKey;
    public LayerMask crate;

    private void Start()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
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
            battery += Time.deltaTime * batteryDrainRate * 5;
        }

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
    public virtual void Die()
    {
        Debug.LogWarning("You died");
        // Do Stuff
    }
    public virtual void TakeDamage(float amount)
    {
        health -= amount;
    }
    public void Heal(float amount)
    {
        health += amount;
    }
    public void HealFull()
    {
        health = maxHealth;
    }
    public void HealthUpgrade()
    {
        maxHealth += 10;
    }
}
