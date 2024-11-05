using UnityEngine;

public class SketchyGuy : MonoBehaviour
{
    public GameObject Shopkeeper;

    private void Awake()
    {
        Shopkeeper = FindAnyObjectByType<Shopkeeper>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Shopkeeper.GetComponent<Shopkeeper>().Enter(); // ADD: freezes the gameplay (player & enemies)
        Destroy(gameObject);
    }
}
