using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : EnemyMovement
{
    [Header("Gun")]
    public LayerMask Player;
    public float fireRate;
    public float damage;
    bool hasFired;
    bool canShoot;

    [Header("Sprits")]
    public Sprite front;
    public Sprite back;
    public Sprite side;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove)
            Rotate();

        Move();

        canShoot = dist <= stoppingDistens;
        if (canShoot)
            Shoot();
    }

    public void Shoot()
    {
        if (!hasFired)
        {
            //Shoots a ray towards the player
            Vector3 dir = -(transform.position - player.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10, Player);
            if (hit)
            {
                hit.transform.gameObject.GetComponent<Player>().TakeDamage(damage);
            }

            hasFired = true;
            StartCoroutine(SetHasFiredToFalse());
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

    public IEnumerator SetHasFiredToFalse()
    {
        yield return new WaitForSeconds(fireRate);
        hasFired = false;
    }
}
