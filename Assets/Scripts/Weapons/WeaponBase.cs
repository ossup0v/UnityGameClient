using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public MeshRenderer WeaponPrefab;
    public GameObject BulletPrefab;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;

    private GameObject InstantiatedWeapon;
    private List<GameObject> InstantiatedBullets;

    public abstract WeaponKind Kind { get; }

    public abstract void Shoot();
    public abstract void Hit(Vector3 at);

    public void MakeActive(Transform parent)
    {
        WeaponPrefab.enabled = true;
    }

    public void Disable()
    {
        WeaponPrefab.enabled = false;
    }
}
