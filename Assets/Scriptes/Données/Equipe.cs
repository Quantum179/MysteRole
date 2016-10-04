using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Equipe
    {
        public string Nom { get; private set;}
        public List<Personnage> Membres { get; private set; }
        public Inventaire Inventaire { get; private set; }
        public Equipe(string Nom)
        {
            this.Nom = Nom;
            Inventaire = new Inventaire();
        }
        public virtual void AjoutMembre(Personnage Personne)
        {
            Membres.Add(Personne);
        }
        public void RetirerMembre(Personnage Personne)
        {
            if (Membres.Contains(Personne))
            {
                Membres.Remove(Personne);
            }
        }
    }
    public class EquipeJoueur : Equipe
    {
        public EquipeJoueur() : base("Mysterole")
        {
            
        }
        public override void AjoutMembre(Personnage Personne)
        {
            throw new ArgumentException("L'équipe joueur ne peut recevoir que des joueurs.");
        }
        public void AjoutMembre(Joueur Personne)
        {
            bool trouve = false;
            foreach (Personnage p in Membres)
            {
                trouve = trouve || p.Role == Personne.Role;
            }
            if (!trouve)
                base.AjoutMembre(Personne);
        }
    }
}
