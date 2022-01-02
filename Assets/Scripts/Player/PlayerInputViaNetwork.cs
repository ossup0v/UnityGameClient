using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInputViaNetwork : PlayerInput
{
    [SerializeField] private NetworkPlayerInputSender _networkPlayerInputSender;

    protected override void Update()
    {
        base.Update();
        _networkPlayerInputSender.UpdatePlayerInput(this);
    }
}