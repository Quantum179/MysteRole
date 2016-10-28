using System;
using System.Collections.Generic;

namespace Mysterole
{
	public class Quete	
	{
        //Attributs 
        private string _nom;
        private string _responsableDebut;
        private string _queteParente;
        private List<Objectif> _objectifs;
		private EtatQuete _etat;
        private List<Gain> _gains;


        //Propriétés
        public string Nom
        {
            get { return _nom; }
            private set { }
        }
        public string ResponsableDebut
        {
            get { return _responsableDebut; }
            private set { }
        }
        public string QueteParente
        {
            get { return _queteParente; }
            private set { _queteParente = value; }
        }
		public List<Objectif> Objectifs
        {
			get { return _objectifs; }
			private set { }
		}
		public EtatQuete Etat {
			get { return _etat;}

		}

        //Constructeurs
		public Quete ()
		{
            //Exemple hardcodé
            //Doit aller récupérer les données dans la base de données
            _nom = "La demande du Chef milicien";
            _responsableDebut = "Chef milicien";
            _objectifs = new List<Objectif>();
            _etat = EtatQuete.Bloquee;
        }
        public Quete(string n)
        {
            _nom = n;
            _responsableDebut = "";
            _objectifs = new List<Objectif>();
            _etat = EtatQuete.Disponible;


            _objectifs.Add(new Objectif("Parler au Sage du village", false, "Sage du village"));
            _objectifs.Add(new Objectif("Obtenir l'autorisation", false, "Sage du village"));
            _objectifs.Add(new Objectif("Retourner parler au Chef milicien", false, "Chef milicien"));
        }

		//Vérifier l'avancée de la quête (void pour l'ébauche)
		private void VerifierAvancee()
		{
			//Peut être à ajouter dans l'update
		}

        private void VerifierPrerequis()
        {
            //Trouver la quête précédent celle ci, à l'aide de la BD ou du main
            _etat = EtatQuete.Bloquee;
        
        }

        private void InitialiserQuete()
        {
            _etat = EtatQuete.Disponible;
        }

		public void DemarrerQuete()
		{
            _etat = EtatQuete.EnCours;
            _objectifs.GetEnumerator().MoveNext();
		}

        private void TerminerQuete()
        {
            _etat = EtatQuete.Terminee;
            GestionMonde.ValiderQuete(_nom, _gains); //Question profs : savoir si c'est mieux de passer attribut ou propriété en paramètres
            //Débloquer la quête suivante
        }

	}
}

