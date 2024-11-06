using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Types type;
    enum Types { ammo, health, dekunrenzi, jubs}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();

        switch (type)
        {
            case Types.ammo:
                // Inventory Method, when charlie is done
                Destroy(gameObject);
                break;

            case Types.health:
                int choice = Random.Range(0, 100);

                if (choice > 65) player.Heal(10);
                else if (choice > 30) player.Heal(20);
                else if (choice > 5) player.Heal(30);
                else player.Heal(50);
                Destroy(gameObject);
                break;

            case Types.dekunrenzi:
                player.dekurenzi += Random.Range(50, 250); // PLACEHOLDER
                Destroy(gameObject);
                break;

            case Types.jubs:
                PlayerPrefs.SetFloat("jubs", PlayerPrefs.GetFloat("jubs", 0) + Random.Range(1, 5));
                Destroy(gameObject);
                break;
        }
    }
}
