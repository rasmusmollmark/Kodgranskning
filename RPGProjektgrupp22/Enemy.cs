using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    public class Enemy : Character // Enemy klassen ärver från Character klassen
    {
        private int expGiven; // Ett fält som anger hur mycket erfarenhet en fiende ger
        private int goldGiven; // Ett fält som anger hur mycket guld en fiende ger
        private static List<Enemy> enemies = new List<Enemy>();
        private EnemyLootDrop lootDrop;

        static Enemy()
        {
            enemies.Add(new Enemy("Troll", 100, 10, 5, 10, 10, 20, 10));
            enemies.Add(new Enemy("Orc", 80, 15, 10, 15, 15, 30, 15));
            enemies.Add(new Enemy("Ogre", 120, 20, 15, 20, 5, 50, 20));
        }
        
        // Strength är felstavat i metoden
        public Enemy(string name, int health, int damage, int expGiven, int goldGiven, int strenght, int defense, int speed) : base(name, health, strenght, defense, damage, speed) // En konstruktor som tar samma parametrar som Character klassen plus expGiven och goldGiven
        {
            this.expGiven = expGiven; // Tilldela expGiven till fältet
            this.goldGiven = goldGiven; // Tilldela goldGiven till fältet
            lootDrop = new EnemyLootDrop(goldGiven);
        }

        public EnemyLootDrop LootDrop => lootDrop;
        public int GetExpGiven() => expGiven; // En metod som returnerar expGiven

        public int GetGoldGiven() => goldGiven; // En metod som returnerar goldGiven

        public override void TakeDamage(int damage) // En metod som överlagrar TakeDamage metoden från Character klassen
        {
            // Behöver man inte här skriva base.TakeDamage(damage) för att den ska anropa basklassen? 
            TakeDamage(damage); // Anropa bas klassens TakeDamage metod
            Console.WriteLine(name + " took " + damage + " damage!"); // Skriv ut ett meddelande om att fienden tog skada
        }
        public static Enemy GetRandomEnemy()
        {
            Random rnd = new Random(); // Skapa ett Random objekt
            int enemyIndex = rnd.Next(0, enemies.Count); // Generera ett slumpmässigt index
            return enemies[enemyIndex]; // Returnera fienden på det indexet
        }

        public override int GetStrength() => strength;

        public override int GetDefense() => defense;

    }
}