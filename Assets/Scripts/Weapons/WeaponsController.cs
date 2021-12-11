using System.Collections.Generic;
using UnityEngine;

public class WeaponsController
{
    private Dictionary<WeaponKind ,WeaponBase> availableWeapons = new Dictionary<WeaponKind, WeaponBase>();
    private WeaponKind selectedWeaponKind;

    public WeaponsController(Dictionary<WeaponKind, WeaponBase> availableWeapons, WeaponKind selectedWeapon)
    {
        this.availableWeapons = availableWeapons;
        this.selectedWeaponKind = selectedWeapon;
    }

    public WeaponBase GetSelectedWeapon()
    { 
        return availableWeapons[selectedWeaponKind];
    }

    public WeaponBase ChangeSelectedWeapon(WeaponKind kind, Transform owner)
    {
        //disable old weapon
        availableWeapons[selectedWeaponKind].Disable();

        //change weapon
        selectedWeaponKind = kind;

        //active new weapon
        availableWeapons[selectedWeaponKind].MakeActive(owner);

        return availableWeapons[selectedWeaponKind];
    }
}
