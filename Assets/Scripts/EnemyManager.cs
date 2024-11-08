using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> publicEnemyList = new List<GameObject>();

    public int maxEnemys;
    public bool debug;

    public static bool canSpawn = true;

    private void Awake()
    {
        enemys.Clear();
    }

    private void Update()
    {
        publicEnemyList = enemys;

        canSpawn = !(maxEnemys < enemys.Count);

        if (!debug)
            maxEnemys = (int)PlayerPrefs.GetFloat("jubsEnemies", 50);

        if(enemys.Count > maxEnemys)
        {
            for(int i = maxEnemys; i < enemys.Count; i++)
            {
                Destroy(enemys[i]);
                enemys.Remove(enemys[i]);
            }
        }
    }
}
