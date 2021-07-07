using System;
namespace Common.Definitions
{
    public enum PacketType : int
    {
        // Login
        LOGIN_REQUEST = 0,
        LOGIN_RESPONSE = 1,

        // Player
        PLAYER_INITIALIZED = 2,
        PLAYER_CONNECTED = 3,
        PLAYER_DISCONNECTED = 4,

        // Movement
        MOVEMENT_OUTPUT = 5,
        MOVEMENT_INPUT = 6
    }
}
