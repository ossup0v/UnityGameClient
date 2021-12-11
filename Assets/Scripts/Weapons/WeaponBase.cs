using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponBase : MonoBehaviour
{
    public GameObject WeaponPrefab;
    public GameObject BulletPrefab;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;

    private GameObject InstantiatedWeapon;
    private List<GameObject> InstantiatedBullets;

    public abstract WeaponKind Kind { get; }

    public abstract void Shoot();

    public void MakeActive(Transform parent)
    {
        if(InstantiatedWeapon == null)
            InstantiatedWeapon = Instantiate(WeaponPrefab, parent);

        if (InstantiatedWeapon!= null && InstantiatedWeapon.activeSelf == false)
            InstantiatedWeapon.SetActive(true);
    }

    public void Disable()
    {
        if (InstantiatedWeapon != null && InstantiatedWeapon.activeSelf == true)
            InstantiatedWeapon.SetActive(false);
    
    }
}
