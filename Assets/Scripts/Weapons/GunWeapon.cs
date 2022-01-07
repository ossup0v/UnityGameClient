using UnityEngine;

public class GunWeapon : WeaponBase
{
    public override WeaponKind Kind => WeaponKind.Gun;

    private void Awake()
    {
        CurrentBulletAmount = 30;
        MaxBulletAmount = 30;
    }

    public override void Hit(Vector3 at)
    {
        Debug.Log($"Gun make hit at {at}");
    }

    public override void Shoot()
    {
        Debug.Log("Gun make Peoy poey poey");
    }
}
