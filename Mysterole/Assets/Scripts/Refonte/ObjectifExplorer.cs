using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class ObjectifExplorer : Objectif
    {
        private GameObject _carteAExplorer;
        private GameObject CarteAExplorer
        {
            get { return _carteAExplorer; }
        }

        public ObjectifExplorer(int id, Quete q, TypeObjectif to, int res, string param, bool ev)
            : base(id, q, to, res, ev)
        {
            string[] lst = param.Split(';');


            _carteAExplorer = GestionMonde.TrouverCarte(lst[0]);
            _description = lst[1];

        }

        public override bool ValiderObjectif()
        {

            return false;
        }
    }
}
