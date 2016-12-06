using System;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;

namespace Mysterole
{
	public class Pnj : MonoBehaviour
	{
        //Attributs
        protected int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        protected string _nom;
        public string Nom
        {
            get { return _nom; }
        }

        protected Vector2 _coor;
        public Vector2 Coor
        {
            get { return _coor; }
            set { _coor = value; }
        }

        protected GameObject _carteActuelle;
        public GameObject CarteActuelle
        {
            get { return _carteActuelle; }
        }

        protected Personnage _personnagePnj;
        public Personnage PersonnagePnj
        {
            get { return _personnagePnj; }
        }



        protected Animator _anim;
        public Animator Anim
        {
            get { return _anim; }
            set { _anim = value; }
        }
        protected Rigidbody2D _rbody;


        protected EtatPnj _etat;
        public EtatPnj Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }

        //Attribut d'event 
        protected bool _seDeplace = false;
        public bool SeDeplace
        {
            get { return _seDeplace; }
            set { _seDeplace = value; }
        }
        protected bool _discute = false;
        public bool Discute
        {
            get { return _discute; }
            set { _discute = value; }
        }

        protected bool _faitPause = false;
        public bool FaitPause
        {
            get { return _faitPause; }
            set { _faitPause = value; }
        }
        //protected bool _faitPattern = false;
        //public bool FaitPattern
        //{
        //    get { return _faitPattern; }
        //    set { _faitPattern = value; }
        //}
        //protected bool _faitCinematique;
        //public bool FaitCinematique
        //{
        //    get { return _faitCinematique; }
        //    set { _faitCinematique = value; }
        //}
        protected bool _peutEvent = false;
        public bool PeutEvent
        {
            get { return _peutEvent; }
            set { _peutEvent = value; }
        }


        protected List<Evenement> _evenementsPassifs;
        public List<Evenement> EvenementsPassifs
        {
            get { return _evenementsPassifs; }
        }
        protected List<Evenement> _evenementsActifs;
        public List<Evenement> EvenementsActifs
        {
            get { return _evenementsActifs; }
        }
        protected List<Evenement> _evenementsCinematiques;
        public List<Evenement> EvenementsCinematiques
        {
            get { return _evenementsCinematiques; }
            set { _evenementsCinematiques = value; }
        }
        protected Evenement _evenementActuel;
        public Evenement EvenementActuel
        {
            get { return _evenementActuel; }
            set { _evenementActuel = value; }
        }


        private Vector2 _deplacementPattern;
        //private bool dec = false;


        private int _indexPattern = 0;
        protected int _indexQuete;
        public int IndexQuete
        {
            get { return _indexQuete; }
        }
        protected int _indexObjectif;
        public int IndexObjectif
        {
            get { return _indexObjectif; }
        }
        protected int _indexEvenement;
        public int IndexEvenement
        {
            get { return _indexEvenement; }
        }
        //private int _idCinematique;
        //public int IDCinematique
        //{
        //    get { return _idCinematique; }
        //    set { _idCinematique = value; }
        //}
        //private int _idEtape;
        //public int IDEtape
        //{
        //    get { return _idEtape; }
        //    set { _idEtape = value; }
        //}

        protected Vector2 _positionInitiale;


        /*
        concepts d'attributs à tester avec UnityEngine

            protected isEvent;
        */





        protected virtual void Awake()
        {

            gameObject.name = gameObject.name.Replace("(Clone)", "");
            _nom = gameObject.name;
            _id = AccesBD.TrouverIdPnj(_nom);


            _anim = GetComponent<Animator>();
            _rbody = GetComponent<Rigidbody2D>();

            //On initialise la position initiale du pnj
            _positionInitiale = new Vector2(transform.position.x, transform.position.y);


            //On récupère les événements passifs du pnj dans la BD et on initialise l'événement à effectuer par défaut
            _evenementsPassifs = AccesBD.TrouverEvenementsPassifs(this);
            _evenementsActifs = AccesBD.TrouverEvenementsActifs(this);
            _evenementsCinematiques = AccesBD.TrouverEvenementsCinematiques(_id);

            _indexQuete = 0;
            _indexObjectif = 0;
            _indexEvenement = 0;

            _coor = transform.position;
            _etat = EtatPnj.Pattern;
        }

        protected virtual void Start()
        {
            _seDeplace = false;

        }


        protected virtual void FixedUpdate()
        {

        }

