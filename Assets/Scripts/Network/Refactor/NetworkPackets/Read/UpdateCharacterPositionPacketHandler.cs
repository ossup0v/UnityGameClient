using System;
using UnityEngine;

namespace Refactor
{
    [InitReadPacketHandler(typeof(Refactor.ClientRoomNetworkBytesReader))]
    public class UpdateCharacterPositionPacketHandler : NetworkReadPacketHandler<UpdateCharacterPositionReadPacket>
    {
        public override int PacketID => UpdateCharacterPositionReadPacket.PacketID_3;

        protected override UpdateCharacterPositionReadPacket CreatePacketInstance()
        {
            return new UpdateCharacterPositionReadPacket();
        }
    }

    public class UpdateCharacterPositionReadPacket : ReadPacketBase
    {
        public Guid CharacterClientID { get; private set; }
        public Vector3 CharacterPosition { get; private set; }

        public const int PacketID_3 = 3;

        public override void DeserializePacket()
        {
            CharacterClientID = this.ReadGuid();
            CharacterPosition = this.ReadVector3();    
            Debug.Log(CharacterPosition.ToString());
        }
    }
}