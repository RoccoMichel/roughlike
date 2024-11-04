using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Variables")]
    public bool rawInput;
    public float speed;
    public Vector2 input;

    [Header("References")]
    Rigidbody2D rigidbody;

    private void Start()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TopDownMove();
    }

    public virtual void TopDownMove()
    {
        // GET PLAYER INPUTS
        if (rawInput) input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        input = Vector3.Normalize(input) * speed * Time.deltaTime;

        // APPLY
        transform.position += new Vector3(input.x, input.y, 0); // TEMP
    }
}
