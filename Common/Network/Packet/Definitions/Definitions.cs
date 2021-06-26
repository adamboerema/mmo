using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions.Schema.Auth;
using Common.Network.Packet.Definitions.Schema.Movement;
using Common.Network.Packet.Definitions.Schema.Player;

namespace Common.Network.Packet.Definitions
{
    public class Definitions: IPacketDefinitions
    {
        // Login
        public const int LOGIN_REQUEST = 0;
        public const int LOGIN_RESPONSE = 1;

        // Player
        public const int PLAYER_CONNECTED = 2;
        public const int PLAYER_DISCONNECTED = 3;

        // Movement
        public const int MOVEMENT_OUTPUT = 4;
        public const int MOVEMENT_INPUT = 5;

        public Dictionary<int, IPacket> Packets => new Dictionary<int, IPacket>
        {
            { LOGIN_REQUEST, new LoginRequestPacket() },
            { LOGIN_RESPONSE, new LoginResponsePacket() },
            { PLAYER_CONNECTED, new PlayerConnectPacket() },
            { PLAYER_DISCONNECTED, new PlayerDisconnectPacket() },
            { MOVEMENT_OUTPUT, new MovementOutputPacket() },
            { MOVEMENT_INPUT, new MovementInputPacket() }
        };
    }
}
