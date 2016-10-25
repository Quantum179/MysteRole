using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class IA : MonoBehaviour
    {
        //private Dictionary<bool, Deplacement> _deplacements;
        //public Dictionary<bool, Deplacement> Deplacements { get; private set; }

        //private List<Quete> _quetes;
        //public List<Quete> Quetes { get; private set; }

        private Dictionary<bool, Dialogue> _dialogues;
        public Dictionary<bool, Dialogue> Dialogues { get; private set; }

        private List<Evenement> _evenements;
        public List<Evenement> Evenement { get; private set; }
        

        private string _nomPnj;
        public string NomPnj { get; private set; }



        //public IA()
        void Start()
        {
            //Initialisation des données avec des requêtes SQL
            //Hardcode

            //Connexion au Game Object du pnj
            //TODO : générer les dessins (sprites et collisions) automatiquement à partir des informations de la BD





            switch(_nomPnj)
            {
                case "Chef milicien":
                    _evenements.Add(new Dialogue("Halte là"));
                    _evenements.Add(new Deplacement())

                    break;
                case "Sage du village":

                    break;
                case "Milicien":

                    break;
            }






        }

        void Update()
        {

        }
        public static void TriggerEvent()
        {

        }
    }
}
