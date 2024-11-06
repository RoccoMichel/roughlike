using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Types type;
    [SerializeField] GameObject infoText;
    enum Types { ammo, health, dekurenzi, jubs, items}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        int choice = Random.Range(0, 100);

        Player player = collision.GetComponent<Player>();

        switch (type)
        {
            case Types.ammo:
                player.gameObject.GetComponent<Inventory>().RefillAmmoRandom();
                Destroy(gameObject);
                break;

            case Types.health:
               

                if (choice > 65) player.Heal(10);
                else if (choice > 30) player.Heal(20);
                else if (choice > 5) player.Heal(30);
                else player.Heal(50);
                Destroy(gameObject);
                break;

            case Types.dekurenzi:

                if (choice > 65) player.dekurenzi += 250;
                else if (choice > 30) player.dekurenzi += 500;
                else if (choice > 5) player.dekurenzi += 1000;
                else player.dekurenzi += 2000;
                Destroy(gameObject);
                break;

            case Types.jubs:
                PlayerPrefs.SetFloat("jubs", PlayerPrefs.GetFloat("jubs", 0) + Random.Range(1, 5));
                Destroy(gameObject);
                break;
            case Types.items:
                break;

        }
    }
}
