using UnityEngine;

public class EnemyExplode : EnemyMovement
{
    [Header("Settings")]
    public float explodeDist = 1;
    public float explodeTime = 1;
    public float damage;

    public GameObject explodeParticle;

    float time;
    public bool isExplode;

    [Header("Sprits")]
    public Sprite front;
    public Sprite back;
    public Sprite side;

    private void FixedUpdate()
    {
        Rotate();
        Move();

        if (dist <= explodeDist)
            isExplode = true;

        if (isExplode)
            Explode();
    }

    void Explode()
    {
        time += Time.fixedDeltaTime;

        if(time >= explodeTime)
        {
            if (dist <= explodeDist)
                player.gameObject.GetComponent<Player>().TakeDamage(damage);

            Instantiate(explodeParticle, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    void Rotate()
    {
        //Facing left
        if (path.desiredVelocity.x < 0 && Mathf.Abs(path.desiredVelocity.y) < 0.5f)
        {
            sr.sprite = side;
            sr.flipX = true;
        }

        //Facing right
        if (path.desiredVelocity.x > 0 && Mathf.Abs(path.desiredVelocity.y) < 0.5f)
        {
            sr.sprite = side;
            sr.flipX = false;
        }

        //Facing up
        if (path.desiredVelocity.y > 0.5f)
        {
            sr.sprite = back;
            sr.flipX = false;
        }

        //Facing down
        if (path.desiredVelocity.y < -0.5f)
        {
            sr.sprite = front;
            sr.flipX = false;
        }
    }
}
