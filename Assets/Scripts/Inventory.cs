using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inv;
    //[HideInInspector]
    public List<int> gunsAmmo;
    [HideInInspector]
    public List<int> maxAmmo;
    public List<bool> unlocked;

    int slot;
    public int currentSlot = 0;

    bool scrolled;

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
            unlocked.Add(false);

        unlocked[0] = true;
    }

    private void Update()
    {
        slot -= (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

        KeybordInput();

        CheckSlot();

        if (currentSlot != slot)
        {
            SwitchWeapon();
            currentSlot = slot;
        }
    }

    void SwitchWeapon()
    {
        while (!unlocked[slot])
        {
            slot -= scrolled ? -1 : (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
            CheckSlot();
        }

        gunsAmmo[currentSlot] = inv[currentSlot].GetComponent<Gun>().ammo;
        inv[currentSlot].SetActive(false);

        inv[slot].SetActive(true);
        inv[slot].GetComponent<Gun>().ammo = gunsAmmo[slot];
        inv[slot].GetComponent<Gun>().CheckDamage();
        inv[slot].GetComponent<Gun>().hasFired = false;

        scrolled = false;
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
        {
            gunsAmmo[i] = maxAmmo[i];
            inv[i].GetComponent<Gun>().RefillAmmoFull();
        }
    }

    public void RefillAmmoRandom()
    {
        int refill = Random.Range(0, inv.Count);
        int firstSelection = refill;

        while (!unlocked[refill] || inv[refill].GetComponent<Gun>().ammo == maxAmmo[refill])
        {
            refill++;

            if(refill < 0)
                refill = inv.Count - 1;
            if (refill > inv.Count - 1)
                refill = 0;

            if (refill == firstSelection) break;
        }
        
        int amount = Random.Range(10, maxAmmo[refill] / 2);

        gunsAmmo[refill] += amount;
        if (gunsAmmo[refill] > maxAmmo[refill])
            gunsAmmo[refill] = maxAmmo[refill];

        inv[refill].GetComponent<Gun>().RefillAmmoAmount(amount);
    }

    void KeybordInput()
    {
        for (int i = 0; i <= 9; i++)
            if (Input.GetKeyDown(i.ToString()))
            {
                int plus = -1;
                bool doPlus = false;
                for(int n = i - 1; n != 0; n--)
                    if (!unlocked[n])
                        doPlus = true;
                if (doPlus)
                    plus = 0;

                slot = i + plus;
                scrolled = true;
            }
    }
}