using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class Etape
    {
        //int compteur = 0;

        private int _id;
        public int ID
        {
            get { return _id; }
        }

        private int _cinematiqueReliee;
        public int CinematiqueReliee
        {
            get { return _cinematiqueReliee; }
        }


        private EtatEtape _etat;
        public EtatEtape Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }

        private int _position;
        public int Position
        {
            get { return _position; }
        }

        private Vector2 _cameraPos;
        public Vector2 CameraPos
        {
            get { return _cameraPos; }
        }

        //private Dictionary<int, Evenement> _evenementsCinematiques;
        //public Dictionary<int, Evenement> EvenementsCinematiques
        //{
        //    get { return _evenementsCinematiques; }
        //}

        private List<GameObject> _acteurs;
        public List<GameObject> Acteurs
        {
            get { return _acteurs; }
            set { _acteurs = value; }
        }
        private List<GameObject> _aDetruire;
        public List<GameObject> ADetruire
        {
            get { return _aDetruire; }
            set { _aDetruire = value; }
        }
        private GameObject _cameraCinematique;
        public GameObject CameraCinematique
        {
            get { return _cameraCinematique; }
            set { _cameraCinematique = value; }
        }



        private float timer;
        private bool dialogueEnCours = false;
        private int compteur = 0;
        int compteurp = 0;
        int compteurj = 0;


        private GameObject _carteReliee;
        public GameObject CarteReliee
        {
            get { return _carteReliee; }
        }

        private Vector2 _positionJoueur;
        public Vector2 PositionJoueur
        {
            get { return _positionJoueur; }
        }

        private bool estActif = false;
        //Constructeurs
        public Etape()
        {
            _etat = EtatEtape.EnAttente;
        }
        public Etape(int id, int idc, int ee, GameObject c, int p, Vector2 pos)
        {
            _id = id;
            _cinematiqueReliee = idc;
            _etat = MethodesEnum.TrouverEtatEtape(ee);
            _carteReliee = c;
            Debug.Log(_carteReliee);
            _position = p;
            _positionJoueur = pos;

            _acteurs = new List<GameObject>();
            _aDetruire = new List<GameObject>();

            //_evenementsCinematiques = AccesBD.TrouverEvenementsCinematiques(this);
        }

        //Méthode pour déclencher les événements des acteurs
        public void DeclencherEtape()
        {
            RecupererActeurs();
            PlacerActeurs();
            _etat = EtatEtape.EnCours;
            bool firstocc = true;

            foreach (GameObject a in _acteurs)
            {
                Debug.Log(a.gameObject.name);


                if (firstocc)
                {
                    CameraMonde.ActeurPrincipal = a;
                    firstocc = false;
                }

                if (a == JoueurMonde.Moi.gameObject)
                {
                    JoueurMonde.ActualiserEvenementCinematique(_id, compteurj);
                    JoueurMonde.FaitCinematique = true;
                    compteurj++;
                }
                else
                {
                    Pnj p = a.GetComponent<Pnj>();
                    p.ActualiserEvenementCinematique(_id, compteurp);
                    p.Etat = EtatPnj.Cinematique;
                    compteurp++;
                }
            }
        }

        //Méthode pour vérifier si tous les acteurs ont fini leurs événements
        public void VerifierEtape()
        {
            bool estFinie = true;
            //compteur = 0;

            foreach (GameObject a in _acteurs)
            {
                if(a == JoueurMonde.Moi.gameObject)
                {
                    switch (JoueurMonde.EvenementActuel.Etat)
                    {
                        case EtatEvenement.EnAttente:
                            estFinie = false;
                            break;
                        case EtatEvenement.EnCours:
                            estFinie = false;
                            break;
                        case EtatEvenement.Fini:
                            if (!JoueurMonde.ActualiserEvenementCinematique(_id, compteurj))
                                estFinie = true;
                            else
                                compteurj++;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (a.GetComponent<Pnj>().EvenementActuel.Etat)
                    {
                        case EtatEvenement.EnAttente:
                            estFinie = false;
                            break;
                        case EtatEvenement.EnCours:
                            estFinie = false;
                            break;
                        case EtatEvenement.Fini:
                            if (!a.GetComponent<Pnj>().ActualiserEvenementCinematique(_id, compteurp))
                                estFinie = true;
                            break;
                    }
                }
            }

            if (estFinie)
                TerminerEtape();
        }

        //Méthode pour terminer l'étape
        public void TerminerEtape()
        {
            DetruireActeurs();
            ReplacerActeurs();
            _etat = EtatEtape.Finie;
        }

        //Méthode pour récupérer les acteurs de l'étape
        public void RecupererActeurs()
        {
            _acteurs = GestionMonde.TrouverActeurs(_id);
        }

        //Méthode pour placer les acteurs de l'étape
        public void PlacerActeurs()
        {
            Debug.Log("hotfixcinéma");

            foreach (GameObject a in _acteurs)
            {
                if (a == JoueurMonde.Moi.gameObject)
                {
                    JoueurMonde.Coor = new Vector2(JoueurMonde.Moi.transform.position.x, JoueurMonde.Moi.transform.position.y);
                    JoueurMonde.Moi.transform.position = _positionJoueur;
                    Debug.Log(JoueurMonde.Moi.gameObject.transform.position);
                }
                else
                {
                    a.GetComponent<Pnj>().Coor = new Vector2(a.transform.position.x, a.transform.position.y);
                    a.transform.position = AccesBD.TrouverPositionActeur(a.GetComponent<Pnj>().ID, _id);
                }
            }


        }

        //Méthode pour détruire les acteurs temporaires
        public void DetruireActeurs()
        {
            foreach (GameObject a in _aDetruire)
            {
                GameObject.Destroy(a);
            }
        }

        //Méthode pour replacer les joueurs à leur position initiale
        public void ReplacerActeurs()
        {
            foreach (GameObject a in _acteurs)
            {
                if (a == JoueurMonde.Moi.gameObject)
                    a.transform.position = JoueurMonde.Coor;
                else
                    a.transform.position = a.GetComponent<Pnj>().Coor;
            }
        }
    }
}
