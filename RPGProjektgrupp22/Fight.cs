using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    internal class Fight
    {
        private Enemy enemy; // The enemy you encounter in the fight
        private Player player;
        private Random rnd = new Random(); // A random object for generating random numbers

        public Fight(Enemy enemy, Dungeon dungeon)
        {
            this.enemy = enemy; // Assign the enemy parameter to the enemy field

        }


        public void Start(Player player)
        {
            Console.WriteLine("You have encountered a " + enemy.GetName() + "!"); // Print a message about the enemy
            Console.WriteLine("Enemy's health: " + enemy.GetHealth()); // Print the enemy's health
            Console.WriteLine("Your health: " + player.GetHealth()); // Print your health

            bool fighting = true; // A boolean variable to keep track of the fight status

            while (fighting) // Loop until the fight is over
            {
                PlayerTurn(player); // Call the PlayerTurn method
                EnemyTurn(player); // Call the EnemyTurn method
                fighting = CheckWin(player); // Call the CheckWin method and assign the result to the fighting variable
            }
        }

        private void PlayerTurn(Player player)
        {
            Console.WriteLine("It's your turn. What do you want to do?");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. High risk attack");
            int choice;
            int.TryParse(Console.ReadLine(), out choice); // Get the player's choice

            switch (choice) // Handle the different choices
            {
                case 1:
                    Attack(player, enemy); // Call the Attack method with the player as the parameter and the enemy as the argument
                    break;
                case 2:
                    Attack(player, enemy, player.GetStrength());
                    break;
                default://Implementera olika typer av attacker
                    Console.WriteLine("You have to fight!"); // Print a message that the player has to fight
                    break;
            }
        }

        private void Attack(Character attacker, Character defender)
        {
            int damage = rnd.Next(attacker.GetStrength() - defender.GetDefense(), attacker.GetStrength() + defender.GetDefense()); // Calculate the damage based on the attacker's strength and the defender's defense
            if (damage < 0) damage = 0; // Make sure the damage is not negative
            if (defender is Player && (defender as Player).DoesPlayerBlock())
            {
                Console.WriteLine(defender.GetName() + " blocked!");
            }
            else
            {
                Console.WriteLine(attacker.GetName() + " attacks " + defender.GetName() + " for " + damage + " damage!"); // Print a message about the attack
                defender.SetHealth(defender.GetHealth() - damage); // Update the defender's health
                Console.WriteLine(defender.GetName() + "'s health: " + defender.GetHealth()); // Print the defender's health
            }
        }
        private void Attack(Character attacker, Character defender, int attackerStrength)
        {
            int CriticalChance = rnd.Next(1, 5);
            if (CriticalChance < 3)
            {
                Console.WriteLine("Critical hit!");
                int damage = rnd.Next(attacker.GetStrength() * 3 - defender.GetDefense(), attacker.GetStrength() * 3 + defender.GetDefense()); // Calculate the damage based on the attacker's strength and the defender's defense
                if (damage < 0) damage = 0; // Make sure the damage is not negative
                Console.WriteLine(attacker.GetName() + " attacks " + defender.GetName() + " for " + damage + " damage!"); // Print a message about the attack
                defender.SetHealth(defender.GetHealth() - damage); // Update the defender's health
                Console.WriteLine(defender.GetName() + "'s health: " + defender.GetHealth()); // Print the defender's health
            }
            else { Console.WriteLine("You missed"); }


        }

        private void EnemyTurn(Player player)
        {
            Console.WriteLine("It's the enemy's turn.");
            Attack(enemy, player); // Call the Attack method with the enemy as the parameter and the player as the argument
        }

        private bool CheckWin(Player player)
        {
            if (player.GetHealth() <= 0) // Check if the player has died
            {
                Console.WriteLine("You have been defeated by the " + enemy.GetName() + "!"); // Print a message about the loss
                Console.WriteLine("Game over!");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return false; // Return false to end the fight loop
            }
            else if (enemy.GetHealth() <= 0) // Check if the enemy has died
            {
                Console.WriteLine("You have defeated the " + enemy.GetName() + "!"); // Print a message about the win
                Console.WriteLine("You gain " + enemy.GetExpGiven() + " experience and " + enemy.GetGoldGiven() + " gold!"); // Print the rewards
                player.ReceiveLoot(enemy.LootDrop, enemy.GetExpGiven());
                Console.WriteLine("Enemy dropped " + enemy.LootDrop.Item.EquipableToString() + "!");
                //player.SetExp(player.GetExp() + enemy.GetExpGiven()); // Update the player's experience
                //player.SetGold(player.GetGold() + enemy.GetGoldGiven()); // Update the player's gold
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                return false; // Return false to end the fight loop
            }
            else // If neither has died
            {
                return true; // Return true to continue the fight loop
            }
        }
        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("You have been defeated by the mighty " + enemy.GetName() + " men du får en chans till!");
            Console.WriteLine("Klicka enter för att fortsätta");
            Console.ReadLine();
        }
        public Player GetPlayer(Player player)
        {
            return player; // Returnera fältet player
        }

        // En metod som returnerar fienden från ett Fight objekt
        public Enemy GetEnemy()
        {
            return enemy; // Returnera fältet enemy
        }
    }
}
