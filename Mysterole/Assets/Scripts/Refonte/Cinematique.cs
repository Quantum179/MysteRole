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
            EcranDialogue.NouveauDialogue(_nom, "Les cinématiques bug");

            _etat = EtatCinematique.EnCours;
            //_etapes = AccesBD.TrouverEtapes(this);
            //JoueurMonde.PeutAgir = false;
            //_carte = _etapes[0].CarteReliee;
        }

        public void FaireCinematique()
        {
            compteur -= Time.deltaTime;
            
            if(compteur < 0)
            {
                EcranDialogue.FermerDialogue();
                TerminerCinematique();
            }
            //Etape e = _etapes[compteur];
            //Debug.Log(_etapes.Count);
            //switch (e.Etat)
            //{
            //    case EtatEtape.EnAttente:
            //        e.DeclencherEtape();
            //        break;
            //    case EtatEtape.EnCours:
            //        e.VerifierEtape();
            //        break;
            //    case EtatEtape.Finie:
            //        e.TerminerEtape();
            //        compteur++;
            //        if (compteur == _etapes.Count)
            //            TerminerCinematique();
            //        break;
            //}

            //foreach (Etape e in _etapes)
            //{
            //    switch (e.Etat)
            //    {
            //        case EtatEtape.EnAttente:
            //            e.DeclencherEtape();
            //            break;
            //        case EtatEtape.EnCours:
            //            e.VerifierEtape();
            //            break;
            //        case EtatEtape.Finie:
            //            _compteur++;

            //            if (_compteur == _etapes.Count)
            //                TerminerCinematique();
            //            break;
            //    }
            //}
        }


        private void TerminerCinematique()
        {
            _etat = EtatCinematique.Terminee;
            ActualiserQuete();
            ActualiserPnjs();
            //GestionMonde.CarteActive = _carte;
            GestionMonde.FaitCinematique = false;
            JoueurMonde.PeutAgir = true;
        }

        private void ActualiserQuete()
        {
            Quete q = GestionMonde.TrouverQuete(_idQuete);
            if (q != null)
            {
                q.Etat = MethodesEnum.ActualiserEtatQuete(q.Etat);
                EcranNotification.NouvelleNotification(q);
            }
        }

        private void ActualiserPnjs()
        {

        }
    }
}
