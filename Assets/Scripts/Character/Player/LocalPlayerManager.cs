using System.Text;
using UnityEngine;
using UnityEngine.UI;

internal class LocalPlayerManager : PlayerManager
{
    [SerializeField] private RatingUI _ratingUI;
    public Text HPText;
    public Text ItemCountText;
    public Text BulletsText;

    private void Awake()
    {
        InitializePostprocess += AfterInitialize;
    }

    private void OnHealthChanged(float hp)
    {
        HPText.text = $" HP: {hp}";
    }

    private void OnRatingChanged()
    {
        _ratingUI.UpdateInfo();
    }

    private void OnWeaponStateChanged(WeaponBase weapon)
    {
        BulletsText.text = $"  B: {weapon.CurrentBulletAmount}/{weapon.MaxBulletAmount}";
    }

    private void OnGrenadeCountChanged(int count)
    {
        ItemCountText.text = $" G: {count}/3";
    }

    private void AfterInitialize()
    { 
        Subscribe();
    }

    private void Subscribe()
    {
        weaponsController.WeaponStateChange += OnWeaponStateChanged;
        healthManager.HealthChanged += OnHealthChanged;
        GrenadeCountChanged += OnGrenadeCountChanged;
        RatingManager.RatingChanged += OnRatingChanged;
    }

    private void Unsubcribe()
    {
        weaponsController.WeaponStateChange -= OnWeaponStateChanged;
        healthManager.HealthChanged -= OnHealthChanged;
        GrenadeCountChanged -= OnGrenadeCountChanged;
        RatingManager.RatingChanged -= OnRatingChanged;
    }

    private void OnDestroy()
    {
        Unsubcribe();
    }
}
