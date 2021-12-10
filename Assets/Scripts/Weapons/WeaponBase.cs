using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public GameObject BulletPrefab;

    public WeaponKind Kind { get; }

    public abstract void Shoot();
}
