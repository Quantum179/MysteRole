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
        public Button val;
        public Button refus;

        private static Text titre;
        private static Text description;
        private static Button valider;
        private static Button refuser;

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
            valider = val;
            refuser = refus;


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

        public static void NouvelleNotification(TypeNotification tn, string message)
        {
            Main.gameObject.SetActive(true);

            switch (tn)
            {
                case TypeNotification.Quete:

                    titre.text = "Mise à jour quête";

                    break;
                case TypeNotification.Objectif:
                    titre.text = "Mise à jour objectif";
                    break;
            }
        }

        

    }
}
