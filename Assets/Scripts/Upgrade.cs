using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("Values")]
    [Tooltip("Leave empty to avoid overwrite")]
    public string name;
    [Tooltip("Leave 0 for Max Value")]
    public int maxCount;
    public int count;
    public float price;
    public float priceScaling = 1.2f;
    public PriceTypes currency;

    [Header("References")]
    public Player player;
    [SerializeField] Shopkeeper shopkeeper;
    [SerializeField] Button button;
    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text nameText;
    public enum PriceTypes { dekurenzi, jubs }

    private void Start()
    {
        if (player == null)
        {
            try { player = GameObject.FindWithTag("Player").GetComponent<Player>();  }
            catch { player = shopkeeper.currentPlayer; }
        }

        if (!string.IsNullOrEmpty(name)) nameText.text = name;
        if (maxCount == 0) maxCount = int.MaxValue;

        RefreshValues();
    }

    private void FixedUpdate()
    {
        RefreshValues();
    }

    public void Purchase()
    {
        if (currency == PriceTypes.jubs) PlayerPrefs.SetFloat("jubs", shopkeeper.jubs -= price);
        else player.dekurenzi -= price;

        price = Mathf.Ceil(price * priceScaling);
        count++;
        RefreshValues();
        shopkeeper.RefreshValues();
    }

    public void RefreshValues()
    {
        priceText.text = $"${price}";
        countText.text = $"{count}x";

        if (currency == PriceTypes.dekurenzi)
        {
            if (player.dekurenzi >= price) button.interactable = true;
            else button.interactable = false;
        }
        else if (currency == PriceTypes.jubs)
        {
            if (PlayerPrefs.GetFloat("jubs", 0) >= price) button.interactable = true;
            else button.interactable = false;
        }

        if (count < maxCount) return;

        priceText.text = $"MAX";
        countText.text = $"{count}x";
        button.interactable = false;

        GetComponent<Image>().color = Color.cyan;
    }
}
