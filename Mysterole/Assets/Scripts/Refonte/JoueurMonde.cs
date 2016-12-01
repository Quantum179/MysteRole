using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Tiled2Unity;


namespace Mysterole
{
    public class JoueurMonde : MonoBehaviour
    {
        private static JoueurMonde _moi;
        public static JoueurMonde Moi
        {
            get { return _moi; }
        }


        private Joueur _joueur;
        public Joueur Joueur
        {
            get { return _joueur; }
            private set { }
        }


        private Role roleJoueur;

        private Animator _anim;
        private Rigidbody2D _rbody;
        private Collider2D _hitbox;



        private static bool _peutAgir = true;
        public static bool PeutAgir
        {
            get { return _peutAgir; }
            set { _peutAgir = value; }
        }

        private static Vector2 _coor;
        public static Vector2 Coor
        {
            get { return _coor; }
            set
            {
                //Vector2 v = new Vector2(value.x - (value.x % 32), value.y - (value.y % 32));
                _coor = value;
                JoueurMonde.Moi.transform.position = value;
            }
        }



        private TiledMap _carte;

        public Rigidbody2D Body
        {
            get { return _rbody; }
            private set { }
        }
        private static bool _faitCinematique = false;
        public static bool FaitCinematique
        {
            get { return _faitCinematique; }
            set { _faitCinematique = value; }
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


        private static Evenement _evenementActuel;
        public static Evenement EvenementActuel
        {
            get { return _evenementActuel; }
            set { _evenementActuel = value; }
        }


        void Awake()
        {

            //Partie sûrement à faire dans une fonction dédiée à la BD
            //Competence c1 = new Competence("Slash", )
            //Role unRole = new Role("Guerrier", 50, 50, 20, )

            if(DonneesJeu.Equipe.Membres.Count <= 0)
            {
                _joueur = new Joueur("Quantum", DonneesJeu.Roles[0], 5);
                DonneesJeu.Equipe.AjoutMembre(_joueur);
            }


        }

        void Start()
        {

            _moi = this;
            _anim = gameObject.GetComponent<Animator>();
            _rbody = gameObject.GetComponent<Rigidbody2D>();

            //_evenementsCinematique = new Dictionary<int, Dictionary<int, Evenement>>();
        }


        void Update()
        {

            if(_faitCinematique)
            {


                if (_seDeplace)
                    EffectuerDeplacement();

                if ((Vector2)_rbody.transform.position == ((Deplacement)_evenementActuel).Destination && !_faitPause)
                {
                    _seDeplace = false;
                    _anim.SetBool("isWalking", false);
                    _evenementActuel.Etat = EtatEvenement.Fini;
                }

                if (_faitPause)
                {
                    _evenementActuel.Decompte -= Time.deltaTime;

                    if (_evenementActuel.Decompte < 0)
                        _evenementActuel.Etat = EtatEvenement.Fini;
                }


                switch (_evenementActuel.Etat)
                {
                    case EtatEvenement.EnAttente:
                        _evenementActuel.DeclencherEvenement(this);
                        break;
                    case EtatEvenement.EnCours:

                        if (_evenementActuel.PeutContinuer)
                        {
                            _evenementActuel.Decompte -= Time.deltaTime;
                            if (_evenementActuel.Type != TypeEvenement.Attente && _evenementActuel.Decompte < 0)
                                _evenementActuel.Etat = EtatEvenement.Fini;
                        }
                        else if (_evenementActuel.Type == TypeEvenement.Dialogue && Input.GetKeyDown(KeyCode.E))
                            _evenementActuel.Etat = EtatEvenement.Fini;

                        break;
                    case EtatEvenement.Fini:
                        if (_evenementActuel.Type == TypeEvenement.Dialogue && Input.GetKeyDown(KeyCode.E))
                            EcranDialogue.FermerDialogue();

                        break;
                    default:
                        break;
                }
            }
            else
            {
                //Factoriser l'update en plusieurs fonctions précises
                _carte = GestionMonde.CarteActive.GetComponent<TiledMap>();

                Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

                if (movement_vector != Vector2.zero && _peutAgir)
                {
                    _anim.SetBool("isWalking", true);
                    _anim.SetFloat("input_x", movement_vector.x);
                    _anim.SetFloat("input_y", movement_vector.y);
                    GestionMonde.Compteur = true;
                }
                else
                {
                    _anim.SetBool("isWalking", false);
                    GestionMonde.Compteur = false;

                }

                if (_rbody.position.x + movement_vector.x > _carte.transform.position.x &&
                    _rbody.position.x + movement_vector.x < _carte.transform.position.x + _carte.NumTilesWide &&
                    _rbody.position.y + movement_vector.y < _carte.transform.position.y &&
                    _rbody.position.y + movement_vector.y > _carte.transform.position.y - _carte.NumTilesHigh && _peutAgir)
                {
                    _rbody.MovePosition(_rbody.position + movement_vector * Time.deltaTime * 20);
                    _coor = transform.position;
                }

            }













        }


        public void Teleporter(int id, Vector2 pos)
        {
            transform.position = pos;
            GestionMonde.CarteActive = GestionMonde.TrouverCarte(id);
        }

        public void EffectuerDeplacement()
        {
            Vector2 offset = new Vector2(((Deplacement)_evenementActuel).Destination.x - _rbody.position.x, ((Deplacement)_evenementActuel).Destination.y - _rbody.position.y);

            _anim.SetBool("isWalking", true);
            _anim.SetFloat("input_x", offset.x);
            _anim.SetFloat("input_y", offset.y);


            transform.position = Vector3.MoveTowards(transform.position, ((Deplacement)_evenementActuel).Destination, Time.deltaTime * 5);
        }




    }
}
