using TMPro;
using UnityEngine;

public class DamgeText : MonoBehaviour
{
    public float lifeSpan;
    float time;
    bool doAni = false;

    public TMP_Text text;

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time >= lifeSpan)
            Destroy(gameObject);

        doAni = time >= lifeSpan/2;

        if (text == null) return;

        transform.position += new Vector3(0, 0.01f, 0);

        if (doAni)
        {
            Color32 color = text.color;
            float alpha = 1 - (time / lifeSpan);
            text.faceColor = new Color32(color.r, color.g, color.b, (byte)(alpha * 255));
        }
    }
}
