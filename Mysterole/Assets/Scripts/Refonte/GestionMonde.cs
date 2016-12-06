using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysterole;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tiled2Unity;



/* 
 * TODO 
 * 
 *  - Remplacer tout le hardcode par des requêtes (hot fix)
 *  - Poser des questions aux profs sur les namespaces
 *  - simplifier les enumérateurs en utilisant des foreach
 *  - Demander si c'est utile de mettre une référence du parent, exemple : Quete qui connait QueteParente
*/







namespace Mysterole
{
    public class GestionMonde : MonoBehaviour
    {
        //Attributs
        private static GestionMonde _monde;


        private GameObject _camera;
        public GameObject Camera
        {
            get { return _camera; }

        }
        private GameObject _cameraGlobale;
        public GameObject CameraGlobale
        {
            get { return _cameraGlobale; }
        }
        private GameObject _cameraCinematique;
        public GameObject CameraCinematique
        {
            get { return _cameraCinematique; }
            set { _cameraCinematique = value; }
        }

        private string _zone;
        public string Zone
        {
            get { return _zone; }
            private set { _zone = value; }
        }






        //Cartes 
        //  chargées en fonction de la zone où est inclut la carte active
        // TODO : faire la différenciation entre les cartes de la zone et les cartes de tout le jeu
        //         -dépendances : chargement des données de pnjs
        private static List<GameObject> _cartes;
        public static List<GameObject> Cartes
        {
            get { return _cartes; }
            private set { _cartes = value; }
        }
        private static GameObject _carteActive;
        public static GameObject CarteActive
        {
            get { return _carteActive; }
            set { _carteActive = value; }
        }


        //Pnjs
        //
        //
        private static List<GameObject> _pnjs;
        public static List<GameObject> Pnjs
        {
            get { return _pnjs; }
        }


        //Quêtes actives de la sauvegarde
        // Différencier entre les quêtes du joueur et les quêtes totales en BD
        private static Dictionary<QueteParente, List<Quete>> _quetes;
        public static Dictionary<QueteParente, List<Quete>> Quetes
        { get { return _quetes; } private set { _quetes = value; } }


        private static List<Cinematique> _cinematiques;
        public static List<Cinematique> Cinematiques
        {
            get { return _cinematiques; }
            set { _cinematiques = value; }
        }
        private static Cinematique _cinematiqueActuelle;
        public static Cinematique CinematiqueActuelle
        {
            get { return _cinematiqueActuelle; }
        }

        //Gestion du joueur de la sauvegarde 
        private static GameObject _joueur;
        public static GameObject Joueur
        {
            get { return _joueur; }
            private set { _joueur = value; }
        }


        private static string pnjProche = "";
        public static string PnjProche
        {
            get { return pnjProche; }
            set { pnjProche = value; }
        }


        private static float timer = 3f;
        public static float Timer
        {
            get { return timer; }
            set { timer = value; }
        }
        private static bool compteur = false;
        public static bool Compteur
        {
            get { return compteur; }
            set { compteur = value; }
        }



        private bool _firstUpdate = true;
        private static bool _faitCinematique = false;
        public static bool FaitCinematique
        {
            get { return _faitCinematique; }
            set { _faitCinematique = value; }
        }
        private static int _indexCinematique = 0;
        public static int IndexCinematique
        {
            get { return _indexCinematique; }
            set { _indexCinematique = value; }
        }

        public bool initialisee = false;



        //Propriétés



