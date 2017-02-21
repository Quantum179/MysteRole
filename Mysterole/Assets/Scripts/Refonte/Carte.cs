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
        }

        void Start()
        {
            _nom = gameObject.name;
        }


        private void InitialiserPnjs()
        {

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

