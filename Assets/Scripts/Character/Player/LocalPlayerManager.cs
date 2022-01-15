using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

internal class LocalPlayerManager : PlayerManager
{
    [SerializeField] private RatingUI _ratingUI;
    [SerializeField] private Text HPText;
    [SerializeField] private Text ItemCountText;
    [SerializeField] private Text BulletsText;
    [SerializeField] private Text StageDuration;
    [SerializeField] private Text StageText;

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

    private void OnStageDurationChanged()
    {
        StageDuration.text = TimeSpan.FromTicks(StageDurationTicks).ToString(@"mm\:ss");
    }

    public void OnStageIdChanged()
    {
        StageText.text = "Stage: " + (StageId == 1 ? "PVE" : "PVP");
    }

    private void Subscribe()
    {
        weaponsController.WeaponStateChange += OnWeaponStateChanged;
        healthManager.HealthChanged += OnHealthChanged;
        GrenadeCountChanged += OnGrenadeCountChanged;
        RatingManager.RatingChanged += OnRatingChanged;
        StageDurationChanged += OnStageDurationChanged;
        StageIdChanged += OnStageIdChanged;
    }

    private void Unsubcribe()
    {
        weaponsController.WeaponStateChange -= OnWeaponStateChanged;
        healthManager.HealthChanged -= OnHealthChanged;
        GrenadeCountChanged -= OnGrenadeCountChanged;
        RatingManager.RatingChanged -= OnRatingChanged;
        StageDurationChanged -= OnStageDurationChanged;
        StageIdChanged -= OnStageIdChanged;
    }

    private void OnDestroy()
    {
        Unsubcribe();
    }
}
