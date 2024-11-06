using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{
    [Header("Type")]
    public Purchases type;
    public enum Purchases { HealthUpgrade, HealthFull, AmmoFull,
    DamageUpgrade, Piercing, ReloadSpeed, BatteryLife, Grenade, SmokeBomb, SuperBoost, Flare, PistolGun, SniperGun, RevolverGun, UziGun, ARGun
    }

    [Header("References (can be empty)")]
    [SerializeField] Player player;
    //[SerializeField] Inventory inventory;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        //inventory = FindFirstObjectByType<Inventory>();

        Button button = GetComponent<Button>();
        if (button == null) return;
        
        button.onClick.AddListener(Purchase);
    }

    public void Purchase()
    {
        GetComponent<Upgrade>().Purchase();

        switch (type)
        {
            // Player
            case Purchases.HealthUpgrade:
                player.HealthUpgrade(); break;
            case Purchases.HealthFull:
                player.HealFull(); break;

            // Equipment
            case Purchases.AmmoFull:
                Debug.Log("This doesn't do anything yet"); break;

        }
    }
}
