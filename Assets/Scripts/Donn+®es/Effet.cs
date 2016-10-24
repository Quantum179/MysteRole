using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Effet
    {
        protected Effet()
        {
            
        }
    }
    public class EffetScenarise : Effet
    {
        public int ID { get; private set; }
        public EffetScenarise(int id) : base()
        {
            ID = id;
        }
    }
    public class EffetStat : Effet
    {
        public Stats Affecte { get; private set; }
        public uint Montant { get; private set; }
        public bool Negatif { get; private set; }
        public EffetStat(bool Negatif, Stats Affecte, uint Montant) : base()
        {
            this.Negatif = Negatif;
            this.Affecte = Affecte;
            this.Montant = Montant;
        }
    }
    public class Degats : EffetStat
    {
        public Degats() : base(true, Stats.PV, 0)
        {
            
        }
    }
    public class Soins : EffetStat
    {
        public Soins() : base(false, Stats.PV, 0)
        {
            
        }
    }
}
