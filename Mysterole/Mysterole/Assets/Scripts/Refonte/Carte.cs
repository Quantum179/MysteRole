using System.Collections;
using System.Collections.Generic;

namespace Mysterole {
	public class Carte {

        private string _nom;
		private List<Pnj> _pnjs;

        //Propriétés
        public string Nom
        {
            get { return _nom; }
            private set { }
        }
        public List<Pnj> Pnjs
        {
            get { return _pnjs; }
            private set { }
        }

        public Carte()
        {

        }

        private void InitialiserPnjs()
        {
            _pnjs.Add(new Queteur());
        }
	}
}

