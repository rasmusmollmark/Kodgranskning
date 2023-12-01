using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    public class Player : Character
    {

        private int levelsCompletedThisStage = 0;
        private int levelsCompletedTotal = 0; // kommer den användas vid senare tillfälle?
        private int gold = 100;
        private int xp = 0;
        private int xpToNextLevel = 20;
        private int level = 1;

        private Inventory inventory;


        public Player(string name) : base(name, 100, 20, 20, 15, 10)
        {
            inventory = new Inventory();
        }



        public void ReceiveLoot(Loot loot, int xpGiven)
        {
            gold += loot.Gold;
            inventory.AddItemToList(loot.Item);

            xp += xpGiven;
            CheckIfLevelUp();
        }
// Receice stavas det, uppdatera samtliga, finns kortkommando för att ändra alla samtidigt, tex CTRL + F6
        public void RecieveBoughtConsumable(Consumable consumable) => inventory.AddConsumableToList(consumable);
        public void RecieveBoughtEquippable(Equipable equipable) => inventory.AddItemToList(equipable);

        private void CheckIfLevelUp()
        {
            if (xp > xpToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            xp -= xpToNextLevel;
            level++;
            xpToNextLevel *= 2;
            PrintLevelUp();
        }

        private void PrintLevelUp()
        {
            Console.WriteLine("Level up!\n"
                + name + " is now level " + level + ". \n" +
                "Xp needed for next level: (" + xp + "/" + xpToNextLevel + ")");
        }

        public void RecieveLoot(Loot loot)
        {
            gold += loot.Gold;
        }

        public int Gold { set => gold = value; get => gold; }

        public void LevelCompleted()
        {
            levelsCompletedThisStage++;
            levelsCompletedTotal++;
            damage++;
            Health += 2;
        }

        public int GetLevelsCompleted() => levelsCompletedThisStage;

        public void ResetLevelsCompleted() => levelsCompletedThisStage = 0;

        public string PrintInventory() => "\nHitpoints: " + Health + "\n" + "Gold: " + gold + "\n" + inventory.InventoryToString();

        public void InteractWithInventory()
        {
            inventory.Interact();
            Console.Write("\nPress 1 to use consumable: (anything else to exit) ");
            if (int.TryParse(Console.ReadLine(), out int result) && result == 1)
            {
                Health = inventory.Heal(Health);
            }
        }

        public override int GetStrength() => strength * inventory.GetDamageModifier();

        public override int GetDefense() => defense + inventory.GetAllAdditionalDefense();


        public bool DoesPlayerBlock() => new Random().Next(1, 101) <= inventory.GetBlockChance();

        public void PrintSellInventory(List<Equipable> sellList) => Console.WriteLine(inventory.SellItemsInventoryToString(sellList));

        public List<Equipable> GetSellList() => inventory.GetPlayerSellList();

        public void RemoveItemFromInventory(Equipable equipable) => inventory.RemoveItem(equipable);

    }

}
