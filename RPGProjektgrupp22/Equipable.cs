using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGProjektgrupp22
{
    public abstract class Equipable
    {
        protected int sellValue;
        protected string type;
        private bool equipped = false;
        public Equipable(int sellValue, string type)
        {
            this.sellValue = sellValue;
            this.type = type;
        }

        public abstract string EquipableToString();

        public int Price => sellValue * 3;
        public int SellValue => sellValue;

        // Verkar inte användas
        public bool isEquipped() => equipped;

        public void Equip()
        {
            equipped = true;
        }
        public void UnEquip()
        {
            equipped = false;
        }
    }
}
