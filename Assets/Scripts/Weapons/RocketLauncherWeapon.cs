using UnityEngine;

public class RocketLauncherWeapon : WeaponBase
{
    public override WeaponKind Kind => WeaponKind.RocketLauncher;
    public GunfireController controller; 

    private void Awake()
    {
        MaxBulletAmount = 30;
        CurrentBulletAmount = 30;
    }

    public override void Hit(Vector3 at)
    {
        Destroy(Instantiate(ImpactEffect, at, Quaternion.identity), 2f);
    }

    public override void Shoot()
    {
        controller.FireWeapon();
        Debug.Log("Rocket launcher make DAM BABAM !!!!");
    }
}
