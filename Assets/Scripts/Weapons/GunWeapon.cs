using UnityEngine;

public class GunWeapon : WeaponBase
{
    public override WeaponKind Kind => WeaponKind.Gun;

    public override void Shoot()
    {
        Debug.Log("Gun make Peoy poey poey");
    }
}
