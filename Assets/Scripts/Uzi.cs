using UnityEngine;

public class Uzi : Gun
{
    public KeyCode shoot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shoot) && ammo > 0)
            Shoot();
    }
}
