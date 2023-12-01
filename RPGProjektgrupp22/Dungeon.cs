using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    internal class Dungeon
    {
        private List<string> dungeonNameList = new List<string>() { "Ice Dungeon", "Fire Dungeon", "Noob Dungeon", "Water dungeon" };
        private Random rnd = new Random();
        private int dungeonsCleared = 0;
        private int dungeonIndex;
        private bool explored = false;

        public bool Explored
        {
            get { return explored; }
        }

        public Dungeon(int i)
        {
            dungeonIndex = i;

        }

        public bool Explore(Player player)
        {
          if (explored)
            {
                Console.WriteLine("This dungeon has already been explored");
                return false;
            }
            int randomOutcome = rnd.Next(1, 10);
            if (randomOutcome < 6)
            {
                Console.WriteLine("You encounter an enemy");
                EncounterEnemy(player);
                // Ta bort kommentar
                //Console.WriteLine("You are in the" + dungeonNameList[dungeonIndex]);
            }
// Ta bort tom rad
            else
            {

                FindChest(player);
                //Console.WriteLine("You are in the" + dungeonNameList[dungeonIndex]);
            }
            explored = true;
            return true;
// Ta bort tomma rader

        }

        public string GetDungeonName() => dungeonNameList[dungeonIndex];

// Ta bort tom rad
        private void EncounterEnemy(Player player)
        {
            Console.Clear();
            Enemy enemy = Enemy.GetRandomEnemy();
            Fight fight = new Fight(enemy, this);
            fight.Start(player);
// Ta bort tomma rader


        }

        private void FindChest(Player player)
        {
            Loot loot = new ChestLoot();
            Console.WriteLine("You found a chest containing " + loot.Gold + " gold!");
            player.RecieveLoot(loot);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        private void GoToNextStage(Player player)
        {
            Console.WriteLine("Congratulations, " + player.GetName() + "! You have completed " + player.GetLevelsCompleted() + " dungeons and advanced to the next stage.");

            // Återställ räknaren för klarade dungeons för nästa omgång
            player.ResetLevelsCompleted();
        }
// Ta bort tomma rader


    }
}
