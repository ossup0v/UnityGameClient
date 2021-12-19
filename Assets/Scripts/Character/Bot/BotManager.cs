using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public int Id { get; private set; }
    public List<GameObject> AvailableWeapons;
    public Transform WeaponPosition;
    private HealthManager _healthManager = new HealthManager();
    private WeaponsController _weaponsController;
    public HealthbarScript Healthbar;

    public void Initialize(int id, WeaponKind currentWeapon)
    {
        Id = id;

        var weapons = AvailableWeapons.Select(x => Instantiate(x, transform)).ToDictionary(x => x.GetComponent<WeaponBase>().Kind, x => x.GetComponent<WeaponBase>());

        foreach (var weapon in weapons.Values)
        {
            weapon.WeaponPrefab.enabled = false;
        }

        _weaponsController = new WeaponsController(weapons, currentWeapon);

        ChooseWeapon(currentWeapon);
    }

    public void ChooseWeapon(WeaponKind kind)
    {
        _weaponsController.ChangeSelectedWeapon(kind, WeaponPosition);
    }
    public void SetHealth(float health)
    {
        _healthManager.SetPureHealth(health);
        Healthbar.SetHealth(_healthManager.CurrentPlayerHealth);

        if (_healthManager.IsDie)
        {
            GameManager.Bots.Remove(Id);
            Destroy(gameObject);
        }
    }
    public void Shoot()
    {
        _weaponsController.GetSelectedWeapon().Shoot();
    }

    public void Hit(WeaponKind weapon, Vector3 position)
    {
        _weaponsController.GetSelectedWeapon().Hit(position);
    }
}
