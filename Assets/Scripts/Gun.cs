using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammo;
    [HideInInspector]
    public float damage;
    public float baseDamage;
    public float range;
    public float fireRate;
    bool hasFired = false;

    public Transform tip;
    public GameObject muzzelFlach;

    public LayerMask enemy;
    public LayerMask create;

    public Player p;

    //Runes when you switch wepons
    public void ChechDamage()
    {
        damage = baseDamage + p.damage;
    }

    public void Shoot()
    {
        if (!hasFired)
        {
            Vector3 rot = new Vector3(0, 0, 0);
            rot.x = -transform.eulerAngles.z;
            rot.y = 90;

            Instantiate(muzzelFlach, tip.position, Quaternion.Euler(rot));

            ammo -= 1;
            RaycastHit2D hit = Physics2D.Raycast(tip.position, transform.right, range, enemy);

            if (hit)
            {
                hit.transform.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            }

            RaycastHit2D hit2 = Physics2D.Raycast(tip.position, transform.right, range, create);

            if (hit2)
            {
                hit.transform.gameObject.GetComponent<Crate>().health = 0;
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
