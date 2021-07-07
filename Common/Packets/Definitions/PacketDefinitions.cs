using System;
using System.Collections.Generic;
using Common.Packets.ClientToServer.Auth;
using Common.Packets.ClientToServer.Movement;
using Common.Packets.ServerToClient.Auth;
using Common.Packets.ServerToClient.Movement;
using Common.Packets.ServerToClient.Player;

namespace Common.Definitions
{
    public class PacketDefinitions: IPacketDefinitions
    {
        public Dictionary<PacketType, IPacket> Packets => new Dictionary<PacketType, IPacket>
        {
            { PacketType.LOGIN_REQUEST, new LoginRequestPacket() },
            { PacketType.LOGIN_RESPONSE, new LoginResponsePacket() },
            { PacketType.PLAYER_INITIALIZED, new PlayerInitializePacket() },
            { PacketType.PLAYER_CONNECTED, new PlayerConnectPacket() },
            { PacketType.PLAYER_DISCONNECTED, new PlayerDisconnectPacket() },
            { PacketType.MOVEMENT_OUTPUT, new MovementOutputPacket() },
            { PacketType.MOVEMENT_INPUT, new MovementInputPacket() }
        };
    }
}
