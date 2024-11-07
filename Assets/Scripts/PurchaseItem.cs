using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{
    [Header("Type")]
    public Purchases type;
    public enum Purchases { HealthUpgrade, HealthFull, AmmoFull, DamageUpgrade,
        Piercing, ReloadSpeed, BatteryLife, /*Upgrades*/
        Grenade, SmokeBomb, Flare, PistolGun, SniperGun, RevolverGun, UziGun, ARGun, /*Equipment*/
        SpeedUpgrade, LuckUpgrade, BiggerFlashlight, MoreEnemies, LessEnemies, SuperBoost /*Jubs Upgrades*/
    }

    [Header("References (should be empty)")]
    [SerializeField] Player player;
    [SerializeField] Inventory inventory;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        inventory = FindFirstObjectByType<Inventory>();

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
            case Purchases.DamageUpgrade:
                player.DamageUpgrade(); break;
            case Purchases.Piercing:
                player.PiercingUpgrade(); break;
            case Purchases.ReloadSpeed:
                player.FireRateUpgrade(); break;
            case Purchases.BatteryLife:
                player.BatteryUpgrade(); break;

            // Equipment
            case Purchases.AmmoFull:
                inventory.RefillAllAmmo(); break;
            case Purchases.Grenade:
                /*MISSING*/ break;
            case Purchases.SmokeBomb:
                /*MISSING*/ break;
            case Purchases.Flare:
                /*MISSING*/ break;
            case Purchases.PistolGun:
                /*MISSING*/ break;
            case Purchases.SniperGun:
                /*MISSING*/ break;
            case Purchases.RevolverGun:
                /*MISSING*/ break;
            case Purchases.UziGun:
                /*MISSING*/ break;
            case Purchases.ARGun:
                /*MISSING*/ break;

            // Jubs
            case Purchases.SpeedUpgrade:
                player.SpeedUpgrade(); break;
            case Purchases.LuckUpgrade:
                /*MISSING*/ break;
            case Purchases.BiggerFlashlight:
                player.FlashLightSizeUpgrade(); break;
            case Purchases.MoreEnemies:
                /*MISSING*/ break;
            case Purchases.LessEnemies:
                /*MISSING*/ break;
            case Purchases.SuperBoost:
                /*MISSING*/ break;
        }
    }
}
