using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Mysterole
{
    class Erreurs : MonoBehaviour
    {
        static GameObject Main;
        public Text textZone;
        static Text tz;
        static bool change = false;
        
        void Start()
        {/*
            Main = gameObject;
            tz = textZone;
            gameObject.SetActive(false);*/
        }
        void OnGUI()
        {/*
            if (change)
            {
                Main.GetComponentInChildren<Scrollbar>().value = 0;
                change = false;
            }*/
        }
        static public void NouvelleErreur(string message)
        {/*
            tz.text += message + '\n';
            Main.SetActive(true);
            change = true;*/
        }

        public void OnClick()
        {/*
            gameObject.SetActive(false);*/
        }
    }
}
