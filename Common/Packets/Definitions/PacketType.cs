using System;
namespace Common.Definitions
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
        MOVEMENT_INPUT = 5,

        // Enemy
        ENEMY_SPAWN = 6,
        ENEMY_MOVEMENT = 7,
        ENEMY_ENGAGE = 8,
        ENEMY_DISENGAGE = 9
    }
}
