using System;
using System.Collections.Generic;
using System.IO.Pipes;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
    public class Room : IRoom
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        
        public Dictionary<Direction, IRoom> NearbyRooms { get; set; }

        public bool Dark { get; set; } = false;
        

        public void AddNearbyRoom(Direction direction, IRoom room)
        {
            NearbyRooms.Add(direction, room);
        }
        
    

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public IRoom MovetoRoom(Direction direction)
        {
            if (NearbyRooms.ContainsKey(direction))
            {
                return NearbyRooms[direction];
            }
            Console.WriteLine("What aren't you a investigator? Why are you running into walls?");
            return (IRoom)this;
        }

        public void PrintRoomItems()
        {
            if (Items.Count > 0)
            {
                Console.WriteLine("You notice this in the room:");
                Items.ForEach(item =>
                {
                    Console.WriteLine($"{item.Name}: {item.Description}");
                });
            }
            else
            {
                Console.WriteLine("There is nothing else in this room.");
            }
        }



        public Room(string name, string desc)
        {
            Name = name;
            Description = desc;
            NearbyRooms = new Dictionary<Direction, IRoom>();
            Items = new List<Item>();
            
        }
    }

    public enum Direction
    {
        forward,
        left,
        right,
        back
    }
}