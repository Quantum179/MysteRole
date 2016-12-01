using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Mysterole
{
    public abstract class Gain
    {
        //Attributs
        protected string _description;
        public string Description
        {
            get { return _description; }
        }

        protected TypeGain _type;
        public TypeGain Type 
        {
            get { return _type; }
            private set { _type = value; }
        }

        public Gain()
        {

        }

        public Gain(string d)
        {
            _description = d;
        }
    }
}
