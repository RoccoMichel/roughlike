using TMPro;
using UnityEngine;

public class EndScrean : MonoBehaviour
{
    public GameObject trollScreen;
    public GameObject normalScreen;

    public TMP_Text jubsText;

    int trolled = 0;

    public bool reset;
    public bool checkTrolled;

    private void Start()
    {
        trolled = PlayerPrefs.GetInt("trolled", 0);

        if (trolled != 1)
            trollScreen.SetActive(true);
        else
            normalScreen.SetActive(true);

        int jubsGain = Random.Range(20, 50);
        jubsText.text = "Jubs gained: +" + jubsGain;

        PlayerPrefs.SetFloat("jubs", PlayerPrefs.GetFloat("jubs") + jubsGain);
    }

    public void Troll()
    {
        trolled = PlayerPrefs.GetInt("trolled", 0);

        if (trolled == 0)
            PlayerPrefs.SetInt("trolled", 1);

        trollScreen.SetActive(false);
        normalScreen.SetActive(true);
    }

    private void OnValidate()
    {
        if (reset)
        {
            PlayerPrefs.SetInt("trolled", 0);

            print("Reset");

            reset = false;
        }

        if (checkTrolled)
        {
            print("You have " + PlayerPrefs.GetInt("trolled"));

            checkTrolled = false;
        }
    }
}
