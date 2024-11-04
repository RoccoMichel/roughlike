using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo;
    public float damage;
    public float range;
    public float fireRate;
    bool hasFired = false;
    public Transform tip;
    public LayerMask enemy;

    public void Shoot()
    {
        if (!hasFired)
        {
            ammo -= 1;
            RaycastHit2D hit = Physics2D.Raycast(tip.position, transform.right, range, enemy);

            if (hit)
            {
                //Do damage
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
