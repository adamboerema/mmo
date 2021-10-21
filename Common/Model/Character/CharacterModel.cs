using System;

namespace Common.Model.Character
{
    public class CharacterModel
    {
        public string Name { get; init; }

        /// <summary>
        /// Health and mana
        /// </summary>

        public int Health { get; set; }

        public int Mana { get; set; }

        public int MaxHealth { get; set; }

        public int MaxMana { get; set; }

        /// <summary>
        /// Stats
        /// </summary>

        public int Strength { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Constitution { get; set; }

    }
}
