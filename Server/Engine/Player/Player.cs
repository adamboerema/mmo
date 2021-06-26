using System;
using Common.Network.Packet.Definitions.Schema.Movement;

namespace Server.Engine.Player
{
    public class Player
    {
        public string Id { get; set; }

        public Character Character { get; set; }
    }

    public class Character
    {

        public string Name { get; set; }

        public MovementType MovementType { get; set;}
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
