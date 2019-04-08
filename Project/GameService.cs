using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Breathing { get; set; }


    public void StartGame()
    {
      Setup();
      Console.Clear();
      Console.WriteLine(@"
                     `. ___
                    __,' __`.                _..----....____
        __...--.'``;.   ,.   ;``--..__     .'    ,-._    _.-'
  _..-''-------'   `'   `'   `'     O ``-''._   (,;') _,'
,'________________                          \`-._`-','
 `._              ```````````------...___   '-.._'-:
    ```--.._      ,.                     ````--...__\-.
            `.--. `-`                       ____    |  |`
              `. `.                       ,'`````.  ;  ;`
                `._`.        __________   `.      \'__/`
                   `-:._____/______/___/____`.     \  `
                               |       `._    `.    \
                               `._________`-.   `.   `.___
                                             SSt  `------'`
");
      Console.WriteLine("Hello Investigator, I hope you've slept well we have arrived at Station Hope. I have been attempting to contact the station since we've been in hailing range.\nI've received no response and my sensors aren't picking up any signs of life. As an Investigator I know " +
                        "You've been tasked with solving mystery's,\nbut I strongly advise waiting for the fleet to arrive before proceeding. However as the ships computer I have to follow your orders what is your command?");
      Console.WriteLine(Environment.NewLine);
      Console.WriteLine("You have a choice either be a gutless Investigator who is a pathetic scaredy cat and (w)ait for the fleet that was sent out two weeks after you left to investigate," +
                        "\nor be a awesome heroic Investigator who does his/her job even in the face of the unknown and (d)ock to the space station and investigate \n" +
                        "What would you like to do (w)ait or (d)ock?");
      Decision();
      while (Breathing)
      {
        Console.Clear();

        /*Console.Clear();
        Console.WriteLine("As you shine the light around the room you see a create at the far end of the room! It has red eyes\nand mandibles very similar to those of an ant" +
                          "as you stare in dumb shock it hisses and starts to run at you!");
        Console.WriteLine("");
        Console.WriteLine("You have one chance what are you going to do?");
        continue;*/

        Console.WriteLine($"{CurrentRoom.Description}");
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Alright Investigator what's your next move?");
        GetUserInput();

      }




    }
    public void Setup()
    {
      Room ship = new Room("ship", "Your ship has been your home for the last several years as you've been in cryo sleep\non your way to investigate the loss of communications from humans first space station in a new solar system.");
      Room hall = new Room("hall", "You enter a hallway that has doors to your right and left, there is a door at the end of the hall that is slightly ajar. The stale air, eerie calm, and lack of any signs of life has your senses straining for any sound or movement.");
      Room dcontrol = new Room("dcontrol", "This is a room for controlling the docking of ships to the space station");
      Room storage = new Room("storage", @"You have entered the space suit storage room. AHHH there is an alien at the end of the room making that sound like low clicks and whistles.
      ____
     /....)
     |v v \
     |^  ^ \
     <^   ^ >
     |^    ^\\_-_
     i^     ^ \^/\
     +' (@)  _\  7
    / ..   _ -'  |
   / ..  _~  _'  |
  i    _~_.-\    |
  I--~~_~  .|    |
   ~~'| ''' |  _ |
     /|-___-| | ||
");

      //relationships
      ship.AddNearbyRoom(Direction.forward, hall);
      hall.AddNearbyRoom(Direction.right, dcontrol);
      hall.AddNearbyRoom(Direction.left, storage);
      hall.AddNearbyRoom(Direction.back, ship);
      dcontrol.AddNearbyRoom(Direction.back, hall);
      bool darkness;
      storage.AddNearbyRoom(Direction.back, hall);

      Item phaser = new Item("phaser", "Your old trusty phaser that has saved your life more than once.", "You've shot your phaser at the alien, you've missed!\nThe alien moved so fast you lose sight of it\nuntil it has your still beating heart in its hands");
      Item flashlight = new Item("flashlight", "When turned on this flashlight will illuminate dark rooms making it much easier to see.", "Good call shining the flashlight into the eyes of the alien makes it go crazy\nit proceeds to rip out it's own heart!");
      /*Item onlight = new Item("Turned on Flashlight", "The flashlight turned on making it easier to see.");
      Item gravity = new Item("Gravity button", "Hmm a button with the words GRAVITY written above it.");*/

      ship.AddItem(phaser);
      dcontrol.AddItem(flashlight);
      /*dcontrol.AddItem(gravity);
*/

      CurrentRoom = ship;
      CurrentPlayer = new Player("Investigator");
      Breathing = true;
    }



    public void GetUserInput()
    {
      string input = Console.ReadLine().ToLower();
      string[] inputs = input.Split(' ');
      string command = inputs[0];
      string option = "";
      if (inputs.Length > 1)
      {
        option = inputs[1];
      }
      switch (command)
      {
        case "go":
          Go(option);
          break;
        case "take":
          TakeItem(option);
          break;
        case "use":
          UseItem(option);
          break;
        case "inventory":
          Inventory();
          break;
        case "look":
          Look();
          break;
        case "help":
          Help();
          break;
        case "quit":
        case "q":
          Quit();
          break;
        case "reset":
        case "r":
          Reset();
          break;
        default:
          Console.Clear();
          System.Console.WriteLine("I'm a computer and I can guess what you are typing, but how bout you try again.");
          Thread.Sleep(1500);
          break;
      }
    }

    void Decision()
    {
      string decision = Console.ReadLine();
      switch (decision.ToLower()[0])
      {
        case 'd':
          Console.Clear();
          Console.WriteLine("As a true Investigator you dock to Station Hope preparing for whatever the unknown has in store for you. You don't even know the meaning of\nfear, which some would argue is a stupid way heading into the unknown but what do those people know psh.");
          Console.WriteLine(@"
+----+--------------------------------------+
| Go + Direction(forward, back, left, right)|
| Take + {itemname} i.e. take flashlight    |
| Use + {itemname}                          |
| Look -Looks around the room for items     |
| Inventory - View your items               |
| Quit - Leave game                         |
| Reset - Resets game to the start          |
+-------------------------------------------+


          ");
          Console.WriteLine("Are you ready? (yes/no)? ");
          string input = Console.ReadLine().ToLower();
          if (input == "yes" || input == "y")
          {
            return;
          }
          else
          {
            Console.Clear();
            Console.WriteLine("Well fine your ships computer goes full Hal and blows the ship up that sucks....");
            Breathing = false;
          }
          break;
        case 'w':
          Console.Clear();
          Console.WriteLine("You wait for the fleet to arrive in the mean time an unknown alian race enters the system detects your " +
                            "\nship captures you and force you to play monopoly for the rest of eternity this is the fate of the faint of heart.");
          Console.WriteLine(Environment.NewLine);
          Console.WriteLine(
              "P.S. Humanity was destroyed because of your horrible decision to wait for the fleet. Good job Investigator...");
          Breathing = false;
          break;
        default:
          Console.Clear();
          Console.WriteLine("I mean I'm a computer I can guess what you are trying to tell me but that could turn out horribly, so lets try that again.\n(W)ait or (D)ock?");
          Decision();
          break;
      }
    }


    public void Help()
    {
      Console.Clear();
      Console.WriteLine(@"
+----+--------------------------------------+
| Go + Direction(forward, back, left, right)|
| Take + {itemname} i.e. take flashlight    |
| Use + {itemname}                          |
| Look -Looks around the room for items     |
| Inventory - View your items               |
| Quit - Leave game                         |
| Reset - Resets game to the start          |
+-------------------------------------------+
");
      Thread.Sleep(3500);
      return;
    }

    public void Go(string direction)
    {
      switch (direction)
      {
        case "forward":
          CurrentRoom = (Room)CurrentRoom.MovetoRoom(Direction.forward);
          break;
        case "right":
          CurrentRoom = (Room)CurrentRoom.MovetoRoom(Direction.right);
          break;
        case "left":
          CurrentRoom = (Room)CurrentRoom.MovetoRoom(Direction.left);
          break;
        case "back":
          CurrentRoom = (Room)CurrentRoom.MovetoRoom(Direction.back);
          break;
        default:
          Console.Clear();
          Console.WriteLine("Please choose from you valid directions: 'Forward', 'Right', 'Left', 'Back' ");
          Thread.Sleep(3000);
          break;
      }
    }
    

    public void TakeItem(string itemName)
    {
      Item item = SecureItem(itemName, CurrentRoom.Items);
      if (item != null)
      {
        Console.Clear();
        CurrentRoom.Items.Remove(item);
        CurrentPlayer.AddItem(item);
        Console.WriteLine($"You picked up {itemName}");
        Thread.Sleep(1750);
      }
      else
      {
        Console.Clear();
        Console.WriteLine(CurrentRoom.Items.Count > 0 ? $"What are you playing at {itemName} isn't there anymore" : "There isn't anything to take.");
        Thread.Sleep(1750);
      }
    }

    private Item SecureItem(string input, List<Item> items)
    {
      return items.Find(i => { return i.Name.ToLower() == input; });
    }

    public void UseItem(string itemName)
    {
      Item item = CurrentPlayer.Inventory.Find(i => { return itemName.ToLower() == i.Name.ToLower(); });
      if (item == null)
      {
        Console.WriteLine("Can't use what you don't have");
        return;
      }

      switch (itemName)
      {
        case "phaser":
          if (CurrentRoom.Name.ToLower() == "storage")
          {
            Console.Clear();
            Console.Write(@"You used your phaser 
|^\                      _________________/\_
|~~~|--------------~~~~~~~~~~~~~~~~,xx.~~~~~~~~\
|___|-------++++==|___|~~~~~|_____(x@x),;'//  ||
                  |~~~||    |~~~~~~~~~~~ //   ||
                   ~\(_(=)~~ ,-~-\       \  __/
                      ~~~~~\[  \ ]\       \/
                            `:  |'()       \\
                              ~~~~\ \       \\
                                   \ \       \\
                                    \ \       \\
                                     \ \       \\
                                      \ \       ||
                                       | \       ||
                                       |  \_  ___||
                                       \____( )-=~
");
            CurrentPlayer.Inventory.Remove(item);
            Thread.Sleep(3000);
            Console.WriteLine($"{item.UseItem}");
            Thread.Sleep(2000);
            Console.WriteLine(@"
▓██   ██▓ ▒█████   █    ██     ██▓     ▒█████    ██████ ▓█████ 
 ▒██  ██▒▒██▒  ██▒ ██  ▓██▒   ▓██▒    ▒██▒  ██▒▒██    ▒ ▓█   ▀ 
  ▒██ ██░▒██░  ██▒▓██  ▒██░   ▒██░    ▒██░  ██▒░ ▓██▄   ▒███   
  ░ ▐██▓░▒██   ██░▓▓█  ░██░   ▒██░    ▒██   ██░  ▒   ██▒▒▓█  ▄ 
  ░ ██▒▓░░ ████▓▒░▒▒█████▓    ░██████▒░ ████▓▒░▒██████▒▒░▒████▒
   ██▒▒▒ ░ ▒░▒░▒░ ░▒▓▒ ▒ ▒    ░ ▒░▓  ░░ ▒░▒░▒░ ▒ ▒▓▒ ▒ ░░░ ▒░ ░
 ▓██ ░▒░   ░ ▒ ▒░ ░░▒░ ░ ░    ░ ░ ▒  ░  ░ ▒ ▒░ ░ ░▒  ░ ░ ░ ░  ░
 ▒ ▒ ░░  ░ ░ ░ ▒   ░░░ ░ ░      ░ ░   ░ ░ ░ ▒  ░  ░  ░     ░   
 ░ ░         ░ ░     ░            ░  ░    ░ ░        ░     ░  ░
 ░Better luck next time..... ░                                                           
");
            Reset();
            
           
          }

          break;
        case "flashlight":
            if (CurrentRoom.Name.ToLower() == "storage")
            {
              Console.Clear();
              Console.WriteLine(@"You used the flashlight, bold move Cotton
                __    .-----, 
 .-------------(__)--'     / \  
/  ========         :     |.-|
\                   :     |'-|
 '-------------------.     \ /
                      '-----'

");
              CurrentPlayer.Inventory.Remove(item);
              Thread.Sleep(3000);
              Console.WriteLine($"{item.UseItem}");
              Thread.Sleep(2000);
              Console.WriteLine(@"
 __     ______  _    _  __          _______ _   _ 
 \ \   / / __ \| |  | | \ \        / /_   _| \ | |
  \ \_/ / |  | | |  | |  \ \  /\  / /  | | |  \| |
   \   /| |  | | |  | |   \ \/  \/ /   | | | . ` |
    | | | |__| | |__| |    \  /\  /   _| |_| |\  |
    |_|  \____/ \____/      \/  \/   |_____|_| \_|
                                                  
");
              Breathing = false;
            }
          
          break;
      }


      /*CurrentPlayer.Inventory.Remove(item);
      CurrentRoom.AddItem(item);
      switch (itemName)
      {
          case "flashlight":
              Console.WriteLine("Shining the light");

      }
*/




    }


    public void Inventory()
    {
      Console.Clear();
      Console.WriteLine($"{CurrentRoom.Description}");
      Console.WriteLine(Environment.NewLine);
      CurrentPlayer.ShowInventory();
      Console.WriteLine(Environment.NewLine);
      Console.WriteLine("Ok investigator what's your next move?");
      GetUserInput();
    }

    public void Look()
    {
      Console.Clear();
      Console.WriteLine($"{CurrentRoom.Description}");
      Console.WriteLine(Environment.NewLine);
      CurrentRoom.PrintRoomItems();
      Console.WriteLine(Environment.NewLine);
      Console.WriteLine("What is your next move Investigator?");
      GetUserInput();
    }
    public void Reset()
    {
      Breathing = false;
      Console.Clear();
      StartGame();

    }
    public void Quit()
    {
      Console.Clear();
      Console.WriteLine("I know it's scary, but that no excuse to quit! However I know we all have things to do so I guess I'll let you quit this one time...");
      Breathing = false;
    }
  }
}