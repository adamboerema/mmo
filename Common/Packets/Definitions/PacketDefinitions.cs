using System;
using System.Collections.Generic;
using Common.Network.Packets.Auth;
using Common.Network.Packets.Movement;
using Common.Network.Packets.Player;

namespace Common.Network.Definitions
{
    public class PacketDefinitions: IPacketDefinitions
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
