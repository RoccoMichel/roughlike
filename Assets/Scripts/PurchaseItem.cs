using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{
    [Header("Type")]
    public Purchases type;
    public enum Purchases { HealthUpgrade, HealthFull, AmmoFull, DamageUpgrade,
        Piercing, ReloadSpeed, BatteryLife, /*Upgrades*/
        Grenade, SniperGun, LazerGun, UziGun, ARGun, /*Equipment*/
        SpeedUpgrade, LuckUpgrade, BiggerFlashlight, MoreEnemies, LessEnemies /*Jubs Upgrades*/
    }

    [Header("References (should be empty)")]
    [SerializeField] Player player;
    [SerializeField] Inventory inventory;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        inventory = FindFirstObjectByType<Inventory>();

        if (!TryGetComponent<Button>(out var button)) return;
        
        button.onClick.AddListener(Purchase);
    }

    public void Purchase()
    {
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
                inventory.UnlockGun(5); break;
            case Purchases.LazerGun:
                inventory.UnlockGun(3); break;
            case Purchases.SniperGun:
                inventory.UnlockGun(4); break;
            case Purchases.UziGun:
                inventory.UnlockGun(2); break;
            case Purchases.ARGun:
                inventory.UnlockGun(1); break;

            // Jubs
            case Purchases.SpeedUpgrade:
                player.SpeedUpgrade(); break;
            case Purchases.LuckUpgrade:
                player.LuckUpgrade(); break;
            case Purchases.BiggerFlashlight:
                player.FlashLightSizeUpgrade(); break;
            case Purchases.MoreEnemies:
                player.IncreaseEnemiesUpgrade(); break;
            case Purchases.LessEnemies:
                player.IncreaseEnemiesUpgrade(); break;
        }

        GetComponent<Upgrade>().Purchase();
    }

    public int JubUpgradeCount()
    {
        switch (type)
        {
            case Purchases.SpeedUpgrade:
                return Mathf.FloorToInt((PlayerPrefs.GetFloat("jubsSpeed", 4) - 4) / 0.5f);
            case Purchases.LuckUpgrade:
                return PlayerPrefs.GetInt("jubsLuck", 0);
            case Purchases.BiggerFlashlight:
                return Mathf.FloorToInt((PlayerPrefs.GetFloat("jubsFlash", 2) - 2) / 0.1f);
            case Purchases.MoreEnemies:
                return PlayerPrefs.GetInt("jubsEnemiesCount", 0);
            case Purchases.LessEnemies:
                return PlayerPrefs.GetInt("jubsEnemiesCount", 0);
        }
        return 0;
    }
}