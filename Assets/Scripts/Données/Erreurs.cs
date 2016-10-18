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
        
        void Start()
        {
            Main = gameObject;
            tz = textZone;
            gameObject.SetActive(false);
        }
        static public void NouvelleErreur(string message)
        {
            tz.text += message + '\n';
            Main.SetActive(true);
        }

        public void OnClick()
        {
            gameObject.SetActive(false);
        }
    }
}
