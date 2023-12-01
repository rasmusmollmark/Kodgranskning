using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPGProjektgrupp22
{
    public class Story
    {
        Player player;
        List<Dungeon> dungeons = new List<Dungeon>();
        bool haveSpokenWithAkara = false;
        bool haveSpokenWithCharsi = false;
        public Story()
        {
            for (int i = 0; i < 4; i++)
            {
                dungeons.Add(new Dungeon(i));
            }
            player = new Player(getPlayerName());
            printStartOfGame();
            ContinueStory();

        }

        private void ContinueStory()
        {
            while (true)
            {
                ClearWindow();
                Console.WriteLine(player.PrintInventory());

                if (PlayerWantsToVisitTown())
                {
                    GoToTown();
                }
                else
                {
                    PlayerWantsToExploreDungeon();
                }
            }
// Ta bort tom rad
        }

        private void PlayerWantsToExploreDungeon()
        {
            ClearWindow();
            while (true)
            {

                int dungeonChoice = GetPlayerDungeonChoice();
                bool dungeonCompleted = dungeons[dungeonChoice].Explore(player);

                if (dungeonCompleted)
                {
                    ContinueStory();
                    return;
                }
            }
        }

        private int GetUserInput()
        {
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                return input;
            }
            else
            {
                return -1;
            }
        }

        private void GoToTown()
        {
            while (true)
            {
                ClearWindow();
                Console.WriteLine("You're now in town.\n" +
                    "1. See inventory\n" +
                    "2. Go to vendors\n" +
                    "3. Explore dungeons");
                int input = GetUserInput();
                switch (input)
                {
                    case 1:
                        ClearWindow();
                        Console.WriteLine(player.PrintInventory());
                        player.InteractWithInventory();
                        break;
                    case 2:
                        ClearWindow();
                        Console.WriteLine("Going to vendors");
                        Thread.Sleep(1000);
                        MeetVendors();
                        break;
                    case 3:
                        PlayerWantsToExploreDungeon();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Enter a number between 1 and 3");
                        break;
                }
            }
        }

        private void MeetVendors()
        {
            Vendor vendor = AskWhichVendorToMeet();
            if (vendor == null)
            {
                return;
            }
            vendor.GreetPlayer();
            while (true)
            {
                ClearWindow();
                vendor.OptionForPlayerMeeting();
                int input = GetUserInput();
                switch (input)
                {
                    case 1:
                        vendor.SellItems(player);
                        break;
                    case 2:
                        vendor.BuyItems(player);
                        break;
                    case 3:
                        vendor.TellGossip();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        private Vendor AskWhichVendorToMeet()
        {
            while (true)
            {
                ClearWindow();
                Console.WriteLine("Which vendor do you want to see?\n" +
                    "1. Akara\n" +
                    "2. Charsi\n" +
                    "3. Back to dungeons!");
                int input = GetUserInput();
                if (input < 4 && input > 0)
                {
                    switch (input)
                    {
                        case 1:
                            if (!haveSpokenWithAkara)
                            {
                                ClearWindow();
                                Vendor akara = new Akara();
                                akara.TellWelcome();
                                haveSpokenWithAkara = true;
                            }
                            return new Akara();

                        case 2:
                            if (!haveSpokenWithCharsi)
                            {
                                ClearWindow();
                                Vendor charsi = new Charsi();
                                charsi.TellWelcome();
                                haveSpokenWithCharsi = true;
                            }
                            return new Charsi();
                        case 3: return null;
                    }
                }
                Console.WriteLine("Wrong input!");

            }
        }

        private void ClearWindow()
        {
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        }

        private bool PlayerWantsToVisitTown()
        {
            while (true)
            {
                Console.WriteLine("Do you want to visit town?\n" +
                    "1: Yes\n" +
                    "2. No, I want to explore");
                int input = GetUserInput();
                if (input < 3 && input > 0)
                {
                    switch (input)
                    {
                        case 1:
                            return true;
                        case 2:
                            return false;
                    }
                }
                Console.WriteLine("Wrong input!");
            }
        }

        private int GetPlayerDungeonChoice()
        {
            while (true)
            {
                Console.Write("Pick the dungeon you wish to explore (1-4): ");
                int input = GetUserInput();
                if (input > 0 && input < 5)
                {
                    return input - 1;
                }
                Console.WriteLine("\nWrong input!");
            }

        }
        private void ExploreDungeon(int dungeonIndex)
        {
            Dungeon selectedDungeon = dungeons[dungeonIndex];

            // Display the name of the selected dungeon
            Console.WriteLine($"You have chosen to explore " + selectedDungeon.GetDungeonName());
            selectedDungeon.Explore(player);


        }

        private string getPlayerName()
        {
            Console.Write("Please enter your characters name: ");
            return Console.ReadLine();
        }

        private void printCurrentLocation(string location)
        {

        }

        private void printStartOfGame()
        {
            Console.WriteLine("This is an rpg based game where you will face different challanges by exploring a collection of dungeons.\n" +
                "You will choose one of the provided dungeons and explore it. If you survive you will have the opportunity to go back to town.\n" +
                "In town you will be able to sell your loot and buy equipment and or consumables such as health potions.\n" +
                "\nPress any key when ready: ");

            Console.ReadKey();

        }
    }
}
