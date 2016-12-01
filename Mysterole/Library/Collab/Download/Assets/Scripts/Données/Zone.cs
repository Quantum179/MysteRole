using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Zone
    {
        public int Largeur { get; private set; }
        public int Longueur { get; private set; }
        protected Zone(int Largeur, int Longueur)
        {
            this.Largeur = Largeur;
            this.Longueur = Longueur;
        }

        public abstract int nbrCases { get; }
    }
    public class ZonePoint : Zone
    {
        public ZonePoint() : base(0, 0)
        {
            
        }

        public override int nbrCases { get { return 1; } }
    }
    public class ZoneLosange : Zone
    {
        public ZoneLosange(int r) : base(r, 0)
        {
            // TODO
        }
        public int Rayon
        {
            get
            {
                return Largeur;
            }
        }
        public override int nbrCases
        {
            get
            {
                int facteur = 0;
                for (int i = 0; i <= Largeur; i++)
                {
                    facteur += i;
                }
                return 4 * facteur + 1;
            }
        }
    }
    public class ZoneLigne : Zone
    {
        public ZoneLigne(int Largeur, int Longueur) : base(Largeur, Longueur)
        {
            if (Largeur % 2 != 1)
                throw new ArgumentException("La Largeur d'une ligne doit être impair.");
        }
        public override int nbrCases
        {
            get
            {
                return Largeur * Longueur;
            }
        }
    }
}