        protected virtual void Update()
        {
            //Debug.Log("testpnj" + _nom);
            //Debug.Log(_indexQuete);
            //Debug.Log(_indexObjectif);
            //Debug.Log(_indexEvenement);



            if (_nom != "Sage du village" && _nom != "Père")
                return;

            if ((_rbody != null) && _rbody.IsSleeping())
                _rbody.WakeUp();

            if (_seDeplace)
            {
                Debug.Log("testpnj2");
                EffectuerDeplacement();


                if ((Vector2)_rbody.transform.position == _deplacementPattern)
                {
                    _seDeplace = false;
                    _anim.SetBool("isWalking", false);
                    _evenementActuel.Etat = EtatEvenement.Fini;
                }
                else if ((Vector2)_rbody.transform.position == ((Deplacement)_evenementActuel).Destination)
                {
                    _seDeplace = false;
                    _anim.SetBool("isWalking", false);
                    _evenementActuel.Etat = EtatEvenement.Fini;
                }
            }

            if (_faitPause)
            {
                _evenementActuel.Decompte -= Time.deltaTime;

                if (_evenementActuel.Decompte < 0)
                    _evenementActuel.Etat = EtatEvenement.Fini;
            }


            switch (_etat)
            {
                case EtatPnj.Inactif:





                    break;
                case EtatPnj.Actif:
                    //Debug.Log("actif" + _nom);
                    switch (_evenementActuel.Etat)
                    {
                        case EtatEvenement.EnAttente:
                            _evenementActuel.DeclencherEvenement(this);
                            break;
                        case EtatEvenement.EnCours:
                            //Debug.Log("lol");
                            if (Input.GetButtonUp("Accepter") && _evenementActuel.Type == TypeEvenement.Dialogue)
                                _evenementActuel.Etat = EtatEvenement.Fini;
                            break;
                        case EtatEvenement.Fini:

                            if (_evenementActuel.Type == TypeEvenement.Dialogue)
                                EcranDialogue.FermerDialogue();

                            if(_indexQuete == -1)
                            {
                                _evenementActuel.Etat = EtatEvenement.EnAttente;
                                _evenementActuel = null;
                            }

                            _indexEvenement++;
                            if(!ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement))
                                JoueurMonde.PeutAgir = true;
                            break;
                    }



                    break;
                case EtatPnj.Cinematique:



                    switch (_evenementActuel.Etat)
                    {
                        case EtatEvenement.EnAttente:
                            _evenementActuel.DeclencherEvenement(this);
                            break;
                        case EtatEvenement.EnCours:
                            if (_evenementActuel.Type == TypeEvenement.Dialogue && Input.GetKeyDown(KeyCode.E))
                                _evenementActuel.Etat = EtatEvenement.Fini;
                            break;
                        case EtatEvenement.Fini:
                            if (_evenementActuel.Type == TypeEvenement.Dialogue && Input.GetKeyDown(KeyCode.E))
                                EcranDialogue.FermerDialogue();

                            break;
                        default:
                            break;
                    }



                    break;
                case EtatPnj.Pattern:

                    break;
            }
        }


        protected virtual void Interagir()
        {

        }

        public void EffectuerDeplacement()
        {
            //Debug.Log(_id);
            //Debug.Log(_evenementActuel);
            Debug.Log(_evenementActuel.Type);


            Vector2 offset = new Vector2(((Deplacement)_evenementActuel).Destination.x - _rbody.position.x, ((Deplacement)_evenementActuel).Destination.y - _rbody.position.y);

            _anim.SetBool("isWalking", true);
            _anim.SetFloat("input_x", offset.x);
            _anim.SetFloat("input_y", offset.y);


            //if(!dec)
            //{
            //    bool dx = (UnityEngine.Random.Range(0, 2) == 0);
            //    bool dy = (UnityEngine.Random.Range(0, 2) == 0);


            //    float x = (dx ? UnityEngine.Random.Range(Coor.x - ((Deplacement)_evenementActuel).Destination.x, Coor.x)
            //    : UnityEngine.Random.Range(Coor.x + ((Deplacement)_evenementActuel).Destination.x, Coor.x)
            //    );

            //    float y = (dy ? UnityEngine.Random.Range(Coor.y - ((Deplacement)_evenementActuel).Destination.y, Coor.y)
            //    : UnityEngine.Random.Range(Coor.y + ((Deplacement)_evenementActuel).Destination.y, Coor.y)
            //    );


            //    _deplacementPattern = new Vector2(x, y);
            //    dec = true;
            //}

             transform.position = Vector3.MoveTowards(transform.position, ((Deplacement)_evenementActuel).Destination, Time.deltaTime * 5);
        }

        public void Teleporter(int id, float x, float y)
        {
            transform.position = new Vector2(x, y);
            _carteActuelle = GestionMonde.TrouverCarte(id);
        }

        public bool ActualiserEvenement(int iq, int io, int ie)
        {
            foreach (Evenement e in _evenementsActifs)
            {
                if (e.IDQuete == iq && e.IDObjectif == io && e.IndexEvenement == ie)
                {
                    _evenementActuel = e;
                    return true;
                }
            }

            return false;
        }

        public bool ActualiserEvenementCinematique(int id, int compteur)
        {
            int lcompteur = 0;
            foreach (Evenement e in _evenementsCinematiques)
            {
                if(e.IdEtape == id && lcompteur == compteur)
                {
                    _evenementActuel = e;
                    return true;
                }

                compteur++;
            }

            return false;
        }

    }



}






