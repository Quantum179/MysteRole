using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mysterole
{
    public class EcranNotification : MonoBehaviour
    {
        static EcranNotification Main;

        public Text ti;
        public Text des;
        public Text qu;
        public Text ob;

        private static Text titre;
        private static Text description;
        private static Text quete;
        private static Text objectif;

        private static bool estActif = false;
        public static bool EstActif
        {
            get { return estActif; }
        }

        private float timer = 5f;


        private void Start()
        {
            Main = this;

            titre = ti;
            description = des;
            quete = qu;
            objectif = ob;


            gameObject.SetActive(false);
        }

        void Update()
        {
            if(gameObject.activeSelf)
            {
                timer -= Time.deltaTime;

                if(timer < 0)
                {
                    gameObject.SetActive(false);
                    timer = 5f;
                }
            }
        }

        private static void Reset()
        {
            titre.text = "";
            description.text = "";
        }
        //public static void OuvrirEcranNotif()
        //{
        //    Main.gameObject.SetActive(true);
        //}

        public static void FermerEcranNotif(float temps)
        {
            Main.gameObject.SetActive(false);
        }

        public static void NouvelleNotification(Quete q)
        {
            Main.gameObject.SetActive(true);
            titre.text = "Mise à jour de quête";
            Debug.Log(q.Nom);
            quete.text = q.Nom;
            

            switch (q.Etat)
            {
                case EtatQuete.Disponible:
                    description.text = "Nouvelle quête disponible : " + q.Nom + "\n\n" + q.Description;
                    break;
                case EtatQuete.EnCours:
                    description.text = "Nouvelle quête en cours : " + q.Nom + "\n\n" + q.Description;
                    break;
                case EtatQuete.Terminee:
                    description.text = "Quête terminée : " + q.Nom + "\n\n" + q.Description + "\n\nVoir gains dans l'écran de quêtes.";
                    break;
            }

        }

        public static void NouvelleNotification(Objectif o)
        {
            Main.gameObject.SetActive(true);
            titre.text = "Mise à jour d'objectif";

            description.text = "Objectif mis à jour : " + o.Description;

        }

    }
}
