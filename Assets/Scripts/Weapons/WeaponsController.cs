using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController
{
    private Dictionary<WeaponKind, WeaponBase> _availableWeapons = new Dictionary<WeaponKind, WeaponBase>();
    private WeaponKind selectedWeaponKind;
    public event Action<WeaponBase> WeaponStateChange = delegate { };

    public WeaponsController(Dictionary<WeaponKind, WeaponBase> availableWeapons, WeaponKind selectedWeapon)
    {
        this._availableWeapons = availableWeapons;
        this.selectedWeaponKind = selectedWeapon;
    }

    public WeaponBase GetSelectedWeapon()
    {
        return _availableWeapons[selectedWeaponKind];
    }

    public WeaponBase ChangeSelectedWeapon(WeaponKind kind, Transform owner)
    {
        //disable old weapon
        _availableWeapons[selectedWeaponKind].Disable();

        //change weapon
        selectedWeaponKind = kind;

        //active new weapon
        _availableWeapons[selectedWeaponKind].MakeActive(owner);

        WeaponStateChange(_availableWeapons[selectedWeaponKind]);

        return _availableWeapons[selectedWeaponKind];
    }

    public void SetBulletAmount(WeaponKind forWeapon, int maxBulletAmount, int currentBulletAmount)
    {
        if (!_availableWeapons.TryGetValue(forWeapon, out var weapon))
        {
            Debug.LogError($"Can't find weapon with kind {forWeapon}, all weapons {string.Join(" ", _availableWeapons.Keys)}");
            return;
        }

        if (weapon.MaxBulletAmount != maxBulletAmount
            || weapon.CurrentBulletAmount != currentBulletAmount)
        {
            weapon.MaxBulletAmount = maxBulletAmount;
            weapon.CurrentBulletAmount = currentBulletAmount;

            WeaponStateChange(weapon);
        }
    }
}
