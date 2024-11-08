using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    [SerializeField] Types type;
    [SerializeField] GameObject infoText;

    public bool enemyDrop;
    enum Types { ammo, health, dekurenzi, jubs}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        int choice = Random.Range(0, 100);

        Player player = collision.GetComponent<Player>();
        GameObject info = Instantiate(infoText, transform.position, Quaternion.identity);
        info.GetComponent<DamgeText>().lifeSpan = 5;

        switch (type)
        {
            case Types.ammo:
                player.gameObject.GetComponent<Inventory>().RefillAmmoRandom();
                info.GetComponent<TMP_Text>().text = "+Ammo";
                Destroy(gameObject);
                break;

            case Types.health:
                int amount;

                if (choice > 65) amount = 10;
                else if (choice > 30) amount = 20;
                else if (choice > 5) amount = 30;
                else amount = 50;

                amount *= 1 + (PlayerPrefs.GetInt("jubsLuck", 0) / 10);

                info.GetComponent<TMP_Text>().text = $"+{amount} HP";
                player.Heal(amount);
                Destroy(gameObject);
                break;

            case Types.dekurenzi:
                int amountDekurenzi;

                if (enemyDrop)
                {
                    if (choice >= 70)
                        amountDekurenzi = Random.Range(20, 50);
                    else
                        amountDekurenzi = Random.Range(75, 150);
                }
                else
                {
                    if (choice > 65) amountDekurenzi = 250;
                    else if (choice > 30) amountDekurenzi = 500;
                    else if (choice > 5) amountDekurenzi = 1000;
                    else amountDekurenzi = 2000;
                }

                amountDekurenzi *= 1 + (PlayerPrefs.GetInt("jubsLuck", 0) / 10);

                info.GetComponent<TMP_Text>().text = $"+{amountDekurenzi}$";
                player.dekurenzi += amountDekurenzi;
                Destroy(gameObject);
                break;

            case Types.jubs:
                int amountJubs = Random.Range(1, 5);
                amountJubs += (PlayerPrefs.GetInt("jubsLuck", 0));

                PlayerPrefs.SetFloat("jubs", PlayerPrefs.GetFloat("jubs", 0) + amountJubs);

                info.GetComponent<TMP_Text>().text = $"+{amountJubs}@";                
                Destroy(gameObject);
                break;
        }
    }
}
