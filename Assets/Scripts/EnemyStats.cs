using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EnemyStats : MonoBehaviour
{
    public float health;

    public List<GameObject> drops;

    public GameObject damageTextObj;
    TMP_Text damageText;

    public Color bigDamage = new Color(193, 20, 20);
    public Color medeumDamage = new Color(182, 179, 26);
    public Color smalDamage = new Color(255, 255, 255);

    private void Update()
    {
        if(health <= 0)
        {
            Instantiate(drops[Random.Range(0, drops.Count)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        //Spawns in the damage text at a random position around the enemy
        Vector2 spawnPos = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        GameObject obj = Instantiate(damageTextObj, spawnPos, Quaternion.identity);
        damageText = obj.GetComponent<TMP_Text>();
        damageText.text = "-" + damage;

        if (damage >= 50)
            damageText.color = bigDamage;
        else if (damage >= 25 && damage < 50)
            damageText.color = medeumDamage;
        else
            damageText.color = smalDamage;
    }
}
