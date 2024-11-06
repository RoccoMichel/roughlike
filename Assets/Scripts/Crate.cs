using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public List<GameObject> drops;

    public int health = 4;

    public bool hit;
    public float puchTime;
    float time;

    private void Update()
    {
        if(health <= 0)
        {
            Instantiate(drops[Random.Range(0, drops.Count)], transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if (hit)
        {
            time += Time.deltaTime;
            if (time >= puchTime)
            {
                hit = false;
            }
                
        }
        else
        {
            time = 0;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && hit)
            health -= 10;
    }
}
