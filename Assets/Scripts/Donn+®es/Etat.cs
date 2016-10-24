using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Etat
    {
        public int PremierTour { get; private set; }
        public int Duree { get; private set; }
        public Effet[] EffetsPermanents { get; private set; }
        public Etat(int Duree, Effet[] Permanents)
        {
            this.Duree = Duree;
            foreach(Effet e in Permanents)
            {
                bool good = true;
                if (e is EffetStat)
                {
                    EffetStat es = (EffetStat)e;
                    if (es.Affecte == Stats.PV)
                        good = false;
                    else if (es.Affecte == Stats.PC)
                        good = false;
                }
                if (good)
                    EffetsPermanents[EffetsPermanents.Count()] = e;
            }
        }
    }
    public class EtatPeriodique : Etat
    {
        public EffetStat[] EffetsParTour { get; private set; }
        public EtatPeriodique(int Duree, EffetStat[] ParTour, Effet[] Permanents) : base(Duree, Permanents)
        {
            foreach (EffetStat e in ParTour)
            {
                bool good = false;
                if (e.Affecte == Stats.PV)
                    good = true;
                if (good)
                    EffetsParTour[EffetsParTour.Count()] = e;
            }
        }
    }
}
