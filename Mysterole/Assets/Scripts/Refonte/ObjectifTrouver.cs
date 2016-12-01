using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class ObjectifTrouver : Objectif
    {
        //private Objets.Objet _objetRecherche;
        //public Objets.Objet ObjetRecherche
        //{
        //    get { return _objetRecherche; }
        //}

        public ObjectifTrouver(int id, Quete q, TypeObjectif to, int res, string param, bool ev)
            : base(id, q, to, res, ev)
        {
            string[] lst = param.Split(';');
            //_objetRecherche = new Objets.Equipement();

            _description = lst[1];
        }

        public override bool ValiderObjectif()
        {
            return false;
        }

    }
}
