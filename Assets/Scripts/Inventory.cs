using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inv;
    [HideInInspector]
    public List<int> gunsAmmo;
    [HideInInspector]
    public List<int> maxAmmo;
    public List<bool> unlocked;

    int slot;
    int curentSlot = 0;

    private void Start()
    {
        for (int i = 0; i < inv.Count; i++)
            inv[i].SetActive(true);

        for (int i = 0; i < inv.Count; i++)
            gunsAmmo.Add(inv[i].GetComponent<Gun>().ammo);

        for (int i = 0; i < inv.Count; i++)
            maxAmmo.Add(inv[i].GetComponent<Gun>().maxAmmo);

        for (int i = 0; i < inv.Count; i++)
            inv[i].SetActive(false);

        inv[0].SetActive(true);

        for (int i = 0; i < inv.Count; i++)
            unlocked.Add(true);

        unlocked[0] = true;
    }

    private void Update()
    {
        slot += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

        CheckSlot();

        if (curentSlot != slot)
        {
            SwitchWepon();
            curentSlot = slot;
        }
    }

    void SwitchWepon()
    {
        while (!unlocked[slot])
        {
            slot += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
            CheckSlot();
        }

        gunsAmmo[curentSlot] = inv[curentSlot].GetComponent<Gun>().ammo;
        inv[curentSlot].SetActive(false);

        inv[slot].SetActive(true);
        inv[slot].GetComponent<Gun>().ammo = gunsAmmo[slot];
        inv[slot].GetComponent<Gun>().ChekcDamage();
    }

    void CheckSlot()
    {
        if (slot < 0)
            slot = inv.Count - 1;
        if (slot > inv.Count - 1)
            slot = 0;
    }

    public void UnlockGun(int slot)
    {
        unlocked[slot] = true;
    }

    public void RefillAllAmmo()
    {
        for(int i = 0; i < inv.Count; i++)
            gunsAmmo[i] = maxAmmo[i];
    }

    public void RefillAmmoRandom()
    {
        int refill = Random.Range(0, inv.Count);
        while (!unlocked[refill])
        {
            refill++;

            if(refill < 0)
                refill = inv.Count - 1;
            if (refill > inv.Count - 1)
                refill = 0;
        }

        gunsAmmo[refill] += Random.Range(10, (int)(maxAmmo[refill] / 2));
        if (gunsAmmo[refill] > maxAmmo[refill])
            gunsAmmo[refill] = maxAmmo[refill];
    }
}
