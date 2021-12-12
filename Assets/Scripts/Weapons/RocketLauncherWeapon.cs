using UnityEngine;

public class RocketLauncherWeapon : WeaponBase
{
    public override WeaponKind Kind => WeaponKind.RocketLauncher;

    public override void Hit(Vector3 at)
    {
        Destroy(Instantiate(ImpactEffect, at, Quaternion.identity), 2f);
    }

    public override void Shoot()
    {
        Debug.Log("Rocket launcher make DAM BABAM !!!!");
    }
}
