using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrateSpawner : MonoBehaviour
{
    public Tilemap tm;

    List<Vector3> positions = new List<Vector3>();

    public List<GameObject> crates = new List<GameObject>();
    public float spawnRate;
    float time;

    private void Start()
    {
        if (tm == null) tm = GameObject.FindGameObjectWithTag("floor").GetComponent<Tilemap>();

        for (int x = tm.cellBounds.xMin; x < tm.cellBounds.xMax; x++)
        {
            for (int y = tm.cellBounds.yMin; y < tm.cellBounds.yMax; y++)
            {
                Vector3Int localLocation = new Vector3Int(
                    x: x,
                    y: y,
                    z: 0);

                Vector3 location = tm.CellToWorld(localLocation) - new Vector3(1, 1, 0);
                if (tm.HasTile(localLocation))
                {
                    positions.Add(location);
                }
            }
        }
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time >= spawnRate)
        {
            Instantiate(crates[Random.Range(0, crates.Count)], positions[Random.Range(0, positions.Count)], Quaternion.identity);

            time = 0;
        }
    }
}
