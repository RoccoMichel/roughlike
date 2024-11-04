using UnityEngine;

public class RemoveAfterXSeconds : MonoBehaviour
{
    public float lifeTime;
    float time;

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time >= lifeTime)
            Destroy(gameObject);
    }
}
