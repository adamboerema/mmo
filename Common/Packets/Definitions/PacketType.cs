using System;
namespace Common.Definitions
{
    public enum PacketType : int
    {
        // Login
        LOGIN_REQUEST,
        LOGIN_RESPONSE,

        // Player
        PLAYER_CONNECTED,
        PLAYER_DISCONNECTED,
        PLAYER_ATTACK_OUTPUT,
        PLAYER_ATTACK_START,
        PLAYER_ATTACK_END,
        PLAYER_MOVEMENT_OUTPUT,
        PLAYER_MOVEMENT_INPUT,

        // Enemy
        ENEMY_SPAWN,
        ENEMY_MOVEMENT,
        ENEMY_ENGAGE,
        ENEMY_DISENGAGE,
        ENEMY_ATTACK,
    }
}
