using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysterole

{
    public class Quete
    {
        public List<bool> avancee;


        public Quete()
        {

        }

        public bool checkPrevious(int index)
        {
            if (avancee[index - 1])
            {
                avancee[index] = true;
                return true;
            }

            return false;
            
        }
    }
}
