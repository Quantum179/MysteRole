using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class ObjectifParler : Objectif
    {
        private int _nbPnjs;
        public int NbPnjs
        {
            get { return _nbPnjs; }
        }

        private List<int> _pnjs;
        public List<int> Pnjs
        {
            get { return _pnjs; }
        }

        public ObjectifParler() : base()
        {
            _nbPnjs = 0;
            _pnjs = null;
        }

        public ObjectifParler(int id, Quete q, TypeObjectif to, int res, string param, bool ev) 
            : base(id, q, to, res, ev)
        {
            string[] lst = param.Split(';');
            _nbPnjs = int.Parse(lst[0]);
            string[] pnjs = lst[1].Split(',');
            _pnjs = new List<int>();
            foreach (var pnj in pnjs)
            {
                int tmp;
                if (int.TryParse(pnj.Trim(), out tmp))
                {
                    _pnjs.Add(int.Parse(pnj));
                }
            }
            _description = lst[2];
        }

        public override bool ValiderObjectif()
        {
            return false;
        }
    }
}