// Programme : Etats
// Auteur : Jean-Michel Beauvais
// Classes et fonctions pour les états applicables aux personnages par les compétences.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Etat
    {
        public int nbrTours { get; private set; }
        public string Nom { get; private set; }
        public int Duree { get; private set; }
        public Effet[] EffetsPermanents { get; private set; }
        public int PUI { get; private set; }
        public int ID { get; private set; }
        public Etat(int ID, string Nom, int Duree, Effet[] Permanents)
        {
            this.Duree = Duree;
            EffetsPermanents = new Effet[Permanents.Count()];
            for(int i = 0; i < Permanents.Count(); i++)
            {
                bool good = true;
                if (Permanents[i] is EffetStat)
                {
                    EffetStat es = (EffetStat)Permanents[i];
                    if (es.Affecte == Stats.PV)
                        good = false;
                    else if (es.Affecte == Stats.PC)
                        good = false;
                }
                if (good)
                    EffetsPermanents[i] = Permanents[i];
            }
            nbrTours = 0;
            this.Nom = Nom;
            this.ID = ID;
            this.PUI = 0;
        }
        public Etat(Etat etat, int PUI)
        {
            this.Duree = etat.Duree;
            this.EffetsPermanents = etat.EffetsPermanents;
            this.nbrTours = 0;
            this.Nom = etat.Nom;
            this.PUI = PUI;
            this.ID = etat.ID;
        }
        public virtual bool Effectuer(Personnage cible)
        {
            if (nbrTours >= Duree)
            {
                foreach (Effet e in EffetsPermanents)
                {
                    e.retirer(PUI, cible);
                }
                return false;
            }
            else if (nbrTours == 0)
            {
                foreach (Effet e in EffetsPermanents)
                {
                    e.appliquer(PUI, cible);
                }
            }
            nbrTours++;
            return true;
        }
    }
    public class EtatPeriodique : Etat
    {
        public EffetStat[] EffetsParTour { get; private set; }
        public EtatPeriodique(int ID, string Nom, int Duree, EffetStat[] ParTour, Effet[] Permanents) : base(ID, Nom, Duree, Permanents)
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

        public EtatPeriodique(EtatPeriodique etat, int PUI) : base(etat, PUI)
        {
            this.EffetsParTour = etat.EffetsParTour;
        }
        public override bool Effectuer(Personnage cible)
        {
            foreach (Effet e in EffetsParTour)
            {
                e.appliquer(PUI, cible);
            }
            return base.Effectuer(cible);
        }
    }
}
