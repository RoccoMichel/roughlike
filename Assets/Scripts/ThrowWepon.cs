using UnityEngine;

public class ThrowWepon : Gun
{
    public KeyCode shoot;

    Rigidbody2D rb;
    public float throwForce;
    public GameObject obj;

    public float explodeTime;

    void Update()
    {
        if (Input.GetKeyDown(shoot) && ammo > 0)
            Throw();
    }

    void Throw()
    {
        GameObject spawnedObj = Instantiate(obj, transform.position, transform.rotation);
        rb = spawnedObj.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * throwForce);
        spawnedObj.GetComponent<Bomb>().damage = damage;
        spawnedObj.GetComponent<Bomb>().range = range;
        spawnedObj.GetComponent<Bomb>().explodeTime = explodeTime;

        ammo -= 1;
    }
}
