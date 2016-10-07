using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Competence
    {
        public string Nom { get; private set; }
        public bool AttaqueBasique { get; private set; }
        public Cible Cible { get; private set; }
        public Effet[] Effets { get; private set; }
        public bool Negatif { get; private set; }
        public Competence(string Nom, Cible Cible, bool Negatif, bool AttaqueBasique, Effet[] Effets)
        {
            this.Nom = Nom;
            this.Cible = Cible;
            this.Negatif = Negatif;
            this.AttaqueBasique = AttaqueBasique;
            this.Effets = Effets;
        }
    }
    public class CompEtat : Competence
    {
        public Etat Etat { get; private set; }
        public CompEtat(string Nom, Cible Cible, bool Negatif, Effet[] Effets, Etat Etat) : base(Nom, Cible, Negatif, false, Effets)
        {
            this.Etat = Etat;
        }
    }
    public class CompAutre : Competence
    {
        public int ID { get; private set; }
        public CompAutre(string Nom, Cible Cible, bool Negatif, int ID) : base(Nom, Cible, Negatif, false, null)
        {
            this.ID = ID;
        }
    }
}
