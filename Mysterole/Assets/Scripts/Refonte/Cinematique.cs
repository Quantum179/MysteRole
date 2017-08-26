using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class Cinematique
    {
        private int _id;
        public int ID
        {
            get { return _id; }
        }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
        }

        private EtatCinematique _etat;
        public EtatCinematique Etat
        {
            get { return _etat; }
        }


        private int _idObjectif;
        public int IDObjectif
        {
            get { return _idObjectif; }
        }

        private int _idQuete;
        public int IDQuete
        {
            get { return _idQuete; }
        }

        private GameObject _carte;
        public GameObject Carte
        {
            get { return _carte; }
            set { _carte = value; }
        }

        private List<Etape> _etapes;
        public List<Etape> Etapes
        {
            get { return _etapes; }
            private set { _etapes = value; }
        }
        public Etape EtapeActuelle
        {
            get
            {
                foreach (Etape e in _etapes)
                {
                    if (e.Etat == EtatEtape.EnCours)
                        return e;
                }
                return null;
            }
        }

        private float compteur = 4f;
        private bool isSet = false;
        private float timer = 5f;


        //TODO : Faire les requêtes dans la BD pour récupérer les actions de la cinématique
        public Cinematique()
        {

        }

        public Cinematique(int id, EtatCinematique ec, string n, int o, int q)
        {
            _id = id;
            _etat = ec;
            _nom = n;
            _idObjectif = o;
            _idQuete = q;

        }




        public void DeclencherCinematique()
        {
            _etat = EtatCinematique.EnCours;
        }

        public void FaireCinematique()
        {
            compteur -= Time.deltaTime;
            
            if(compteur < 0)
            {
                EcranDialogue.FermerDialogue();
                TerminerCinematique();
            }
        }


        private void TerminerCinematique()
        {
            _etat = EtatCinematique.Terminee;
            ActualiserQuete();
            ActualiserPnjs();
            GestionMonde.FaitCinematique = false;
            JoueurMonde.PeutAgir = true;
        }

        private void ActualiserQuete()
        {
            Quete q = GestionMonde.TrouverQuete(_idQuete);
            if (q != null)
            {
                q.Etat = MethodesEnum.ActualiserEtatQuete(q.Etat);
                //refonte : q.ActualiserEtatQuete();
                EcranNotification.NouvelleNotification(q);
            }
        }

        private void ActualiserPnjs()
        {

        }
    }
}
