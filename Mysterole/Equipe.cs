using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public class Equipe
    {
        public string Nom { get; private set;}
        public Personnage[] Membres { get; private set; }
        public Equipe(string Nom)
        {
            // TODO
        }
        public void AjoutMembre(Personnage Personne)
        {
            // TODO
        }
        public void RetirerMembre(Personnage Personne)
        {
            // TODO
        }
    }
    public class EquipeJoueur : Equipe
    {
        public EquipeJoueur() : base("")
        {
            // TODO
        }
        public void AjoutMembre(Joueur Personne)
        {
            base.AjoutMembre(Personne); // TODO?
        }
    }
}
