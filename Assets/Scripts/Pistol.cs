using UnityEngine;

public class Pistol : Gun
{
    public KeyCode shoot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shoot) && ammo > 0)
            Shoot();
    }
}
