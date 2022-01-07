using UnityEngine;

public class TeleportWeapon : WeaponBase
{
    public override WeaponKind Kind => WeaponKind.Teleport;

    private void Awake()
    {
        CurrentBulletAmount = 1;
        MaxBulletAmount = 1;
    }

    public override void Hit(Vector3 at)
    {

    }

    public override void Shoot()
    {

    }
}
