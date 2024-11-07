using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Settings")]
    public int ammo;
    [HideInInspector]
    public float damage;
    public float baseDamage;
    public float range;
    float fireRate;
    public float baseFireRate;
    public int maxAmmo;
    bool hasFired = false;

    [Header("References")]
    public Transform tip;
    public GameObject muzzleFlash;
    public Player p;

    [Header("LayerMasks")]
    public LayerMask enemy;
    public LayerMask create;

    [Header("Debugging")]
    public bool setDamageOnStart;

    //Runes when you switch weapons
    public void CheckDamage()
    {
        damage = baseDamage + p.damage;

        fireRate = baseFireRate + p.fireRate;
    }

    private void Awake()
    {
        if (setDamageOnStart) CheckDamage();

        if (p == null)
            p = transform.GetComponentInParent<Player>();
    }

    virtual public void Shoot()
    {
        if (!hasFired)
        {
            Vector3 rot = new Vector3(0, 0, 0);
            rot.x = -transform.eulerAngles.z;
            rot.y = 90;

            Instantiate(muzzleFlash, tip.position, Quaternion.Euler(rot));

            ammo -= 1;
            RaycastHit2D hit = Physics2D.Raycast(tip.position, transform.right, range, enemy);

            if (hit)
            {
                hit.transform.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
            }

            RaycastHit2D hit2 = Physics2D.Raycast(tip.position, transform.right, range, create);

            if (hit2)
            {
                hit2.transform.gameObject.GetComponent<Crate>().health = 0;
            }

            hasFired = true;
            StartCoroutine(SetHasFiredToFalse());
        }
    }

    public void RefillAmmo()
    {
        ammo = maxAmmo;
    }

    public IEnumerator SetHasFiredToFalse()
    {
        yield return new WaitForSeconds(fireRate);
        hasFired = false;
    }
}
