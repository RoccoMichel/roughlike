using UnityEngine;

public class Hand : MonoBehaviour
{
    Vector2 mus;
    Vector2 der;
    float ang;
    public Rigidbody2D rb;
    Vector2 pos;
    public float distens = 1.5f;
    public Rigidbody2D player;

    private void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (Time.timeScale == 0) return;

        mus = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        der = mus - player.position;
        ang = Mathf.Atan2(der.y, der.x) * Mathf.Rad2Deg;
        rb.rotation = ang;
        pos.y = Mathf.Sin(Mathf.Atan2(der.y, der.x)) * distens;
        pos.x = Mathf.Cos(Mathf.Atan2(der.y, der.x)) * distens;

        transform.position = player.position + pos;
    }
}
