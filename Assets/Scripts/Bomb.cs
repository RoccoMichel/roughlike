using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explodeTime;
    public float damage;
    public float range;
    float time;

    public GameObject particle;

    public GameObject[] enemyPos;
    public List<GameObject> doDamage;

    private void Awake()
    {
        enemyPos = GameObject.FindGameObjectsWithTag("Enemy");

        transform.localScale = Vector3.one / 10;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= explodeTime)
            Explode();
    }

    void Explode()
    {
        for (int i = 0; i < enemyPos.Length; i++)
        {
            if (Vector2.Distance(transform.position, enemyPos[i].transform.position) <= range)
                doDamage.Add(enemyPos[i]);
        }

        for (int i = 0; i < doDamage.Count; i++)
            doDamage[i].GetComponent<EnemyStats>().TakeDamage(damage);

        Instantiate(particle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