        private System.Collections.IEnumerator initialiser()
        {
            yield return new WaitForEndOfFrame();

            //Initialiser les cartes et les quêtes de jeu 
            _cartes = AccesBD.TrouverCartes();
            //Initialisation du joueur monde
            if (JoueurMonde.Coor != Vector2.zero)
            {
                Destroy(_joueur);
                _joueur = Instantiate(Resources.Load("Prefab/player"), JoueurMonde.Coor, Quaternion.identity) as GameObject;
                _joueur.name = _joueur.name.Replace("(Clone)", "");
            }
            else
            {
                _joueur = Instantiate(Resources.Load("Prefab/player"), new Vector2(25, -125), Quaternion.identity) as GameObject;
                _joueur.name = _joueur.name.Replace("(Clone)", "");
            }

            JoueurMonde.PeutAgir = true;
            //_carteActive = TrouverCarte(new Vector2(_joueur.transform.position.x, Joueur.transform.position.y));


            //Initialisation de la caméra principale
            _camera = Instantiate(Resources.Load("Prefab/camera"), _joueur.transform.position, Quaternion.identity) as GameObject;
            _camera.name = _camera.name.Replace("(Clone)", "");
            _camera.transform.position = _joueur.transform.position;
            //if(DonneesJeu.)

            //Initialiser les cinématiques
            _cinematiques = AccesBD.TrouverCinematiques();

            //Initialiser les quêtes
            _quetes = AccesBD.TrouverToutesQuetes();

            //Initialiser les Pnjs
            _pnjs = AccesBD.TrouverPnjs();


            _carteActive = TrouverCarte(_joueur.transform.position);


            //Dictionary<QueteParente, List<Quete>>.Enumerator enumq = _quetes.GetEnumerator();

            //while (enumq.MoveNext())
            //{
            //    Debug.Log(enumq.Current.Key.Nom);
            //    foreach (Quete q in enumq.Current.Value)
            //    {
            //        Debug.Log(q.Nom);
            //    }

            //}





            initialisee = true;
        }

        void Awake()
        {
            StartCoroutine(initialiser());
            initialisee = false;
        }




        void Update()
        {
            if (_firstUpdate && initialisee)
            {
                InitialiserCartes();
                _firstUpdate = false;
            }

            if (Input.GetButtonUp("Accepter") && JoueurMonde.PeutAgir)
            {
                if (pnjProche != "")
                {
                    Queteur q = GameObject.Find(pnjProche).GetComponent<Queteur>();

                    if (q.Etat != EtatPnj.Quete)
                        q.VerifierAvanceeQuete();
                }
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!EcranQuete.EstActif && JoueurMonde.PeutAgir)
                {
                    EcranQuete.OuvrirEcranQuete("");
                }
                else
                    EcranQuete.FermerEcranQuete();
            }



            if (Input.GetKeyDown(KeyCode.C))
            {
                if (_camera.gameObject.activeSelf)
                {
                    _cameraGlobale = Instantiate(Resources.Load("Prefab/cameraGlobale"), _joueur.transform.position, Quaternion.identity) as GameObject;
                    _cameraGlobale.name = _cameraGlobale.name.Replace("(Clone)", "");
                    _camera.gameObject.SetActive(false);
                    JoueurMonde.PeutAgir = false;
                }
                else
                {
                    Destroy(_cameraGlobale);
                    _camera.gameObject.SetActive(true);
                    JoueurMonde.PeutAgir = true;
                }


            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                _faitCinematique = true;
                _cinematiqueActuelle = _cinematiques[0];
                Debug.Log(_cinematiqueActuelle.Nom);
            }

            //Debug.Log(JoueurMonde.Coor);
            if (Compteur)
                Timer -= Time.deltaTime;
            //Debug.Log(Timer);
            if (initialisee)
            {

                //Debug.Log(_carteActive.GetComponent<Carte>().EstHostile.ToString());

                if (timer < 0 && _carteActive.GetComponent<Carte>().EstHostile && !_faitCinematique)
                {
                    JoueurMonde.PeutAgir = false;
                    LancerCombat();
                    timer = UnityEngine.Random.Range(0,15);
                }
            }

            //if(Input.GetKeyDown(KeyCode.J))
            //{
            //    //AccesBD.TrouverQuetes();
            //}


            if(_cinematiqueActuelle != null && _faitCinematique)
            {

                _cinematiqueActuelle.DeclencherCinematique();
                //Debug.Log(_cinematiqueActuelle.Etat);





                switch (_cinematiqueActuelle.Etat)
                {
                    case EtatCinematique.Bloquee:
                        break;
                    case EtatCinematique.Disponible:


                        _cinematiqueActuelle.DeclencherCinematique();
                        break;
                    case EtatCinematique.EnCours:
                        _cinematiqueActuelle.FaireCinematique();
                        break;
                    case EtatCinematique.Terminee:
                        _cinematiqueActuelle = null;
                        break;
                    case EtatCinematique.NULL:
                        break;
                    default:
                        break;
                }
            }


        }


