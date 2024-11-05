using TMPro;
using UnityEngine;

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

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            currentPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        RefreshValues();
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
    public void Leave()
    {
        Destroy(gameObject);
    }
    public void RefreshValues()
    {
        dekurenzi = currentPlayer.dekurenzi;
        jubs = PlayerPrefs.GetFloat("jubs", 0);

        jubDisplay.text = $"JUBS: {jubs}";
        dekurenziDisplay.text = $"DEKURENZI: {dekurenziDisplay}";
    }
}
