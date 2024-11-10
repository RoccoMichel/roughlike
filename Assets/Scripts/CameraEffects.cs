using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    [Header("Blood Effect")]
    [SerializeField] GameObject BloodObject;
    [SerializeField] Sprite[] bloodSprites;

    public void BloodOnScreen(int times)
    {
        for (int i = 0; i < times; i++)
        {
            GameObject blood = Instantiate(BloodObject, Camera.main.transform);
            blood.transform.localPosition = new Vector3 (Random.Range(-14f, 14), Random.Range(-6f, 6), 1);
            blood.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));

            blood.GetComponent<SpriteRenderer>().sprite = bloodSprites[Random.Range(0, bloodSprites.Length)];
            blood.GetComponent<DamgeText>().lifeSpan = Random.Range(4.5f, 4.8f);
        }
    }
}
