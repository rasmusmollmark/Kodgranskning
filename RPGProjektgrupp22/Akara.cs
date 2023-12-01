using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RPGProjektgrupp22
{
    public class Akara : Vendor
    {
        public Akara()
        {
            name = "Akara";
            GenerateInventory();
            AddLore();
        }

        public override void AddLore()
        {
            string welcome = "I am  Akara, High Priestess of the Sisterhood of the Sightless Eye.\n" +
                "I welcome you, traveler, to our camp, but I'm afraid I can offer you but poor shelter within these rickety walls." +
                "\nYou see, our ancient Sisterhood has Fallen under a strange curse. The mighty Citadel from which we have \n" +
                "guarded the gates to the East for generations, has been corrupted by the evil Demoness, Andariel.\n" +
                "I still can't believe it... but she turned many of our sister Rogues against us and drove us from our ancestral home.\n" +
                "Now the last defenders of the Sisterhood are either dead or scattered throughout the wilderness." +
                "\nI implore you, stranger. Please help us. Find a way to lift this terrible curse and \n" +
                "we will pledge our loyalty to you for all time.";
            Lore.Add(welcome);

            string gossipAboutCharsi = " Charsi is young and innocent. However, I believe her Barbarian blood thrills to the \n" +
                "prospect of adventure and danger. She takes great Pride in her work and finds comfort in the fact that her \n" +
                "weapons and armor are helping to end this evil Plague.";
            Lore.Add(gossipAboutCharsi);
            differentTypesOfGreetings = new string[] {"Good day.",
"Good evening.",
"Good morning.",
"Greetings.",
"Hello.",
"The Order welcomes you.",
"Welcome back, my friend. We are still clearing the monastery, but you're welcome to stay here as long as you need.",
"Yes?"};

        }

        public override void GenerateInventory()
        {
            inventory = new VendorInventory(new Random().Next(4, 7));
        }


        public override void BuyItems(Player player)
        {
            while (true)
            {
                Console.WriteLine(inventory.InventoryToString());
                Console.WriteLine(player.Gold + " gold\nPlease input the number of the item you would like to buy: (press 0 to exit vendor)");
                if (int.TryParse(Console.ReadLine(), out int input) && input > 0 && input < 6)
                {
                    Consumable consumable = inventory.consumables[input - 1];
                    if (PlayerHasEnoughGold(player, consumable.Price))
                    {
                        player.RecieveBoughtConsumable(consumable);
                        player.Gold -= consumable.Price;
                        RemoveConsumableFromInventory(consumable);
                        Console.WriteLine("Consumable purchased successfully!\nItem bought: " + consumable.ConsumableToString() + "\nCurrent balance: " + player.Gold);

                    }
                    else
                    {
                        Console.WriteLine("Not enough gold!");
                    }
                }
                else if (input == 0)
                {
                    return;
                }
            }
        }
        private void RemoveConsumableFromInventory(Consumable consumable) => inventory.consumables.Remove(consumable);
    }
}
