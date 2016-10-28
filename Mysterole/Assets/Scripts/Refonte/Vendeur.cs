using System;
using System.Collections.Generic;

namespace Mysterole
{
	public class Vendeur : Pnj
	{
		private Dictionary<string, ObjetPnj> _objets;

        public Dictionary<string, ObjetPnj> Objets {
            get { return _objets; }
            private set { _objets = value; }
        }


		public Vendeur () : base()
		{
		}






	}
}

