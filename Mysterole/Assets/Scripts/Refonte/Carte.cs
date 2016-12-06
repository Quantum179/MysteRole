using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiled2Unity;
using System.Text;
using System;

namespace Mysterole {

    public class Carte : MonoBehaviour
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private ZoneCarte _zone;
        public ZoneCarte Zone
        {
            get { return _zone; }
            set { _zone = value; }
        }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private bool _estHostile;
        public bool EstHostile
        {
            get { return _estHostile; }
            set { _estHostile = value; }
        }

        //Propriétés
        private TiledMap _cartePrefab;
        public TiledMap CartePrefab
        {
            get { return _cartePrefab; }
            private set { }
        }

        private List<GameObject> _pnjs;
        public List<GameObject> Pnjs
        {
            get { return _pnjs; }
            private set { }
        }

        private List<Equipe> _equipesMonstres;
        public Equipe EquipesMonstres
        {
            get
            {
                int r = UnityEngine.Random.Range(0, DonneesJeu.EquipesMonstre.Count);

                return DonneesJeu.EquipesMonstre[r];
            }
        }


        void Awake()
        {
            gameObject.name = gameObject.name.Replace("(Clone)", "");
            _nom = gameObject.name;
            //Debug.Log(_nom);
            List<string> li = AccesBD.TrouverIdCarte(_nom);
            _id = int.Parse(li[0]);
            _estHostile = bool.Parse(li[1].ToString());

            _cartePrefab = gameObject.GetComponent<TiledMap>();




            //_pnjs = new List<GameObject>();
            //_equipeMonstres = new List<Equipe>();
        }

        void Start()
        {
            //Réflechir sur Awake et Start 
            _nom = gameObject.name;


            //if (_nom == "carte00")
            //{
            //    GameObject unPnj = Instantiate(Resources.Load("Prefab/Pnjs/Sage du village"), new Vector2(27, -27), Quaternion.identity) as GameObject;
            //    unPnj.name = unPnj.name.Replace("(Clone)", "");
            //    _pnjs.Add(unPnj);

            //    GameObject unPnj2 = Instantiate(Resources.Load("Prefab/Pnjs/Chef milicien"), new Vector2(40, -40), Quaternion.identity) as GameObject;
            //    unPnj2.name = unPnj2.name.Replace("(Clone)", "");
            //    _pnjs.Add(unPnj2);

            //}






        }


        private void InitialiserPnjs()
        {
            //_pnjs.Add(new Queteur());
        }

        private void InitialiserEquipesMonstres()
        {
            //TODO : faire un héritage spéciale pour l'affichage des équipes de monstres
            //DonneesJeu.EquipesMonstre
        }


        public void ChoisirEquipeMonstre(string mode)
        {
            switch(mode)
            {
                case "aléatoire":

                    break;
                case "quete":

                    break;
                case "cinématique":
                    
                    break;
                default:

                    break;
            }
        }
	}
}

