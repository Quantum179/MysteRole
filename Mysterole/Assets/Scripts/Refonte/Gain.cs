using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Mysterole
{
    public abstract class Gain
    {
        //Attributs
        protected TypeGain _type;

        //Propriétés
        public TypeGain Type 
        {
            get { return _type; }
            private set { _type = value; }
        }

        public Gain()
        {

        }
    }
}
