using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject[] HotbarSlots;
    int unlocked;

    [Header("References")]
    public Player playerScript;
    public Inventory inventoryScript;
    public TMP_Text healthText;
    public TMP_Text dekurenziDisplay;
    public TMP_Text jubsDisplay;
    public Slider healthBar;
    public Slider batteryBar;

    private void Start()
    {
        if (playerScript == null) playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (inventoryScript == null) inventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        RefreshValues();
    }

    private void Update()
    {
        // BATTRY UI
        batteryBar.value = playerScript.battery;

        // HEALTH UI
        float health = playerScript.health < playerScript.maxHealth / 10 ? 
            (float)System.Math.Round(playerScript.health, 1) : Mathf.Round(playerScript.health);
        healthText.text = $"{health}/{playerScript.maxHealth} HP";
        healthBar.value = playerScript.health;

        // CURRENCY DISPLAY
        dekurenziDisplay.text = $"${playerScript.dekurenzi}";
        jubsDisplay.text = $"@{PlayerPrefs.GetFloat("jubs", 0)}";

        // INVENTORY

        // Check Unlock Change
        if (unlocked != UnlockedGuns())
        {
            unlocked = UnlockedGuns();
            HotbarSetActive(false);

            for (int i = 0; i < unlocked; i++)
            {
                HotbarSlots[i].SetActive(true);
            }

            RefreshIcon();
        }

        RefreshAmmo();
    }
    public void RefreshAmmo()
    {
        int current = 0;

        for (int i = 0; i < inventoryScript.inv.Count; i++)
        {
            if (i > HotbarSlots.Length) break;

            if (!inventoryScript.unlocked[i]) continue;

            // Apply Text
            TMP_Text ammo = HotbarSlots[current].GetComponentInChildren<TMP_Text>();
            int ammoCount = inventoryScript.inv[i].GetComponent<Gun>().ammo;
            ammo.text = $"{ammoCount}";

            // Apply Color
            if (inventoryScript.currentSlot == i) // if selected
            {
                HotbarSlots[current].GetComponent<Image>().CrossFadeAlpha(0.9f, 0, true);
                ammo.alpha = 1;
            }
            else
            {
                HotbarSlots[current].GetComponent<Image>().CrossFadeAlpha(0.2f, 0, true);
                ammo.alpha = 0.7f;
            }

            if (ammoCount <= 0) ammo.color = Color.red;

            current++;
        }
    }
    public void RefreshIcon()
    {
        int current = 0;

        for (int i = 0; i < inventoryScript.inv.Count; i++)
        {
            if (i > HotbarSlots.Length) break;

            if (!inventoryScript.unlocked[i]) continue;

            foreach (Image image in HotbarSlots[current].GetComponentsInChildren<Image>())
                if (image.gameObject != HotbarSlots[current])
                    image.sprite = inventoryScript.inv[i].GetComponent<SpriteRenderer>().sprite;

            current++;
        }
    }
    public void HotbarSetActive(bool value)
    {
        foreach (GameObject slot in HotbarSlots) slot.SetActive(value);
    }
    public int UnlockedGuns()
    {
        int amount = 0;
        foreach (bool b in inventoryScript.unlocked)
            if (b) amount++;

        return amount;
    }
    public void RefreshValues()
    {
        batteryBar.maxValue = 100;
        batteryBar.minValue = 0;
        healthBar.maxValue = playerScript.maxHealth;
        healthBar.minValue = 0;
    }
}
