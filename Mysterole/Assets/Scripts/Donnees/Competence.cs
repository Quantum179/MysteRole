// Programme : Compétences
// Auteur : Jean-Michel Beauvais
// Classes et fonctions pour les compétences utilisées par les personnages et les objets.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Competence
    {
        public int ID { get; private set; }
        public string Nom { get; private set; }
        public bool AttaqueBasique { get; private set; }
        public Portee Cible { get; private set; }
        public Effet[] Effets { get; private set; }
        public bool Negatif { get; private set; }
        public Competence(int ID, string Nom, Portee Cible, bool Negatif, bool AttaqueBasique, Effet[] Effets)
        {
            this.ID = ID;
            this.Nom = Nom;
            this.Cible = Cible;
            this.Negatif = Negatif;
            this.AttaqueBasique = AttaqueBasique;
            this.Effets = Effets;
        }
        public virtual void Effectuer(int PUI, List<Personnage> cibles)
        {
            foreach(Effet effet in Effets)
            {
                effet.appliquer(PUI, cibles, this);
            }
        }
        public virtual List<Dictionary<Stats, int>> Verifier(int PUI, List<Personnage> cibles)
        {
            List<Dictionary<Stats, int>> totaux = new List<Dictionary<Stats, int>>(cibles.Count);
            for (int i = 0; i < cibles.Count; i++)
            {
                totaux[i] = new Dictionary<Stats, int>();
            }
            foreach(Effet effet in Effets)
            {
                List<Dictionary<Stats, int>> total = effet.verifier(PUI, cibles, this);
                for(int i = 0; i < cibles.Count; i++)
                {
                    Dictionary<Stats, int>.Enumerator e = total[i].GetEnumerator();
                    while (e.MoveNext()) {
                        if (!totaux[i].ContainsKey(e.Current.Key))
                            totaux[i].Add(e.Current.Key, e.Current.Value);
                        else
                            totaux[i][e.Current.Key] += e.Current.Value;
                    }
                }
            }
            return totaux;
        }
    }
    public class CompEtat : Competence
    {
        public Etat Etat { get; private set; }
        public CompEtat(int ID, string Nom, Portee Cible, bool Negatif, Effet[] Effets, Etat Etat) : base(ID, Nom, Cible, Negatif, false, Effets)
        {
            this.Etat = Etat;
        }
        public override void Effectuer(int PUI, List<Personnage> cibles)
        {
            base.Effectuer(PUI, cibles);
            foreach (Personnage cible in cibles)
            {
                cible.Etats.Add(new Etat(Etat, PUI));
            }
        }
    }
    public class CompAutre : Competence
    {
        public int ID { get; private set; }
        public CompAutre(int cID, string Nom, Portee Cible, bool Negatif, int ID) : base(cID, Nom, Cible, Negatif, false, null)
        {
            this.ID = ID;
        }

        public override void Effectuer(int PUI, List<Personnage> cibles)
        {
            // TODO : Appeler Event ID
        }

        public override List<Dictionary<Stats, int>> Verifier(int PUI, List<Personnage> cibles)
        {
            // TODO : Appeler Event ID
            return new List<Dictionary<Stats, int>>();
        }
    }
}
