using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace RPGProjektgrupp22
{
    public class Inventory
    {
        private List<Equipable> itemsInventoryList = new List<Equipable>();
        private List<Consumable> consumablesList = new List<Consumable>();

        private Equipable firstHand;
        private Equipable secondHand;
        private Equipable helm;
        private Equipable chestArmor;


        public Inventory()
        {
            EquipStarterItems();
            RecieveStarterPotions();
        }

        private void RecieveStarterPotions()
        {
            for (int i = 0; i < 4; i++)
            {
                consumablesList.Add(new MinorHealingPotion());
            }
        }

        public void AddConsumableToList(Consumable consumable) => consumablesList.Add(consumable);

        private void EquipStarterItems()
        {
            firstHand = new StarterSword();
            secondHand = new StarterShield();
            firstHand.Equip();
            secondHand.Equip();
        }

        public void RemoveItem(Equipable item)
        {
            if (itemsInventoryList.Contains(item))
            {
                itemsInventoryList.Remove(item);
            }
            else
            {
                switch (item)
                {
                    case Weapon sword:
                        firstHand = null;
                        break;
                    case Shield shield:
                        secondHand = null;
                        break;
                    case Helmet helmet:
                        helm = null;
                        break;
                    case ChestArmor armor:
                        chestArmor = null;
                        break;

                }
            }
        }

        public string InventoryToString()
        {
            string result = "";
            result += "Equipped: \n" + EquippedItemsToString();
            result += "Items in inventory: \n";
            foreach (Equipable item in itemsInventoryList)
            {
                result += item.EquipableToString() + "\n";
            }
            result += "\nConsumables: \n";
            foreach (Consumable consumable in consumablesList)
            {
                result += consumable.ConsumableToString() + "\n";
            }
            return result;
        }

        public string SellItemsInventoryToString(List<Equipable> sellList)
        {
            string result = "";
            for (int i = 0; i < sellList.Count; i++)
            {
                result += i + 1 + ". " + sellList[i].EquipableToString() + "\n";
            }
            return result;
        }

        public List<Equipable> GetPlayerSellList()
        {
            List<Equipable> result = new List<Equipable>();
            foreach (Equipable item in itemsInventoryList)
            {
                result.Add(item);
            }
            if (firstHand != null)
            {
                result.Add(firstHand);
            }
            if (secondHand != null)
            {
                result.Add(secondHand);
            }
            if (helm != null)
            {
                result.Add(helm);
            }
            if (chestArmor != null)
            {
                result.Add(chestArmor);
            }
            return result;

        }

        private string EquippedItemsToString()
        {
            string result = "";
            if (firstHand != null)
            {
                result += firstHand.EquipableToString() + "\n";
            }
            if (secondHand != null)
            {
                result += secondHand.EquipableToString() + "\n";
            }
            if (helm != null)
            {
                result += helm.EquipableToString() + "\n";
            }
            if (chestArmor != null)
            {
                result += chestArmor.EquipableToString() + "\n";
            }
            return result;
        }

        public void Interact()
        {
            if (itemsInventoryList.Count > 0)
            {
                Console.WriteLine("Would you like to equip non-equipped items? (1 for yes, anything else for no)");
                if (int.TryParse(Console.ReadLine(), out int input) && input == 1)
                {
                    for (int i = 0; i < itemsInventoryList.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + itemsInventoryList[i].EquipableToString());
                    }
                    Console.Write("Please input the number of the item you would like to equip (anything else for no): ");
                    if (int.TryParse(Console.ReadLine(), out int inputForItem) && inputForItem > 0 && inputForItem <= itemsInventoryList.Count)
                    {
                        Equip(inputForItem - 1);
                        Console.WriteLine("Item swapped successfully!");
                    }
                }
            }
        }

        public int Heal(int playerHealth)
        {

            while (true)
            {
                for (int i = 0; i < consumablesList.Count; i++)
                {
                    Console.WriteLine(i + 1 + ". " + consumablesList[i].ConsumableToString());
                }
                Console.Write("\nCurrent health: " + playerHealth + "\nPlease input the number of the item you would like to consume (anything else for no): ");
                if (int.TryParse(Console.ReadLine(), out int inputForConsumable) && inputForConsumable > 0 && inputForConsumable <= consumablesList.Count)
                {
                    playerHealth += Consume(inputForConsumable - 1);
                    Console.WriteLine("Potion consumed successfully!\n");
                    if (playerHealth > 100)
                    {
                        playerHealth = 100;
                    }
                }
                else
                {
                    break;
                }
            }
            return playerHealth;

        }

        private int Consume(int index)
        {
            int hpHealed = (consumablesList[index] as HealingPotion).HPHealed;
            consumablesList.Remove(consumablesList[index]);
            return hpHealed;
        }

        private void Equip(int index)
        {
            switch (itemsInventoryList[index])
            {
                case Weapon sword:
                    Equip(index, firstHand, sword);
                    firstHand = sword;
                    firstHand.Equip();
                    break;
                case Shield shield:
                    Equip(index, secondHand, shield);
                    secondHand = shield;
                    secondHand.Equip();
                    break;
                case Helmet helmet:
                    Equip(index, helm, helmet);
                    helm = helmet;
                    helm.Equip();
                    break;
                case ChestArmor armor:
                    Equip(index, chestArmor, armor);
                    chestArmor = armor;
                    chestArmor.Equip();
                    break;

            }
        }
        private void Equip(int index, Equipable equipped, Equipable toBeEquipped)
        {
            // jag tycker man ska vända på dessa, inte negera villkor om man kan. Så 
            // equipped == null och det som ska ske då år else.  Samma i metod under
            if (equipped != null)
            {
                itemsInventoryList[index] = equipped;
                equipped.UnEquip();
            }
            else
            {
                itemsInventoryList.Remove(toBeEquipped);
            }
        }

        public int GetDamageModifier()
        {
            // Borde ni inte här använda metoden ni skapade isEquipped()
            return firstHand != null ? (firstHand as Weapon).DamageModifier : 0;
        }

        public int GetBlockChance()
        {
            if (secondHand != null) // Borde ni inte använda isEquipped()
            {
                return (secondHand as Shield).BlockChance;
            }
            return 0;
        }

        internal int GetAllAdditionalDefense()
        {
            // Alla dessa som är en Equipable borde väl kolla om de IsEquipped() istället för null?
            int result = 0;
            if (chestArmor != null)
            {
                result += (chestArmor as ChestArmor).Defense;
            }
            if (helm != null)
            {
                result += (helm as Helmet).Defense;
            }
            if (secondHand != null)
            {
                result += (secondHand as Shield).Defense;
            }
            return result;
        }

        public void AddItemToList(Equipable equipable) => itemsInventoryList.Add(equipable);
    }
}
