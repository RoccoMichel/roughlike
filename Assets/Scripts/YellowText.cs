using TMPro;
using UnityEngine;

public class YellowText : MonoBehaviour
{
    public string[] plashTexts;

    private void Start()
    {
        GetComponent<TMP_Text>().text = plashTexts[Random.Range(0, plashTexts.Length)];
    }
}
