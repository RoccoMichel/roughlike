using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("References")]
    public Player playerScript;
    public TMP_Text healthText;
    public TMP_Text dekurenziDisplay;
    public TMP_Text jubsDisplay;
    public Slider healthBar;
    public Slider batteryBar;

    private void Start()
    {
        if (playerScript == null) playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

        // Currency Display
        dekurenziDisplay.text = $"${playerScript.dekurenzi}";
        jubsDisplay.text = $"@{PlayerPrefs.GetFloat("jubs", 0)}";

        // Inventory
    }

    public void RefreshValues()
    {
        batteryBar.maxValue = 100;
        batteryBar.minValue = 0;
        healthBar.maxValue = playerScript.maxHealth;
        healthBar.minValue = 0;
    }
}
