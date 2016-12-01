using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mysterole
{
    public class Queteur : Pnj
    {
        //Attributs
        private List<Quete> _quetes;
        public List<Quete> Quetes
        {
            get { return _quetes; }
            private set { }
        }



        
        //private Dictionary<string, Dictionary<int, List<Evenement>>> _evenementsQuete;

        //private string _indexQuete = "";
        //private int _indexQueteList = 0;
        //private int _indexQueteEvent = 0;
        private bool _queteEnCours = false;
        private bool _peutQuete = false;

        private string robj = "";
        private Objectif obj;
        //private bool _avanceeQuete = true;
        private bool objvalide = false;


        //Propriétés



        //public Dictionary<string, Dictionary<int, List<Evenement>>> EvenementsQuete
        //{
        //    get { return _evenementsQuete; }
        //    private set { _evenementsQuete = value; }
        //}



        protected override void Awake()
        {
            base.Awake();
            _quetes = new List<Quete>();
            //_evenementsQuete = new Dictionary<string, Dictionary<int, List<Evenement>>>();
            _etat = EtatPnj.Pattern;
            _seDeplace = true;

        }



        //Méthode Start, à l'instanciation du GameObject
        protected override void Start()
        {
            InitialiserQuetes();
        }

        //Méthode Update, en fonction des fps
        protected override void Update()
        {
            if ((_rbody != null) && _rbody.IsSleeping())
                _rbody.WakeUp();

            if (_etat == EtatPnj.Cinematique)
            {
                Debug.Log("testcinematique");
                base.Update();
                return;
            }




            if (!ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement))
            {
                _etat = EtatPnj.Pattern;
            }
            else
            {
                //Debug.Log("testpasevent");

                _etat = EtatPnj.Actif;
                //if (_evenementActuel.Etat == EtatEvenement.Fini)
                //    _indexEvenement++;
            }


            base.Update();

            //if (_faitPattern || _faitCinematique)
            //{
            //    Debug.Log("testpattern");
            //    base.Update();
            //}
            //else
            //{
            //    if ((_rbody != null) && _rbody.IsSleeping())
            //        _rbody.WakeUp();

            //    if (_seDeplace)
            //        EffectuerDeplacement();

            //    if (_evenementActuel.Type == TypeEvenement.Deplacement && (Vector2)_rbody.transform.position == ((Deplacement)_evenementActuel).Destination)
            //    {
            //        _seDeplace = false;
            //        _anim.SetBool("isWalking", false);
            //    }



            //    //S'il y a une quête en cours dans la liste des quêtes reliées au quêteur
            //    if (_indexQuete != -1 && _avanceeQuete)
            //    {

            //        //On actualise l'événement actuel en fonction de l'actualisation des quêtes
            //        if (!ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement))
            //        {
            //            JoueurMonde.PeutAgir = true;
            //            _indexEvenement = 0;
            //            _indexQuete = -1;
            //            _estActif = false;
            //            _faitPattern = true;
            //            _peutQuete = false;
            //        }



            //        switch (_evenementActuel.Etat)
            //        {
            //            case EtatEvenement.EnAttente:
            //                _evenementActuel.DeclencherEvenement(this);
            //                break;
            //            case EtatEvenement.EnCours:
            //                if (Input.GetKeyDown(KeyCode.T) && _evenementActuel.Type == TypeEvenement.Dialogue || _evenementActuel.Type == TypeEvenement.Deplacement && !_seDeplace)
            //                    _evenementActuel.Etat = EtatEvenement.Fini;
            //                break;
            //            case EtatEvenement.Fini:
            //                switch (_evenementActuel.Type)
            //                {
            //                    case TypeEvenement.Dialogue:
            //                        EcranDialogue.closeDialog();
            //                        _evenementActuel = null;
            //                        break;
            //                    case TypeEvenement.Deplacement:
            //                        _evenementActuel = null;
            //                        break;
            //                    case TypeEvenement.Attente:

            //                        break;
            //                }
            //                _indexEvenement++;
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        if (Input.GetKeyDown(KeyCode.T) && _evenementActuel.Type == TypeEvenement.Dialogue)
            //        {
            //            EcranDialogue.closeDialog();
            //            JoueurMonde.PeutAgir = true;
            //        }
            //    }
            //}
        }




        //Méthodes

        private void InitialiserQuetes()
        {
            //On va chercher les quêtes reliées au quéteur
            //On récupère l'énumérateur du dictionnaire de quêtes (parent)

            Dictionary<QueteParente, List<Quete>>.Enumerator enumQuetesP = GestionMonde.Quetes.GetEnumerator();

            while (enumQuetesP.MoveNext())
            {
                //On récupére l'énumérateur de la liste de quêtes enfante
                List<Quete>.Enumerator enumQuetesE = enumQuetesP.Current.Value.GetEnumerator();
                while (enumQuetesE.MoveNext())
                {
                    //On vérifie si le pnj est responsable du début de la quête
                    if (enumQuetesE.Current.ResponsablePnj == _id)
                        if (!_quetes.Contains(enumQuetesE.Current))
                        {
                            _quetes.Add(enumQuetesE.Current);
                            continue;
                        }

                    //On vérifie si le pnj est responsable d'un objectif de la quête
                    foreach (Objectif obj in enumQuetesE.Current.Objectifs)
                    {
                        //Debug.Log(obj.Responsable);
                        if (obj.Responsable == _id)
                            if (!_quetes.Contains(enumQuetesE.Current))
                                _quetes.Add(enumQuetesE.Current);
                    }
                }
            }
        }


        public void VerifierAvanceeQuete()
        {
            //_avanceeQuete = true;
            _peutQuete = false;
            List<Quete>.Enumerator enumQuete = _quetes.GetEnumerator();
            int occQuetes = 0;

            while (enumQuete.MoveNext())
            {
                //TODO : Diviser tout ces blocs en switch si possible
                //TODO : rajouter la gestion d'ordre des quêtes (bloquer les quêtes indisponibles)

                //Démarrage de la quête par le responsable
                if (enumQuete.Current.ResponsablePnj == _id && enumQuete.Current.Etat == EtatQuete.Disponible)
                {
                    //Démarrer la quête aussi bien dans la liste de quete que dans la gestion des événements
                    enumQuete.Current.DemarrerQuete();
                    _queteEnCours = true;
                    EcranNotification.NouvelleNotification(TypeNotification.Quete, "Quête : " + enumQuete.Current.Nom + "\nDébut");

                    _indexQuete = enumQuete.Current.ID;
                    _indexObjectif = 0;
                    _indexEvenement = 0;

                    _etat = EtatPnj.Actif;
                    //TODO : gérer les événements à l'activation
                    break;
                }

                //Validation de l'avanceée de la quête
                if (enumQuete.Current.Etat == EtatQuete.EnCours && _etat != EtatPnj.Actif)
                {
                    List<Objectif>.Enumerator enumObjectif = enumQuete.Current.Objectifs.GetEnumerator();
                    int compteur = -1;

                    //On actualise l'index
                    _indexQuete = enumQuete.Current.ID;

                    //On vérifie si les objectifs de la quête en cours sont validés et/ou si le queteur est responsable
                    while (enumObjectif.MoveNext())
                    {
                        compteur++;
                        if (enumObjectif.Current.EstValide)
                            continue;

                        if (enumObjectif.Current.Responsable == _id)
                        {
                            //On actualiser l'index et on arrête le comportement par défaut
                            _indexObjectif = enumObjectif.Current.ID;

                            //On vérifie que tous les pré-requis sont validés
                            if (enumObjectif.Current.ValiderObjectif())
                            {
                                //TODO : Pop-up d'objectif accompli
                                _indexEvenement = 0;

                                //On vérifie si tous les objectifs de la quête sont validés
                                if (enumQuete.Current.ObjectifsValides == enumQuete.Current.Objectifs.Count)
                                {
                                    _queteEnCours = false;

                                    //On termine la quête : distribution des gains et pop-up de quête accomplie
                                    enumQuete.Current.TerminerQuete();
                                    EcranNotification.NouvelleNotification(TypeNotification.Quete, "Quête : " + enumQuete.Current.Nom + "\nFin");

                                    //On vérifie si l'objectif validé déclenche une cinématique
                                    GestionMonde.VerifierCinematiques(enumObjectif.Current.ID);
                                    break;
                                }

                                //On vérifie si le quêteur est responsable de l'objectif suivant pour le valider en même temps
                                if (enumQuete.Current.Objectifs[compteur + 1].Responsable == _id)
                                {
                                    objvalide = true;
                                    continue; //TODO : vérifier s'il ne manque rien après la validation de l'objectif
                                }
                                else
                                {
                                    //Pop-up d'objectif validé
                                    EcranNotification.NouvelleNotification(TypeNotification.Objectif, "Objectif : " + enumObjectif.Current.Description + "\n");
                                    //On vérifie si l'objectif validé déclenche une cinématique
                                    GestionMonde.VerifierCinematiques(enumObjectif.Current.ID);
                                    break;
                                }
                            }
                            else
                            {
                                _indexEvenement = -1;
                                
                                //_evenementActuel = new Dialogue("Objectif non validé", TypeEvenement.Dialogue);
                                //_evenementActuel.DeclencherEvenement(this);

                                //Cas particulier où le pnj est responsable de l'objectif mais que le joueur ne l'a pas terminé (pas tous les prerequis)
                            }
                        }
                        else
                        {
                            if (objvalide)
                                break;

                            _indexQuete = -1;
                            _indexObjectif = -1;
                            _indexEvenement = 0;

                            //_avanceeQuete = false;

                            break;
                            //TODO : gérer le pattern du pnj à adopter selon l'avancée des quêtes qui le concerne
                        }
                    }

                    objvalide = false;
                }

                occQuetes++;
                if (_queteEnCours)
                    break;
            }

            //Comportement par défaut car aucune quête à mettre à jour
            if(occQuetes == _quetes.Count && _etat != EtatPnj.Actif)
            {

                _indexQuete = -1;
                _indexObjectif = -1;
                _indexEvenement = 0;

                //Discussion par défaut ou discussion de quête par défaut
                //TODO : Empecher le joueur de lancer la discussion pendant le déroulement d'un événement



            }

        }





        protected override void Interagir()
        {
            // if (_indexQuete == "")
            //     _indexQuete == ;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.name == JoueurMonde.Moi.gameObject.name)
                GestionMonde.PnjProche = _nom;
            //Debug.Log("testtrigger" + _nom);
            //Debug.Log(transform.position);
        }


        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.name == JoueurMonde.Moi.gameObject.name)
                _peutQuete = true;
            //Debug.Log("testtestest" + _nom + transform.position);

            //TODO : Faire une logique pour déclencher le base.Interagir() si toutes les quêtes sont terminées

        }

        void OnTriggerLeave2D(Collider2D col)
        {
            if (col.gameObject.name == JoueurMonde.Moi.gameObject.name)
            {
                _peutQuete = false;
                GestionMonde.PnjProche = "";
            }

        }
        //public void DeclencherEvenementsQuete()
        //{
        //    List<Evenement>.Enumerator enumEventQuete = _evenements[TypeEvenement.Quete].GetEnumerator();

        //    enumEventQuete.MoveNext();






        //    //TODO : gérer les événements à l'activation*


        //    //ELSE : Evenements par défaut



        //}


    }
}


