using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Mysterole
{
    public class Bandeau : MonoBehaviour
    {
        static GameObject Main;
        public Text message;
        static Text m;

        private float decompte = 3f;

        void Start()
        {
            Main = this.gameObject;
            m = message;
            Main.SetActive(false);
        }

        void Update()
        {
            if(Main.activeSelf)
                decompte -= Time.deltaTime;

            if (decompte < 0)
            {
                Main.SetActive(false);
                decompte = 3f;
            }
        }

        public static void NouveauBandeau(ZoneCarte z)
        {
            switch (z)
            {
                case ZoneCarte.Foret:
                    m.text = "Forêt";
                    break;
                case ZoneCarte.Cite:
                    m.text = "Cité";
                    break;
                case ZoneCarte.Caverne:
                    m.text = "Caverne";
                    break;
                case ZoneCarte.NULL:
                    m.text = "identifié";
                    break;
            }

            Main.SetActive(true);
        }
    }
}