//if (FaitCinematique && _peutEvent)
//{
//    if(_faitPattern)
//    {
//        _seDeplace = false;
//        _anim.SetBool("isWalking", false);
//    }

//    _faitPause = false;
//    _faitPattern = false;

//    if (_seDeplace)
//        EffectuerDeplacement();

//    Debug.Log(_destinationActuelle + "   " + _nom);
//    Debug.Log(transform.position + "   " + _nom);
//    Debug.Log(_faitPause);

//    if ((Vector2)_rbody.transform.position == _destinationActuelle && !_faitPause)
//    {
//        Debug.Log("test1" + _nom);
//        _seDeplace = false;
//        _anim.SetBool("isWalking", false);
//    }





//    switch (_evenementsCinematique[_idCinematique][_idEtape].Etat)
//    {
//        case EtatEvenement.EnAttente:


//            //Debug.Log(_idCinematique);
//            //Debug.Log(_idEtape);
//            //Debug.Log(_evenementsCinematique[_idCinematique][_idEtape].Etat);





//            _evenementsCinematique[_idCinematique][_idEtape].DeclencherEvenement(this);

//            break;
//        case EtatEvenement.EnCours:
//            if (Input.GetKeyDown(KeyCode.T) && _evenementsCinematique[_idCinematique][_idEtape].GetType() == typeof(Dialogue)) //Bouton ok pour les dialogues
//            {
//                _evenementsCinematique[_idCinematique][_idEtape].Etat = EtatEvenement.Fini;    
//            }

//            if (_evenementsCinematique[_idCinematique][_idEtape].GetType() == typeof(Deplacement) && !_seDeplace)
//            {
//                _evenementsCinematique[_idCinematique][_idEtape].Etat = EtatEvenement.Fini;
//            }
//            break;
//        case EtatEvenement.Fini:

//            if (_evenementsCinematique[_idCinematique][_idEtape].GetType() == typeof(Dialogue))
//            {
//                EcranDialogue.closeDialog();
//                _dialogueActuel = null;
//                JoueurMonde.CanMove = true;
//            }
//            GestionMonde.Cinematiques[_idCinematique].Etapes[_idEtape].ActualiserActeur(_nom);
//            break;
//        default:
//            break;
//    }
//}
//else
//{

//    //On actualise la destination actuelle du Pnj 
//    if (_destinationActuelle != _evenementsPattern[0][_indexPattern].Destination && !_faitPause && !_seDeplace)
//    {
//        _evenementsPattern[0][_indexPattern].DeclencherEvenement(this);
//    }

//    if ((_rbody != null) && _rbody.IsSleeping())
//        _rbody.WakeUp();

//    if (_seDeplace)
//        EffectuerDeplacement();

//    if ((Vector2)_rbody.transform.position == _destinationActuelle && !_faitPause)
//    {
//        _faitPause = true;
//        _seDeplace = false;
//        _anim.SetBool("isWalking", false);
//    }





//    if (_faitPause)
//    {
//        pause -= Time.deltaTime;

//        if (pause < 0)
//        {
//            _faitPause = false;
//            pause = 2.0f;

//            if (_indexPattern == 0)
//                _indexPattern = 1;
//            else
//                _indexPattern = 0;
//        }
//    }
//}