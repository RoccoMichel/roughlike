using TMPro;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    [Header("Values")]
    public float dekurenzi;
    public float jubs; // PlayerPref
    float playerSpeed;

    [Header("References")]
    public Player currentPlayer;
    public GameObject upgradeTab;
    public GameObject equipmentTab;
    public GameObject gemTab;
    public TMP_Text dekurenziDisplay;
    public TMP_Text jubDisplay;
    GameObject playerUI;

    private void Start()
    {
        if (GameObject.FindWithTag("Player UI") != null) playerUI = GameObject.FindWithTag("Player UI");
        if (GameObject.FindWithTag("Player") != null)
            currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
        jubs = PlayerPrefs.GetFloat("jubs", 0);

        playerSpeed = currentPlayer.speed;

        Leave();
        RefreshValues();
        UpgradeButton();
    }
    public void UpgradeButton()
    {
        upgradeTab.SetActive(true);
        equipmentTab.SetActive(false);
        gemTab.SetActive(false);
    }
    public void EquipmentButton()
    {
        upgradeTab.SetActive(false);
        equipmentTab.SetActive(true);
        gemTab.SetActive(false);
    }
    public void GemButton()
    {
        upgradeTab.SetActive(false);
        equipmentTab.SetActive(false);
        gemTab.SetActive(true);
    }
    public void Enter()
    {
        playerUI.SetActive(false);
        GetComponent<Canvas>().enabled = true;
        playerSpeed = currentPlayer.speed;
        currentPlayer.speed = 0;
        // Animation?
    }
    public void Leave()
    {
        playerUI.SetActive(true);
        GetComponent<Canvas>().enabled = false;
        currentPlayer.speed = playerSpeed;
        // Animation?
    }
    public void RefreshValues()
    {
        if (currentPlayer != null) dekurenzi = currentPlayer.dekurenzi;
        jubs = PlayerPrefs.GetFloat("jubs", 0);

        dekurenziDisplay.text = $"DEKURENZI: {dekurenzi}";
        jubDisplay.text = $"JUBS: {jubs}";
    }
}
