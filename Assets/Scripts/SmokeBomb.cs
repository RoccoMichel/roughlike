using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    public float explodeTime;
    public float damage;
    public float range;
    float time;

    public GameObject[] enemyPos;
    public List<GameObject> doDamage;

    private void Awake()
    {
        enemyPos = GameObject.FindGameObjectsWithTag("Enemy");
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

        Destroy(gameObject);
    }
}
