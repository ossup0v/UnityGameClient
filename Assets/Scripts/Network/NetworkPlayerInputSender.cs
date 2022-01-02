using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NetworkPlayerInputSender", menuName = "Network/NetworkPlayerInputSender", order = 0)]
public sealed class NetworkPlayerInputSender : NetworkClientUpdateableBase
{
    private sealed class PlayerInputToSendData
    {
        public bool ForwardKey { get; set; }
        public bool BackwardKey { get; set; }
        public bool StrafeLeftKey { get; set; }
        public bool StrafeRightKey { get; set; }
        public bool JumpKey { get; set; }
        public bool SprintKey { get; set; }
        public bool DuckKey { get; set; }

        public bool[] _inputsToSend { get; private set; } = new bool[7];

        public void PrepareDataToSend()
        {
            _inputsToSend[0] = ForwardKey;
            _inputsToSend[1] = BackwardKey;
            _inputsToSend[2] = StrafeLeftKey;
            _inputsToSend[3] = StrafeRightKey;
            _inputsToSend[4] = JumpKey;
            _inputsToSend[5] = SprintKey;
            _inputsToSend[6] = DuckKey;
        }
    }
    
    private PlayerInputToSendData _playerInputToSendData;

    public override void Init()
    {
        _playerInputToSendData = new PlayerInputToSendData();
    }

    public void UpdatePlayerInput(PlayerInput playerInput)
    {
        _playerInputToSendData.ForwardKey = playerInput.ForwardKey;
        _playerInputToSendData.BackwardKey = playerInput.BackwardKey;
        _playerInputToSendData.StrafeLeftKey = playerInput.StrafeLeftKey;
        _playerInputToSendData.StrafeRightKey = playerInput.StrafeRightKey;
        _playerInputToSendData.JumpKey = playerInput.JumpKey;
        _playerInputToSendData.SprintKey = playerInput.SprintKey;
        _playerInputToSendData.DuckKey = playerInput.DuckKey;
    }

    public override void OnUpdate()
    {
        _playerInputToSendData.PrepareDataToSend();
        // TODO: переделать отправку
        NetworkClientSendRoom.PlayerMovement(_playerInputToSendData._inputsToSend);
    }

}