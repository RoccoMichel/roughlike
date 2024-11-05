using TMPro;
using UnityEngine;

public class EndScrean : MonoBehaviour
{
    public GameObject trollScrean;
    public GameObject normalScrean;

    public TMP_Text jubsText;

    int trolled = 0;

    public bool reset;
    public bool checkTrolled;

    private void Start()
    {
        trolled = PlayerPrefs.GetInt("trolled");

        if (trolled != 1)
            trollScrean.SetActive(true);
        else
            normalScrean.SetActive(true);

        int jubsGain = Random.Range(20, 50);
        jubsText.text = "You Got " + jubsGain + " Jubs";

        PlayerPrefs.SetFloat("jubs", PlayerPrefs.GetFloat("jubs") + jubsGain);
    }

    public void Troll()
    {
        trolled = PlayerPrefs.GetInt("trolled");

        if (trolled == 0)
            PlayerPrefs.SetInt("trolled", 1);

        trollScrean.SetActive(false);
        normalScrean.SetActive(true);
    }

    private void OnValidate()
    {
        if (reset)
        {
            PlayerPrefs.SetInt("trolled", 0);

            print("Reseted");

            reset = false;
        }

        if (checkTrolled)
        {
            print("You have " + PlayerPrefs.GetInt("trolled"));

            checkTrolled = false;
        }
    }
}