        //Méthode pour distribuer les gains à l'équipe du joueur
        public static void DistribuerGains(int idQuete, List<Gain> gains)
        {
            List<Gain>.Enumerator enumGain = gains.GetEnumerator();

            while (enumGain.MoveNext())
            {
                switch (enumGain.Current.Type)
                {
                    case TypeGain.Argent:

                        break;
                    case TypeGain.Objet:

                        break;
                    case TypeGain.Competence:

                        break;
                    case TypeGain.GainQuete:

                        break;
                    default:

                        break;
                }
            }

            //TODO : Notifier le joueur que les gains ont bien été distribués
            //TODO : Gérer le débloquement de la quête suivante si existente


        }

        //Méthode pour vérifier si l'objectif validé était le déclencheur d'une cinématique
        public static void VerifierCinematiques(int id)
        {
            foreach (Cinematique c in _cinematiques)
            {
                if (c.IDObjectif == id)
                {
                    c.DeclencherCinematique();
                    break;
                }
            }
        }

        //Méthode pour trouver l'état d'une cinématique
        public static EtatCinematique TrouverEtatCinematique(int id)
        {
            foreach (Cinematique c in _cinematiques)
            {
                if (c.ID == id)
                {
                    return c.Etat;
                }
            }

            return EtatCinematique.NULL;
        }

        //Méthode pour initialiser les points de connexion des cartes
        private void InitialiserCartes()
        {

            //TODO : Initialiser les cartes avec cette fonction en utilisant les données de la BD



            for (int i = 0; i < _cartes.Count; i++)
            {

                Carte uneCarte = _cartes[i].GetComponent<Carte>();


                if (uneCarte.Nom == "carte11" || uneCarte.Nom == "carte65" || uneCarte.Nom == "maison02_00" || uneCarte.Nom == "maison02_01" || uneCarte.Nom == "maison55_00" || uneCarte.Nom == "maison55_00_00")
                    continue;

                //Ajout des connexions entre maps
                int x = Int32.Parse(uneCarte.Nom.Substring(5, 1));
                int y = Int32.Parse(uneCarte.Nom.Substring(6, 1));



                if (uneCarte.gameObject.transform.Find("exits"))
                {



                    Transform exit = uneCarte.gameObject.transform.Find("exits");
                    foreach (Transform child in exit)
                    {
                        Transform warpTarget = child.GetComponent<Warp>().warpTarget;

                        //Debug.Log(uneCarte.Nom);
                        switch (child.name)
                        {
                            case "north_ex":
                                child.GetComponent<Warp>().warpTarget = GameObject.Find("carte" + x.ToString() + (y - 1).ToString()).transform.Find("arrivals").transform.Find("south_arr");
                                break;
                            case "south_ex":
                                child.GetComponent<Warp>().warpTarget = GameObject.Find("carte" + x.ToString() + (y + 1).ToString()).transform.Find("arrivals").transform.Find("north_arr");
                                break;
                            case "east_ex":
                                child.GetComponent<Warp>().warpTarget = GameObject.Find("carte" + (x + 1).ToString() + y.ToString()).transform.Find("arrivals").transform.Find("west_arr");
                                break;
                            case "west_ex":
                                child.GetComponent<Warp>().warpTarget = GameObject.Find("carte" + (x - 1).ToString() + y.ToString()).transform.Find("arrivals").transform.Find("east_arr");
                                break;
                        }
                    }
                }
            }
        }



        //Méthode pour trouver un pnj selon son id
        public static GameObject TrouverPnj(int id)
        {
            foreach (var pnj in GestionMonde.Pnjs)
            {
                if (pnj.GetComponent<Pnj>().ID == id)
                    return pnj;
            }

            return null;
        }

        //Méthode pour trouver une cinématique selon son id
        public static Cinematique TrouverCinematique(int id)
        {
            foreach (Cinematique c in _cinematiques)
            {
                if (c.ID == id)
                    return c;
            }

            return null;
        }


        //Méthode pour trouver une quête parente selon son nom 
        public static QueteParente TrouverQueteParente(string nomQueteParente)
        {
            foreach (QueteParente key in _quetes.Keys)
            {
                if (key.Nom == nomQueteParente)
                    return key;
            }

            return null;
        }

