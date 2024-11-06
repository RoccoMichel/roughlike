using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{
    [Header("Type")]
    public Purchases type;
    public enum Purchases { HealthUpgrade, HealthFull, AmmoFull, DamageUpgrade,
        Piercing, ReloadSpeed, BatteryLife, Grenade, SmokeBomb,
        Flare, PistolGun, SniperGun, RevolverGun, UziGun, ARGun,
        SpeedUpgrade, LuckUpgrade, BiggerFlashlight, MoreEnemies, LessEnemies, SuperBoost
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
            case Purchases.DamageUpgrade:
                player.DamageUpgrade(); break;
            case Purchases.BatteryLife:
                player.BatteryUpgrade(); break;
            case Purchases.SpeedUpgrade:
                player.SpeedUpgrade(); break;


            // Equipment
            case Purchases.AmmoFull:
                Debug.Log("This doesn't do anything yet"); break;

        }
    }
}
