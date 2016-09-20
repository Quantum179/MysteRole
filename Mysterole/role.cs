using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Role
    {
        protected Role()
        {
            // TODO
        }
        public uint PrendrePUI(int Niveau)
        {
            return 0; // TODO
        }
        public uint PrendreDEF(int Niveau)
        {
            return 0; // TODO
        }
        public uint PrendreVIT(int Niveau)
        {
            return 0; // TODO
        }
    }
    public class RoleJoueur : Role
    {
        // TODO
    }
    public class RoleEnemie : Role
    {
        // TODO
    }
}
