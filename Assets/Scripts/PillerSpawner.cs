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

    public GameObject lrPrefab;

    [Header("Debugging")]
    public bool debug;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (!debug)
        {
            float dist = Vector2.Distance(player.position, transform.position);

            if (dist < minPlayerDist)
            {
                Spawn();
            }
        }
        else
            if (Input.GetKey(KeyCode.Space)) Spawn();
    }

    void Spawn()
    {
        time += Time.fixedDeltaTime;

        if (time >= spawnFrequency)
        {
            for(int i = 0; i < spawnFrequency; i++)
            {
                Vector2 pos = new Vector2(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));
                Instantiate(enemys[Random.Range(0, enemys.Count)], pos, Quaternion.identity);

                LineRenderer lr = Instantiate(lrPrefab).GetComponent<LineRenderer>();

                lr.positionCount = 4;

                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, new Vector3((pos.x / 3) + Random.Range(-3, 3), (pos.y / 3) + Random.Range(-3, 3), 0));
                lr.SetPosition(2, new Vector3((pos.x / 3) * 2 + Random.Range(-3, 3), (pos.y / 3) * 2 + Random.Range(-3, 3), 0));
                lr.SetPosition(3, pos);
            }
            time = 0;
        }
    }
}
