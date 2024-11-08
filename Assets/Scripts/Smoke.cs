using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public List<GameObject> smokeObj = new List<GameObject>();

    public Color smokeColor;

    public float maxSize;
    public float smokeTime;
    float time;

    private void Awake()
    {
        for(int i = 0; i < smokeObj.Count; i++)
        {
            smokeObj[i].transform.position = new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y + Random.Range(-3, 3), 0);
            smokeObj[i].transform.localScale = new Vector3(0, 0, 1);
            smokeObj[i].GetComponent<SpriteRenderer>().color = smokeColor;
        }
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        for (int i = 0; i < smokeObj.Count; i++)
        {
            if (smokeObj[i].transform.localScale.x <= maxSize)
            smokeObj[i].transform.localScale += new Vector3(time, time, 0);
        }

        if (time >= smokeTime)
            Destroy(gameObject);
    }
}
