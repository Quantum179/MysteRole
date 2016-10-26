using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class InfosWorld
    {
        public string carte;
        public GameObject player;

        public InfosWorld(string c, GameObject p)
        {
            carte = c;
            player = p;
        }

    }
}
