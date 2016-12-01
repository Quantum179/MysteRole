using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysterole
{
    public class Notification
    {

        protected string _titre;
        public string Titre
        {
            get { return _titre; }
            set { _titre = value; }
        }

        protected string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        protected TypeNotification _type;
        public TypeNotification Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Notification()
        {
            _titre = "";
            _description = "";
        }

        public Notification(string t, string d)
        {
            _titre = t;
            _description = d;
        }

        //public abstract void NouvelleNotification();

    }
}