        //Méthode pour trouver une quête selon son id
        public static Quete TrouverQuete(int id)
        {
            Dictionary<QueteParente, List<Quete>>.Enumerator enumQueteP = _quetes.GetEnumerator();

            while (enumQueteP.MoveNext())
            {
                foreach (Quete q in enumQueteP.Current.Value)
                {
                    if (q.ID == id)
                        return q;
                }
            }

            return null;
        }

        //Méthode pour trouver une quête selon son nom
        public static Quete TrouverQuete(string n)
        {
            Dictionary<QueteParente, List<Quete>>.Enumerator enumQueteP = _quetes.GetEnumerator();

            while (enumQueteP.MoveNext())
            {
                foreach (Quete q in enumQueteP.Current.Value)
                {
                    if (q.Nom == n)
                        return q;
                }
            }

            return null;
        }

        //Méthode pour trouver l'état d'une quête
        public static EtatQuete TrouverEtatQuete(int id)
        {
            Dictionary<QueteParente, List<Quete>>.Enumerator enumQueteP = _quetes.GetEnumerator();

            while (enumQueteP.MoveNext())
            {
                foreach (Quete q in enumQueteP.Current.Value)
                {
                    if (q.ID == id)
                        return q.Etat;
                }
            }

            return EtatQuete.NULL;
        }

        //Méthode pour trouver un objectif selon son id
        public static Objectif TrouverObjectif(int id)
        {
            Dictionary<QueteParente, List<Quete>>.Enumerator enumQueteP = _quetes.GetEnumerator();

            while (enumQueteP.MoveNext())
            {
                foreach (Quete q in enumQueteP.Current.Value)
                {
                    foreach (Objectif o in q.Objectifs)
                    {
                        if (o.ID == id)
                            return o;
                    }
                }
            }

            return null;
        }

        //Méthode pour trouver une carte selon son id
        public static GameObject TrouverCarte(int id)
        {
            foreach (GameObject c in _cartes)
            {
                if (c.GetComponent<Carte>().ID == id)
                    return c;
            }

            return null;
        }

        //Méthode pour trouver une carte selon son nom
        public static GameObject TrouverCarte(string n)
        {
            foreach (GameObject c in _cartes)
            {
                if (c.GetComponent<Carte>().Nom == n)
                    return c;
            }

            return null;
        }

        //
        public static GameObject TrouverCarte(Vector2 pos)
        {
            foreach (GameObject c in _cartes)
                if (pos.x - c.transform.position.x < 50 && pos.x - c.transform.position.x > 0 && pos.y - c.transform.position.y < 0 && pos.y - c.transform.position.y > -50)
                    return c;

            return null;
        }

        //
        public static List<GameObject> TrouverActeurs(int id)
        {
            List<GameObject> la = new List<GameObject>();

            foreach (Evenement e in JoueurMonde.EvenementsCinematiques)
            {
                if(e.IdEtape == id)
                {
                    la.Add(JoueurMonde.Moi.gameObject);
                    break;
                }
            }
            foreach (GameObject p in _pnjs)
            {
                foreach (Evenement e in p.GetComponent<Pnj>().EvenementsCinematiques)
                {
                    if(e.IdEtape == id)
                    {
                        la.Add(p);
                        break;
                    }
                }
            }

            return la;
        }

        //
        public static void Reinitialiser()
        {
            _quetes = AccesBD.TrouverToutesQuetes();
        }



        public void LancerCombat()
        {
            DonneesJeu.Adversaires = new Equipe(_carteActive.GetComponent<Carte>().EquipesMonstres);
            JoueurMonde.Coor = JoueurMonde.Moi.transform.position;
            GestScene.ProchaineSceneTransition("Combat");
        }

        public static void VerifierExplorations(GameObject c)
        {
            Dictionary<QueteParente, List<Quete>>.Enumerator enumQuete = _quetes.GetEnumerator();

            while (enumQuete.MoveNext())
            {
                foreach (Quete q in enumQuete.Current.Value)
                {
                    int compteurc = 0;
                    foreach (Objectif o in q.Objectifs)
                    {
                        if(o.Type == TypeObjectif.Explorer && c == ((ObjectifExplorer)o).CarteAExplorer && q.ObjectifsValides == compteurc)
                        {
                            o.ValiderObjectif();
                            EcranNotification.NouvelleNotification(o);
                            break;
                        }
                        compteurc++;
                    }
                }
            }

        }
    }
}


