using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public class Etat
    {
        public int PremierTour { get; private set; }
        public int Duree { get; private set; }
        public Effet[] EffetsPermanents { get; private set; }
        public Etat(int PremierTour, int Duree, Effet[] Permanents)
        {
            // TODO
        }
    }
    public class EtatPeriodique : Etat
    {
        public Effet[] EffetsParTour { get; private set; }
        public EtatPeriodique(int PremierTour, int Duree, Effet[] ParTour, Effet[] Permanents) : base(PremierTour, Duree, Permanents)
        {
            // TODO
        }
    }
}
