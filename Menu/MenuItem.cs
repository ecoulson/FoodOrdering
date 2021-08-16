using System;

namespace Menu
{
    public class MenuItem
    {
        public string Name { get; }
        public string Description { get; }
        public int Cost { get; }

        public MenuItem(string name, string description, int cost)
        {
            Name = name;
            Description = description;
            Cost = cost;
        }
    }
}
