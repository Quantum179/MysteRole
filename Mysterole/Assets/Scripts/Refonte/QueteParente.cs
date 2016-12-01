using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysterole
{
    public class QueteParente
    {
        //Attributs
        private int _id;
        public int ID
        {
            get { return _id; }
        }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
        }

        public QueteParente(int id, string n)
        {
            _id = id;
            _nom = n;
        }
    }
}
