using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Personnage
    {
        public string Nom { get; private set; }
        public uint NIV { get; private set; }
        public uint PV
        {
            set
            {
                ;
            }
        }
        public uint MPV
        {
            get
            {
                return 0; // TODO
            }
            private set
            {
                ; // TODO
            }
        }
        public uint PC { get; private set; }
        public uint MPC
        {
            get
            {
                return 0; // TODO
            }
            private set
            {
                ; // TODO
            }
        }
        public uint PUI
        {
            get
            {
                return 0; // TODO
            }
            private set
            {
                ; // TODO
            }
        }
        public uint DEF
        {
            get
            {
                return 0; // TODO
            }
            private set
            {
                ; // TODO
            }
        }
        public uint VIT
        {
            get
            {
                return 0; // TODO
            }
            private set
            {
                ; // TODO
            }
        }
        protected struct Bonus
        {
            uint PUI;
            uint DEF;
            uint VIT;
        }
        public Role Role { get; private set; }
        protected Personnage(string Nom, Role Role, int Niveau)
        {
            // TODO
        }

    }
    public class Joueur : Personnage
    {
        public float EXP
        {
            set
            {
                ;
            }
        }
        public Joueur(string Nom, RoleJoueur Role, int Niveau) : base(Nom, Role, Niveau)
        {
            // TODO
        }
    }
    public class Enemie : Personnage
    {
        public Enemie(string Nom, Stats Stats, RoleEnemie Role, int Niveau) : base(Nom, Role, Niveau)
        {
            // TODO
        }
    }
}
