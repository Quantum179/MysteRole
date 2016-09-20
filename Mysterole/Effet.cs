using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Effet
    {
        protected Effet()
        {
            // TODO
        }
    }
    public class EffetScenarise : Effet
    {
        public int ID { get; private set; }
        public EffetScenarise(int id) : base()
        {
            // TODO
        }
    }
    public class EffetStat : Effet
    {
        public uint[] Affecte { get; private set; }
        public bool Negatif { get; private set; }
        public EffetStat(bool Negatif, uint[] Affecte) : base()
        {
            // TODO
        }
    }
    public class Degats : EffetStat
    {
        public Degats(uint[] Affecte) : base(true, Affecte)
        {
            // TODO
        }
    }
    public class Soins : EffetStat
    {
        public Soins(uint[] Affecte) : base(false, Affecte)
        {
            // TODO
        }
    }
}
