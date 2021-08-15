using System;
using System.Collections.Generic;
using Common.Packets.ClientToServer.Auth;
using Common.Packets.ClientToServer.Movement;
using Common.Packets.ServerToClient.Auth;
using Common.Packets.ServerToClient.Enemy;
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
            { PacketType.PLAYER_CONNECTED, new PlayerConnectPacket() },
            { PacketType.PLAYER_DISCONNECTED, new PlayerDisconnectPacket() },
            { PacketType.MOVEMENT_OUTPUT, new MovementOutputPacket() },
            { PacketType.MOVEMENT_INPUT, new MovementInputPacket() },
            { PacketType.ENEMY_SPAWN, new EnemySpawnPacket() },
            { PacketType.ENEMY_MOVEMENT, new EnemyMovementPacket() },
            { PacketType.ENEMY_ENGAGE, new EnemyEngagePacket() },
            { PacketType.ENEMY_DISENGAGE, new EnemyDisengagePacket() }
        };
    }
}
