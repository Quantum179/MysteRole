using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Mysterole
{
    public class GainArgent : Gain
    {
        private float _montant;
        //Prévoir une économie (taxes, cour de la monnaie

        public GainArgent(string d, float m) : base(d)
        {
            _montant = m;
            _type = TypeGain.Argent;
        }
    }
}
