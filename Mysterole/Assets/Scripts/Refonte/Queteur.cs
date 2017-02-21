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

        private bool _queteEnCours = false;
        private bool _peutQuete = false;

        private string robj = "";
        private Objectif obj;
        private bool _avanceeQuete = true;
        private bool objvalide = false;


        //Propriétés






        protected override void Awake()
        {
            base.Awake();
            _quetes = new List<Quete>();
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



            if(_etat == EtatPnj.Quete)
            {

                if (_seDeplace)
                {
                    EffectuerDeplacement();

                    if ((Vector2)_rbody.transform.position == ((Deplacement)_evenementActuel).Destination)
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

                if (_evenementActuel == null)
                {
                    _etat = EtatPnj.Inactif;
                    base.Update();
                    return;
                }

                switch (_evenementActuel.Etat)
                {
                    case EtatEvenement.EnAttente:
                        _evenementActuel.DeclencherEvenement(this);
                        break;
                    case EtatEvenement.EnCours:
                        if(_evenementActuel.Type == TypeEvenement.Dialogue && Input.GetButtonUp("Accepter"))
                        {
                            EcranDialogue.FermerDialogue();
                            _evenementActuel.Etat = EtatEvenement.Fini;
                        }
                        break;
                    case EtatEvenement.Fini:
                        if (_indexEvenement < 0)
                            _indexEvenement--;
                        else
                            _indexEvenement++;
                        if(!ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement))
                        {
                            _indexQuete = 0;
                            _indexObjectif = 0;
                            _indexEvenement = 0;

                            _etat = EtatPnj.Inactif;
                            JoueurMonde.PeutAgir = true;
                            _queteEnCours = false;
                        }
                        else
                        {
                            Debug.Log(_indexQuete + "    " + _indexObjectif + _indexEvenement);
                            _etat = EtatPnj.Pattern;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                base.Update();
            }
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
                    {
                        if (!_quetes.Contains(enumQuetesE.Current))
                        {
                            _quetes.Add(enumQuetesE.Current);
                            //_evenementsQuete.Add(enumQuetesE.Current, new List<Objectif>());
                            continue;
                        }
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
            if (_etat == EtatPnj.Quete)
                return;



            _peutQuete = false;
            //List<Quete>.Enumerator enumQuete = _quetes.GetEnumerator();
            int occQuetes = 0;

            int _compteur = 0;
            foreach(Quete q in _quetes)
            {
                if (_etat == EtatPnj.Quete)
                    break;

                //TODO : Diviser tout ces blocs en switch si possible
                //TODO : rajouter la gestion d'ordre des quêtes (bloquer les quêtes indisponibles)

                //Démarrage de la quête par le responsable
                if (q.ResponsablePnj == _id && q.Etat == EtatQuete.Disponible)
                {
                    //Démarrer la quête aussi bien dans la liste de quete que dans la gestion des événements
                    q.DemarrerQuete();
                    _queteEnCours = true;
                    EcranNotification.NouvelleNotification(q);

                    //TODO : gérer les événements à l'activation
                    _indexQuete = q.ID;
                    _indexObjectif = 0;
                    _indexEvenement = 0;

                    ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement);
                    Debug.Log(q.Nom + q.Etat);
                    _etat = EtatPnj.Quete;
                    break;
                }

                //Validation de l'avanceée de la quête
                if (q.Etat == EtatQuete.EnCours)
                {


                    int compteur = -1;

                    //On vérifie si les objectifs de la quête en cours sont validés et/ou si le queteur est responsable
                    foreach(Objectif o in q.Objectifs)
                    //while (enumObjectif.MoveNext())
                    {

                        compteur++;
                        if (o.EstValide)
                            continue;

                        if (o.Responsable == _id)
                        {

                            //On vérifie que tous les pré-requis sont validés
                            if (o.ValiderObjectif())
                            {

                                if (!objvalide)
                                {
                                    _indexQuete = q.ID;
                                    _indexObjectif = o.ID;
                                    _indexEvenement = 0;

                                    ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement);
                                    _etat = EtatPnj.Quete;

                                    //Pop-up d'objectif validé
                                    EcranNotification.NouvelleNotification(o);
                                    GestionMonde.VerifierCinematiques(o.ID);

                                }

                                //TODO : Pop-up d'objectif accompli
                                //On vérifie si tous les objectifs de la quête sont validés
                                if (q.ObjectifsValides == q.Objectifs.Count)
                                {

                                    _queteEnCours = false;
                                    q.TerminerQuete();
                                    EcranNotification.NouvelleNotification(q);
                                    GestionMonde.VerifierCinematiques(o.ID);
                                    objvalide = false;
                                    break;
                                }

                                //On vérifie si le quêteur est responsable de l'objectif suivant pour le valider en même temps
                                if (q.Objectifs[compteur + 1].Responsable == _id)
                                {

                                    objvalide = true;
                                    continue; //TODO : vérifier s'il ne manque rien après la validation de l'objectif
                                }
                                else
                                {
                                    //Pop-up d'objectif validé
                                    EcranNotification.NouvelleNotification(o);
                                    GestionMonde.VerifierCinematiques(o.ID);
                                    objvalide = false;
                                    break;


                                }
                            }
                            else
                            {
                                _indexQuete = q.ID;
                                _indexObjectif = o.ID;
                                _indexEvenement = -1;
                                ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement);
                                _etat = EtatPnj.Quete;
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
                {
                    Debug.Log(q.Nom + "queteencours");
                    break;
                }
            }

            //Comportement par défaut car aucune quête à mettre à jour
            if(occQuetes == _quetes.Count && _etat != EtatPnj.Actif)
            {

                _indexQuete = -0;
                _indexObjectif = 0;
                _indexEvenement = 0;
                //Discussion par défaut ou discussion de quête par défaut
                //TODO : Empecher le joueur de lancer la discussion pendant le déroulement d'un événement



            }

        }





        protected override void Interagir()
        {

        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.name == JoueurMonde.Moi.gameObject.name)
                GestionMonde.PnjProche = _nom;

        }


        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.name == JoueurMonde.Moi.gameObject.name)
            {
                _peutQuete = true;
                GestionMonde.PnjProche = _nom;
            }


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
    }
}


