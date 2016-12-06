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

        //private Dictionary<Quete, List<Objectif>> _evenementsQuete;
        //public Dictionary<Quete, List<Objectif>> EvenementsQuete
        //{
        //    get { return _evenementsQuete; }
        //}

        
        //private Dictionary<string, Dictionary<int, List<Evenement>>> _evenementsQuete;

        //private string _indexQuete = "";
        //private int _indexQueteList = 0;
        //private int _indexQueteEvent = 0;
        private bool _queteEnCours = false;
        private bool _peutQuete = false;

        private string robj = "";
        private Objectif obj;
        private bool _avanceeQuete = true;
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
            //_evenementsQuete = new Dictionary<Quete, List<Objectif>>();
            //_evenementsQuete = new Dictionary<string, Dictionary<int, List<Evenement>>>();
            //_etat = EtatPnj.Pattern;
            //_seDeplace = true;

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
                //Debug.Log(_quetes.Count);
                //Debug.Log(q.Nom + q.Etat);
                //Debug.Log(_etat);
                //compteur++;
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

                    //Debug.Log("test debutquete : " + q.Nom);
                    //TODO : gérer les événements à l'activation
                    _indexQuete = q.ID;
                    _indexObjectif = 0;
                    _indexEvenement = 0;

                    ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement);
                    Debug.Log(q.Nom + q.Etat);
                    _etat = EtatPnj.Quete;
                    break;
                }

                Debug.Log("pasdebut" + q.Nom);
                //Validation de l'avanceée de la quête
                if (q.Etat == EtatQuete.EnCours)
                {
                    Debug.Log(q.Nom);

                    //List<Objectif>.Enumerator enumObjectif = q.Objectifs.GetEnumerator();
                    int compteur = -1;

                    Debug.Log("hotfixquete" + q.Nom);
                    //On vérifie si les objectifs de la quête en cours sont validés et/ou si le queteur est responsable
                    foreach(Objectif o in q.Objectifs)
                    //while (enumObjectif.MoveNext())
                    {
                        Debug.Log(o.Description + "  :   " + _nom);
                        compteur++;
                        if (o.EstValide)
                            continue;

                        if (o.Responsable == _id)
                        {
                            Debug.Log("ilestla" + q.Nom);
                            //On vérifie que tous les pré-requis sont validés
                            if (o.ValiderObjectif())
                            {

                                Debug.Log("ilestla2" + q.Nom);
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
                                Debug.Log(q.Objectifs.Count);
                                Debug.Log(q.ObjectifsValides);
                                //On vérifie si tous les objectifs de la quête sont validés
                                if (q.ObjectifsValides == q.Objectifs.Count)
                                {

                                    _queteEnCours = false;
                                    Debug.Log("jenaimarre");
                                    q.TerminerQuete();
                                    EcranNotification.NouvelleNotification(q);
                                    GestionMonde.VerifierCinematiques(o.ID);
                                    objvalide = false;
                                    break;
                                }

                                //On vérifie si le quêteur est responsable de l'objectif suivant pour le valider en même temps
                                if (q.Objectifs[compteur + 1].Responsable == _id)
                                {
                                    Debug.Log("jenaimarreconnard");

                                    objvalide = true;
                                    continue; //TODO : vérifier s'il ne manque rien après la validation de l'objectif
                                }
                                else
                                {
                                    Debug.Log("ilestla3" + q.Nom);
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
                                Debug.Log("jenaimarresdsqdsqdsqdsqdsq");
                                Debug.Log(o.Description + q.Nom);
                                ActualiserEvenement(_indexQuete, _indexObjectif, _indexEvenement);
                                _etat = EtatPnj.Quete;
                            }
                        }
                        else
                        {
                            if (objvalide)
                                break;
                            Debug.Log("jenaimarreqsdqsdsqdsqmaaaaaarre");
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
                Debug.Log("pasencours" + q.Nom);

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
                Debug.Log("jetetiens");
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


            Debug.Log(GestionMonde.PnjProche);
            //Debug.Log(transform.position);
        }


        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.name == JoueurMonde.Moi.gameObject.name)
            {
                _peutQuete = true;
                GestionMonde.PnjProche = _nom;
            }
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
            Debug.Log(GestionMonde.PnjProche);

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


