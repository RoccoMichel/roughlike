using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Shopkeeper : MonoBehaviour
{
    [Header("Values")]
    public float dekurenzi;
    public float jubs; // PlayerPref

    [Header("References")]
    public Player currentPlayer;
    public GameObject upgradeTab;
    public GameObject equipmentTab;
    public GameObject gemTab;
    public TMP_Text dekurenziDisplay;
    public TMP_Text jubDisplay;
    GameObject playerUI;

    [Header("Developer Options")]
    [SerializeField] bool resetJubUpgrades;

    private void Start()
    {
        if (GameObject.FindWithTag("Player UI") != null) playerUI = GameObject.FindWithTag("Player UI");
        if (GameObject.FindWithTag("Player") != null)
            currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
        jubs = PlayerPrefs.GetFloat("jubs", 0);

        Leave();
        RefreshValues();
        UpgradeButton();
    }
    public void UpgradeButton()
    {
        upgradeTab.SetActive(true);
        equipmentTab.SetActive(false);
        gemTab.SetActive(false);
        RefreshValues();
    }
    public void EquipmentButton()
    {
        upgradeTab.SetActive(false);
        equipmentTab.SetActive(true);
        gemTab.SetActive(false);
        RefreshValues();
    }
    public void GemButton()
    {
        upgradeTab.SetActive(false);
        equipmentTab.SetActive(false);
        gemTab.SetActive(true);
        RefreshValues();
    }
    public void Enter()
    {
        RefreshValues();
        Time.timeScale = 0;
        if (playerUI != null) playerUI.SetActive(false);
        GetComponent<Canvas>().enabled = true;
    }
    public void Leave()
    {
        Time.timeScale = 1;
        if (playerUI != null) playerUI.SetActive(true);
        GetComponent<Canvas>().enabled = false;
    }
    public void RefreshValues()
    {
        if (currentPlayer != null) dekurenzi = currentPlayer.dekurenzi;
        jubs = PlayerPrefs.GetFloat("jubs", 0);

        dekurenziDisplay.text = $"DEKURENZI: ${dekurenzi}";
        jubDisplay.text = $"JUBS: @{jubs}";

        foreach (GameObject upgrade in GameObject.FindGameObjectsWithTag("Upgrade"))
            upgrade.GetComponent<Upgrade>().RefreshValues();
    }
    static void ResetJubsUpgrades()
    {
        PlayerPrefs.SetFloat("jubsSpeed", 4);
        PlayerPrefs.SetInt("jubsLuck", 0);
        PlayerPrefs.SetFloat("jubsFlash", 2);
        PlayerPrefs.SetInt("jubsEnemiesCount", 0);
    }

    void OnValidate()
    {
        if (resetJubUpgrades)
        {
            ResetJubsUpgrades();
            Debug.Log("Jub Upgrades have been reset");
            resetJubUpgrades = false;
        }
    }
}
