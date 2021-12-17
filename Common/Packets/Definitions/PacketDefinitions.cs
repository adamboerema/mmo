using System;
using System.Collections.Generic;
using Common.Packets.ClientToServer.Auth;
using Common.Packets.ClientToServer.Player;
using Common.Packets.ServerToClient.Auth;
using Common.Packets.ServerToClient.Enemy;
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
            { PacketType.PLAYER_ATTACK_START, new PlayerAttackStartPacket() },
            { PacketType.PLAYER_ATTACK_END, new PlayerAttackEndPacket() },
            { PacketType.PLAYER_ATTACK_OUTPUT, new PlayerAttackOutputPacket() },
            { PacketType.PLAYER_MOVEMENT_OUTPUT, new PlayerMovementOutputPacket() },
            { PacketType.PLAYER_MOVEMENT_INPUT, new PlayerMovementPacket() },
            { PacketType.ENEMY_SPAWN, new EnemySpawnPacket() },
            { PacketType.ENEMY_MOVEMENT, new EnemyMovementPacket() },
            { PacketType.ENEMY_ENGAGE, new EnemyEngagePacket() },
            { PacketType.ENEMY_DISENGAGE, new EnemyDisengagePacket() },
            { PacketType.ENEMY_ATTACK, new EnemyAttackPacket() }
        };
    }
}
