using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
    public class Player : IPlayer
    {
        public string Name { get; set; }
        public List<Item> Inventory { get; set; }
        
        

        public Player(string name)
        {
            Name = name;
            Inventory = new List<Item>();
        }

        public void AddItem(Item item)
        {
            Inventory.Add(item);
        }

        public void ShowInventory()
        {
            if (Inventory.Count > 0)
            {
             Console.WriteLine("You currently have these items in your inventory:");
             Inventory.ForEach(item =>
             {
                 Console.WriteLine(item.Name);
                 
             });
            }
            else
            {
                Console.WriteLine("You don't have anything in your inventory.");
            }
        }
    }
}