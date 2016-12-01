using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class ObjectifBattre : Objectif
    {
        private Equipe _equipeABattre;
        public Equipe EquipeABattre
        {
            get { return _equipeABattre; }
        }

        public ObjectifBattre(int id, Quete q, TypeObjectif to, int res, string param, bool ev)
            : base(id, q, to, res, ev)
        {
            string[] lst = param.Split(';');
            //_equipeABattre = new Equipe();

            _description = lst[1];
        }

        public override bool ValiderObjectif()
        {
            return false;
        }
    }
}
