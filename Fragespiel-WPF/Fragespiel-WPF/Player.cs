using System;

namespace Fragespiel_WPF
{
    class Player
    {
        public readonly String Name;

        public int Points { get; set; }

        public Player(String name)
        {
            Name = name;
            Points = 0;
        }
    }
}
