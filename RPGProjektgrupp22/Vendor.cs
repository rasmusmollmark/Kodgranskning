using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    public abstract class Vendor
    {
        protected string name;
        protected VendorInventory inventory;
        protected List<string> Lore = new List<string>(); // ska det vara stor bokstav på Lore när det är ett variabelnamn?
        protected string[] differentTypesOfGreetings;

// Anropas denna metod någonstans ifrån
        public string InventoryToString() => inventory.InventoryToString();


        public abstract void AddLore();

        public abstract void GenerateInventory();

        public void TellGossip()
        {
            for (int i = 0; i < Lore[1].Length; i++)
            {
                Console.Write(Lore[1][i]);
                Thread.Sleep(10);
                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            Console.ReadKey();
            Console.WriteLine();
        }

        public void TellWelcome()
        {
            for (int i = 0; i < Lore[0].Length; i++)
            {
                Console.Write(Lore[0][i]);
                Thread.Sleep(10);
                if (Console.KeyAvailable)
                {
                    break;
                }
            }
            Console.ReadKey();
            Console.WriteLine();
        }

        public void GreetPlayer()
        {
            Console.WriteLine(differentTypesOfGreetings[new Random().Next(1, differentTypesOfGreetings.Length)]);
        }

        public void OptionForPlayerMeeting()
        {
            Console.WriteLine("1. Sell items\n" +
            "2. Buy items\n" +
            "3. Hear stories\n" +
            "4. Back to town");

        }

        public void SellItems(Player player)
        {
            Console.WriteLine("WARNING\nSold items cannot be bought back!");
            Thread.Sleep(1000);
            List<Equipable> sellList = player.GetSellList();
            while (true && sellList.Count > 0)
            {
                player.PrintSellInventory(sellList);
                Console.WriteLine("Please input the number of the item you would like to sell: (0 to exit vendor)");
                if (int.TryParse(Console.ReadLine(), out int input) && input > 0 && input <= sellList.Count)
                {
                    player.Gold += sellList[input - 1].SellValue;
                    player.RemoveItemFromInventory(sellList[input - 1]);
                    sellList.Remove(sellList[input - 1]);
                    Console.WriteLine("Item sold successfully! " +
                        "\nBalance: " + player.Gold);
                }
                else if (input == 0)
                {
                    return;
                }
            }
        }

        protected bool PlayerHasEnoughGold(Player player, int price) => player.Gold >= price;


        public abstract void BuyItems(Player player);







    }
}
