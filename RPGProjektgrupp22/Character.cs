using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    public abstract class Character
    {
        private int health; // Ett fält som representerar hälsa
        protected int maxHealth; // Ett fält som representerar maximal hälsa
        protected string name; // Ett fält som representerar namn
        protected int strength; // Ett fält som representerar styrka
        protected int defense; // Ett fält som representerar försvar
        protected int agility; // Ett fält som representerar smidighet
        protected int speed; // Ett fält som representerar hastighet
       // Ni ger denna ett värde men används inte sedan, om ni uppdaterar i tex Enemy så kommer den nog användas.
        protected int damage; //Ett fält som representerar skada
        protected bool isDead = false; // Ett fält som representerar om karaktären är död eller inte
        public Character(string name, int health, int damage) // används inte, kan tas bort
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
        }
        public Character(string name, int maxHealth, int strength, int defense, int damage, int speed)
        {
            this.name = name;
            this.maxHealth = maxHealth;
            this.health = maxHealth; // Sätt hälsan till maximal hälsa
            this.strength = strength;
            this.defense = defense;
            this.damage = damage;
            this.speed = speed;
        }

        public bool IsDead => isDead; // Verkar inte användas

        protected int Health
        {
            set { health = value > maxHealth ? maxHealth : value; }
            get { return health; }
        }

        public string GetName() => name;

        public int GetHealth() => health; // En metod som returnerar health

        public int GetMaxHealth() => maxHealth; // En metod som returnerar maxHealth

        public abstract int GetStrength(); // En metod som returnerar strength

        public abstract int GetDefense(); // En metod som returnerar defense

        public int GetAgility() => agility; // En metod som returnerar agility

        public int GetSpeed() => speed; // En metod som returnerar speed

        public void SetHealth(int health) // En metod som sätter health till ett nytt värde
        {
            this.health = health; // Tilldela health till fältet
            if (this.health > maxHealth) this.health = maxHealth; // Se till att health inte överstiger maxHealth
            if (this.health < 0) this.health = 0; // Se till att health inte blir negativ
            if (this.health == 0) isDead = true; // Sätt isDead till true om health är noll
        }

        public virtual void TakeDamage(int damage) // En metod som minskar health med ett skadevärde
        {
            health -= damage; // Minska health med damage
            if (health < 0) // Om health blir negativ
            {
                health = 0; // Sätt health till noll
                isDead = true; // Sätt isDead till true
            }
        }

// Används inte
        public void AttackCharacter(Character character) // En metod som attackerar en annan karaktär
        {
            character.TakeDamage(strength); // Anropa TakeDamage metoden på den andra karaktären med styrka som argument
        }

    }
}

