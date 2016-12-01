using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mysterole
{
    public class Quete
    {
        //Attributs
        private int _id;
        public int ID
        {
            get { return _id; }
        }

        private EtatQuete _etat;
        public EtatQuete Etat
        {
            get { return _etat; }
            set { _etat = value; }

        }

        private QueteParente _queteParenteReliee;
        public QueteParente QueteParenteReliee
        {
            get { return _queteParenteReliee; }
        }


        private string _nom;
        public string Nom
        {
            get { return _nom; }
        }

        private int _responsablePnj;
        public int ResponsablePnj
        {
            get { return _responsablePnj; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            private set { _description = value; }
        }



        private List<Objectif> _objectifs;
        public List<Objectif> Objectifs
        {
            get { return _objectifs; }
            private set { }
        }
        public int ObjectifsValides
        {
            get
            {
                int c = 0;
                foreach (Objectif obj in _objectifs)
                    if (obj.EstValide)
                        c++;
                return c;
            }
        }


        private List<Gain> _gains;
        public List<Gain> Gains
        {
            get { return _gains; }
            private set { }
        }






        //Constructeurs
        public Quete()
        {
            //Exemple hardcodé
            //Doit aller récupérer les données dans la base de données
            //_nom = "";
            //_responsableDebut = "";
            //_objectifs = new List<Objectif>();
            //_etat = EtatQuete.Bloquee;
        }
        public Quete(string n)
        {
            ////Hardcode
            //_nom = n;

            //if (_nom == "La requête du milicien")
            //{
            //    _responsableDebut = "Chef milicien";
            //    _idQueteParente = "La visite du Roi"; //Juger si c'est utile ou pas
            //    _objectifs = new List<Objectif>();
            //    _etat = EtatQuete.Disponible;


            //    _objectifs.Add(new Objectif("Parler au Sage du village", false, "Sage du village"));
            //    _objectifs.Add(new Objectif("Obtenir l'autorisation", false, "Sage du village"));
            //    _objectifs.Add(new Objectif("Retourner parler au Chef milicien", false, "Chef milicien"));
            //    _objectifs.Add(new Objectif("Retourner parler au Sage du village", false, "Sage du village"));

            //    _gains = new List<Gain>();
            //    _gains.Add(new GainArgent("argent gagné première quête", 100));
                
            //}
            //else
            //{
            //    _responsableDebut = "Chef milicien";
            //    _idQueteParente = "La visite du Roi"; //Juger si c'est utile ou pas
            //    _objectifs = new List<Objectif>();
            //    _etat = EtatQuete.Disponible;

            //}


        }

        public Quete(int id, int eq, QueteParente qp, string n, int rp, string d)
        {
            _id = id;
            _etat = MethodesEnum.TrouverEtatQuete(eq);
            _queteParenteReliee = qp;
            _nom = n;
            _responsablePnj = rp;
            _description = d;

            _objectifs = AccesBD.TrouverObjectifs(this);
            _gains = AccesBD.TrouverGains(this);

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
            //_indexObjectif++;

            //ActualiserPnjs()
        }

        public void TerminerQuete()
        {
            _etat = EtatQuete.Terminee;
            GestionMonde.DistribuerGains(_id, _gains);
            //EcranNotification.NouvelleNotification(new NotificationQuete(this, _gains));



            //Question profs : savoir si c'est mieux de passer attribut ou propriété en paramètres
            //Débloquer la quête suivante InitialiserQuete
            //Actualiser les Pnjs

        }

    }
}
