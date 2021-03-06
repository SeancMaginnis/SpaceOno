using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string UseItem { get; set; }

        public Item(string name, string desc, string useItem)
        {
            Name = name;
            Description = desc;
            UseItem = useItem;
        }
    }
    
    
}