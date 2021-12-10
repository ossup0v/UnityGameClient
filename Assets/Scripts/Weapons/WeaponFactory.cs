using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    public static WeaponFactory Instance;
    public GameObject DefaultWeapon;
    public List<GameObject> AvailableWeapons;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError($"{nameof(GameManager)} singletone error!");
            Destroy(this);
        }
    }

    public GameObject CreateWeapon(WeaponKind kind)
    {
        foreach (var weapon in AvailableWeapons)
        {
            var weaponScript = weapon.GetComponent<WeaponBase>();
            if (weaponScript == null)
            {
                Debug.LogError("In weapon factory store prefab without weaponbase script !");
                continue;
            }

            if (weaponScript.Kind == kind)
                return weapon;
        }

        Debug.LogError($"In weapon factory can't find weapon prefab with kind {kind}," +
            $" all available perfabs is {string.Join(" ", AvailableWeapons.Select(x => x.GetComponent<WeaponBase>().Kind))}!");

        return DefaultWeapon;
    }
}