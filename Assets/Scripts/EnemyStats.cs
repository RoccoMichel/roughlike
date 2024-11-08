using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EnemyStats : MonoBehaviour
{
    public float health;

    public GameObject dekurenzi;

    public GameObject damageTextObj;
    TMP_Text damageText;

    public Color bigDamage = new Color(193, 20, 20);
    public Color medeumDamage = new Color(182, 179, 26);
    public Color smalDamage = new Color(255, 255, 255);

    private void Update()
    {
        if(health <= 0)
        {
            GameObject obj = Instantiate(dekurenzi, transform.position, Quaternion.identity);
            obj.GetComponent<PickUp>().enemyDrop = true;

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        //Spawns in the damage text at a random position around the enemy
        Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), -1);
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
