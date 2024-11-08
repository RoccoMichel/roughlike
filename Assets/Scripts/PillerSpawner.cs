using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PillerSpawner : MonoBehaviour
{
    public float minPlayerDist = 5;
    public float spawnFrequency = 5;
    public float maxSpawnDist;
    float time;
    public float numberOfEnemySpawning = 2;

    public List<GameObject> enemys;
    List<Vector3> positions = new List<Vector3>();
    Tilemap tm;

    Transform player;

    public GameObject lrPrefab;

    [Header("Debugging")]
    public bool debug;
    public bool seeDistens;

    private void Start()
    {
        if (tm == null) tm = GameObject.FindGameObjectWithTag("floor").GetComponent<Tilemap>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        for (int x = tm.cellBounds.xMin; x < tm.cellBounds.xMax; x++)
        {
            for (int y = tm.cellBounds.yMin; y < tm.cellBounds.yMax; y++)
            {
                Vector3Int localLocation = new Vector3Int(
                    x: x,
                    y: y,
                    z: 0);

                Vector3 location = tm.CellToWorld(localLocation) + new Vector3(2.5f, 2.5f, 0);
                if (tm.HasTile(localLocation))
                {
                    positions.Add(location);
                }
            }
        }

        for(int i = 0; i < positions.Count; i++)
        {
            if (Vector2.Distance(positions[i], transform.position) > maxSpawnDist)
                positions.Remove(positions[i]);
        }
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
                Vector2 pos = positions[Random.Range(0, positions.Count)];

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

    private void OnDrawGizmos()
    {
        if (seeDistens)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minPlayerDist);
        }
    }
}
