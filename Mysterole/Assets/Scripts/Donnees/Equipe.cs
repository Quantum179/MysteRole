// Programme : Equipe
// Auteur : Jean-Michel Beauvais
// Classes et fonctions pour les équipes de monstres et l'équipe joueur.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
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
            Membres = new List<Personnage>();
        }

        public Equipe(Equipe e)
        {
            this.Nom = e.Nom;
            Inventaire = new Inventaire();
            Membres = new List<Personnage>();
            foreach (Personnage p in e.Membres)
            {
                Membres.Add(new Personnage(p));
            }
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
            Joueur j = Personne as Joueur;
            if (j == null)
                throw new ArgumentException("L'équipe joueur ne peut recevoir que des joueurs.");
            AjoutMembre(j);
        }
        public void AjoutMembre(Joueur Personne)
        {
            if (Personne == null)
                throw new ArgumentException("Le personnage n'existe pas.");
            bool trouve = false;
            /*foreach (Personnage p in Membres)
            {
                trouve = trouve || p.Role == Personne.Role;
            }*/
            if (!trouve)
                base.AjoutMembre(Personne);
        }
        public void FinCombat()
        {
            foreach(Joueur j in Membres)
            {
                j.FinCombat();
            }
        }
    }
}
