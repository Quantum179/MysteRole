using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // TODO
        }
        public Personnage[] TrouverCibles(int x, int y)
        {
            return new Joueur[]{new Joueur("", new RoleJoueur(), 1)}; // TODO
        }
        public virtual void Utiliser(int x, int y)
        {

        }
    }
    /*public class CompDegat : Competence
    {
        public Degats Degats { get; private set; }
        public CompDegat(string Nom, Cible Cible, bool AttaqueBasique, Effet[] Effets, Degats Degats) : base(Nom, Cible, AttaqueBasique)
        {
            // TODO
        }
        public override void Utiliser(Personnage[] Cibles)
        {
            // TODO
        }
    }
    public class CompSoin : Competence
    {
        public Soins Soins { get; private set; }
        public CompSoin(string Nom, Cible Cible, Effet[] Effets, Soins Soins) : base(Nom, Cible, false)
        {
            // TODO
        }
        public override void Utiliser(Personnage[] Cibles)
        {
            // TODO
        }
    }*/
    public class CompEtat : Competence
    {
        public Etat Etat { get; private set; }
        public CompEtat(string Nom, Cible Cible, bool Negatif, Effet[] Effets, Etat Aide) : base(Nom, Cible, Negatif, false, Effets)
        {
            // TODO
        }
        public override void Utiliser(int x, int y)
        {
            // TODO
        }
    }
    /*public class CompNuir : Competence
    {
        public Etat Nuir { get; private set; }
        public CompNuir(string Nom, Cible Cible, Effet[] Effets, Etat Nuir) : base(Nom, Cible, false)
        {
            // TODO
        }
        public override void Utiliser(Personnage[] Cibles)
        {
            // TODO
        }
    }*/
    public class CompAutre : Competence
    {
        public int ID { get; private set; }
        public CompAutre(string Nom, Cible Cible, bool Negatif, int ID) : base(Nom, Cible, Negatif, false, null)
        {
            // TODO
        }
        public override void Utiliser(int x, int y)
        {
            // TODO
        }
    }
}
