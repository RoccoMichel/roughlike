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
    public int currentSlot = 0;

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
        slot -= (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

        NumberInput();

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
            slot += (int)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
            CheckSlot();
        }

        gunsAmmo[currentSlot] = inv[currentSlot].GetComponent<Gun>().ammo;
        inv[currentSlot].SetActive(false);

        inv[slot].SetActive(true);
        inv[slot].GetComponent<Gun>().ammo = gunsAmmo[slot];
        inv[slot].GetComponent<Gun>().CheckDamage();
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
    void NumberInput()
    {
        int desiredSlot = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1) && inv.Count >= 1) desiredSlot = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inv.Count >= 2) desiredSlot = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inv.Count >= 3) desiredSlot = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4) && inv.Count >= 4) desiredSlot = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5) && inv.Count >= 5) desiredSlot = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha6) && inv.Count >= 6) desiredSlot = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha7) && inv.Count >= 7) desiredSlot = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha8) && inv.Count >= 8) desiredSlot = 7;
        else if (Input.GetKeyDown(KeyCode.Alpha9) && inv.Count >= 9) desiredSlot = 8;

        if (desiredSlot == -1) return;

        // If desired is locked (it should go the next one in the array)
        while (desiredSlot < inv.Count && !unlocked[desiredSlot])
        {
            desiredSlot++;
        }

        // If desired is unlocked (check if there are any locked before it array selection increases)
        if (desiredSlot < inv.Count && unlocked[desiredSlot])
        {
            int difference = 0;
            int i = 0;
            foreach (bool unlock in unlocked)
            {
                if (i >= desiredSlot) break;

                if (unlock) i++;
                else difference++;
            }
            
            desiredSlot += difference;
        }

        // Apply
        if (unlocked[desiredSlot])
        {
            slot = Mathf.Clamp(desiredSlot, 0, inv.Count - 1);
        }
    }
}