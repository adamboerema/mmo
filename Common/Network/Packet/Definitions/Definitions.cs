using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions.Schema.Auth;
using Common.Network.Packet.Definitions.Schema.Movement;
using Common.Network.Packet.Definitions.Schema.Player;

namespace Common.Network.Packet.Definitions
{
    public class Definitions: IPacketDefinitions
    {
        public Dictionary<PacketType, IPacket> Packets => new Dictionary<PacketType, IPacket>
        {
            { PacketType.LOGIN_REQUEST, new LoginRequestPacket() },
            { PacketType.LOGIN_RESPONSE, new LoginResponsePacket() },
            { PacketType.PLAYER_CONNECTED, new PlayerConnectPacket() },
            { PacketType.PLAYER_DISCONNECTED, new PlayerDisconnectPacket() },
            { PacketType.MOVEMENT_OUTPUT, new MovementOutputPacket() },
            { PacketType.MOVEMENT_INPUT, new MovementInputPacket() }
        };
    }
}
