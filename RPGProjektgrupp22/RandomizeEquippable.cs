using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    public static class RandomizeEquippable
    {
        public static Equipable GetRandom()
        {
            switch (new Random().Next(1, 5))
            {
                case 1:
                    return new ShortSword();
                case 2:
                    return new LeatherCap();
                case 3:
                    return new BreastPlate();
                case 4:
                    return new Buckler();
                default:
                    return null; // Borde ni inte skapa upp en klass här som innebär att man saknar utrustning istället för null
            }
        }
    }
}
