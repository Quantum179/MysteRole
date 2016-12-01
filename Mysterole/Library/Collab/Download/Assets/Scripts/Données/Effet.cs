using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Effet
    {
        protected Effet()
        {

        }
        public abstract void appliquer(int PUI, List<Personnage> cibles, Competence competence);
        public abstract void appliquer(int PUI, Personnage cible);
        public abstract void retirer(int PUI, Personnage cible);
        public abstract void utiliser(int PUI, List<Personnage> cibles, Competence competence);
        public abstract List<Dictionary<Stats, int>> verifier(int PUI, List<Personnage> cibles, Competence competence);
    }
    public class EffetScenarise : Effet
    {
        public int ID { get; private set; }
        public EffetScenarise(int id) : base()
        {
            ID = id;
        }

        public override void appliquer(int PUI, List<Personnage> cibles, Competence competence)
        {
            // TODO Call Event ID on 'cibles'
        }
        public override List<Dictionary<Stats, int>> verifier(int PUI, List<Personnage> cibles, Competence competence)
        {
            return new List<Dictionary<Stats, int>>(cibles.Count);
        }

        public override void appliquer(int PUI, Personnage cible)
        {
            // TODO Call Event ID Apply on 'cible'
        }
        public override void retirer(int PUI, Personnage cible)
        {
            // TODO Call Event ID Remove on 'cible'
        }
        public override void utiliser(int PUI, List<Personnage> cibles, Competence competence)
        {
            // TODO Call Event ID Utiliser on 'cibles'
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
        public override void appliquer(int PUI, List<Personnage> cibles, Competence competence)
        {
            List<Dictionary<Stats, int>> totaux = verifier(PUI, cibles, competence);
            for(int i = 0; i < cibles.Count; i++)
            {
                cibles[i].ChangerStat(Affecte, totaux[i][Affecte]);
            }
        }
        public override List<Dictionary<Stats, int>> verifier(int PUI, List<Personnage> cibles, Competence competence)
        {
            List<Dictionary<Stats, int>> effets = new List<Dictionary<Stats, int>>();
            foreach (Personnage cible in cibles)
            {
                effets.Add(new Dictionary<Stats, int>());
                effets[effets.Count-1].Add(Affecte, ((Negatif ? -1 : 1) * (int)Montant));
            }
            return effets;
        }

        public override void appliquer(int PUI, Personnage cible)
        {
            cible.ChangerStat(Affecte, (Negatif ? -1 : 1) * (int)Montant);
        }
        public override void retirer(int PUI, Personnage cible)
        {
            cible.ChangerStat(Affecte, (Negatif ? 1 : -1) * (int)Montant);
        }
        public override void utiliser(int PUI, List<Personnage> cibles, Competence competence)
        {
            return;
        }
    }
    public class Degats : EffetStat
    {
        public Degats(Stats s) : base(true, s, 0)
        {
            if (!(s == Stats.PV || s == Stats.PC))
            {
                throw new Exception("Mauvaise statistique pour le dégât.");
            }
        }
        public override void appliquer(int PUI, List<Personnage> cibles, Competence competence)
        {
            List<Dictionary<Stats, int>> degats = verifier(PUI, cibles, competence);
            for(int i = 0; i < cibles.Count; i++)
            {
                cibles[i].ChangerStat(Affecte, degats[i][Affecte]);
            }
        }
        public override List<Dictionary<Stats, int>> verifier(int PUI, List<Personnage> cibles, Competence competence)
        {
            double amp = (competence.Cible.Zone.nbrCases + cibles.Count) / ((competence.AttaqueBasique ? 3 : 2) * cibles.Count) + 0.25;
            return verifierChaque(PUI, cibles, amp);
        }
        private List<Dictionary<Stats, int>> verifierChaque(int PUI, List<Personnage> cibles, double amp)
        {
            List<Dictionary<Stats, int>> degats = new List<Dictionary<Stats, int>>();
            foreach (Personnage cible in cibles) {
                int degat = (int)Math.Ceiling(PUI * ((double)PUI / (PUI + cible.DEF)) * amp) * -1;
                degats.Add(new Dictionary<Stats, int>());
                degats[degats.Count-1].Add(Affecte, degat);
            }
            return degats;
        }
        public override void appliquer(int PUI, Personnage cible)
        {
            double amp = 1.25;
            List<Personnage> cibles = new List<Personnage>();
            cibles.Add(cible);
            List<Dictionary<Stats,int>> effets = verifierChaque(PUI, cibles, amp);
            cible.ChangerStat(Affecte, effets[0][Affecte]);
        }
        public override void retirer(int PUI, Personnage cible)
        {
            return;
        }
        public override void utiliser(int PUI, List<Personnage> cibles, Competence competence)
        {
            List<Dictionary<Stats, int>> degats = verifier(PUI, cibles, competence);
            for (int i = 0; i < cibles.Count; i++)
            {
                cibles[i].ChangerStat(Affecte, degats[i][Affecte]);
            }
        }
    }
    public class Soins : EffetStat
    {
        public Soins(Stats s) : base(false, s, 0)
        {
            if (!(s == Stats.PV || s == Stats.PC))
            {
                throw new Exception("Mauvaise statistique pour le soin.");
            }
        }
        public override void appliquer(int PUI, List<Personnage> cibles, Competence competence)
        {
            List<Dictionary<Stats, int>> soins = verifier(PUI, cibles, competence);
            for (int i = 0; i < cibles.Count; i++)
            {
                cibles[i].ChangerStat(Affecte, soins[i][Affecte]);
            }
        }
        public override List<Dictionary<Stats, int>> verifier(int PUI, List<Personnage> cibles, Competence competence)
        {
            double amp = (competence.Cible.Zone.nbrCases + cibles.Count) / ((competence.AttaqueBasique ? 3 : 2) * cibles.Count) + 0.25;
            return verifierChaque(PUI, cibles, amp);
        }
        private List<Dictionary<Stats, int>> verifierChaque(int PUI, List<Personnage> cibles, double amp)
        {
            List<Dictionary<Stats, int>> soins = new List<Dictionary<Stats, int>>();
            foreach (Personnage cible in cibles)
            {
                int soin = (int)Math.Ceiling(PUI * ((double)PUI / (PUI + cible.DEF)) * amp);
                soins.Add(new Dictionary<Stats, int>());
                soins[soins.Count - 1].Add(Affecte, soin);
            }
            return soins;
        }
        public override void appliquer(int PUI, Personnage cible)
        {
            double amp = 1.25;
            List<Personnage> cibles = new List<Personnage>();
            cibles.Add(cible);
            List<Dictionary<Stats, int>> effets = verifierChaque(PUI, cibles, amp);
            cible.ChangerStat(Affecte, effets[0][Affecte]);
        }
        public override void retirer(int PUI, Personnage cible)
        {
            return;
        }
        public override void utiliser(int PUI, List<Personnage> cibles, Competence competence)
        {
            List<Dictionary<Stats, int>> soins = verifier(PUI, cibles, competence);
            for (int i = 0; i < cibles.Count; i++)
            {
                cibles[i].ChangerStat(Affecte, soins[i][Affecte]);
            }
        }
    }
}
