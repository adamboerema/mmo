using System;
namespace Common.Network.Packet.Definitions
{
    public enum PacketType : int
    {
        // Login
        LOGIN_REQUEST = 0,
        LOGIN_RESPONSE = 1,

        // Player
        PLAYER_CONNECTED = 2,
        PLAYER_DISCONNECTED = 3,

        // Movement
        MOVEMENT_OUTPUT = 4,
        MOVEMENT_INPUT = 5
    }
}
