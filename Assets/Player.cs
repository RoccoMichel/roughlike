using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public float health;
    public float maxHealth;
    public float currency;
    public float battery;
    public float batteryDrainRate;

    [Header("Status")]
    public bool usingFlashlight;

    [Header("Movement")]
    public bool rawInput;
    public float speed;
    public Vector2 input;

    [Header("References")]
    [SerializeField] KeyCode FlashlightKey;
    Rigidbody2D rigidbody;

    private void Start()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movement
        TopDownMovement();

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
    }

    public virtual void TopDownMovement()
    {
        // GET PLAYER INPUTS
        if (rawInput) input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        input = Vector3.Normalize(input) * speed * 100 * Time.deltaTime;

        // APPLY
        rigidbody.linearVelocity = input;
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
}
