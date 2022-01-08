using System;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _username;
    [SerializeField] private TextMeshProUGUI _kills;
    [SerializeField] private TextMeshProUGUI _mobs;

    public Guid Id { get; set; }
    public void UpdateInfo(RatingEntity entity)
    {
        _username.text = entity.Username;
        _kills.text = entity.Killed.ToString();
        _mobs.text = entity.KilledBots.ToString();
    }
}
