using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [Header("Values")]
    public int count;
    public float price;
    public priceTypes currency;

    [Header("References")]
    public Player player;
    [SerializeField] Shopkeeper shopkeeper;
    [SerializeField] Button button;
    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text countText;
    public enum priceTypes { dekurenzi, jubs }

    private void Start()
    {
        if (player == null)
        {
            try { player = shopkeeper.currentPlayer; }
            catch { player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); }
        }
    }

    public void Purchase()
    {
        if (currency == priceTypes.dekurenzi) PlayerPrefs.SetFloat("jubs", shopkeeper.dekurenzi -= price);
        else player.dekurenzi -= price;

        price *= 1.2f;
        count++;
        RefreshValues();
    }

    public void RefreshValues()
    {
        priceText.text = $"{price}";
        countText.text = $"{count}";

        if (currency == priceTypes.dekurenzi)
        {
            if (shopkeeper.dekurenzi >= price) button.interactable = true;
            else button.interactable = false;
        }
        else if (currency == priceTypes.jubs)
        {
            if (shopkeeper.jubs >= price) button.interactable = true;
            else button.interactable = false;
        }
    }
}
