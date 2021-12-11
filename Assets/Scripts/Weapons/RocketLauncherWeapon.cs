using UnityEngine;

public class RocketLauncherWeapon : WeaponBase
{
    public override WeaponKind Kind => WeaponKind.RocketLauncher;

    public override void Shoot()
    {
        Debug.Log("Rocket launcher make DAM BABAM !!!!");
    }
}
