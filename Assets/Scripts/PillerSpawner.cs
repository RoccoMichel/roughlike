using System.Collections.Generic;
using UnityEngine;

public class PillerSpawner : MonoBehaviour
{
    public float minPlayerDist = 5;
    public float spawnFrequency = 5;
    float time;
    public float numberOfEnemySpawning = 2;

    public List<GameObject> enemys;

    Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        float dist = Vector2.Distance(player.position, transform.position);

        if(dist < minPlayerDist)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        time += Time.fixedDeltaTime;

        if (time >= spawnFrequency)
        {
            for(int i = 0; i < spawnFrequency; i++)
            {
                Instantiate(enemys[Random.Range(0, enemys.Count)]);
            }
            time = 0;
        }
    }
}
