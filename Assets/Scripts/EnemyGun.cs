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

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        canShoot = dist <= stoppingDistens;
        if (canShoot)
            Shoot();
    }

    public void Shoot()
    {
        if (!hasFired)
        {
            //Shoots a ray twords the player
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

    public IEnumerator SetHasFiredToFalse()
    {
        yield return new WaitForSeconds(fireRate);
        hasFired = false;
    }
}
