// Programme : Role
// Auteur : Jean-Michel Beauvais
// Classes et fonctions pour les rôles de personnage. Plus communemment référé par « Classe » dans l'univers des jeux de rôle.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Role
    {
        private uint bPUI;
        private uint bDEF;
        private uint bVIT;
        private uint bMOV;
        private uint bMPC;
        private int _id = 0;
        public string Nom { get; private set; }
        public Competence Base { get; private set; }
        public Competence Speciale { get; private set; }
        public Role(int ID, string Nom, uint PUI, uint DEF, uint VIT, Competence Base, Competence Speciale)
        {
            this.Nom = Nom;
            bPUI = PUI;
            bDEF = DEF;
            bVIT = VIT;
            bMOV = 5;
            bMPC = 1;
            _id = ID;
            this.Base = Base;
            this.Speciale = Speciale;
        }
        /*public Role(string Nom, uint PUI, uint DEF, uint VIT, Competence Base, Competence Speciale)
        {
            Debug.LogWarning("Mysterole.Role : Une update fut produite. Veuillez ajouter un ID pour le rôle.");
            this.Nom = Nom;
            bPUI = PUI;
            bDEF = DEF;
            bVIT = VIT;
            bMOV = 5;
            bMPC = 1;
            this.Base = Base;
            this.Speciale = Speciale;
        }*/
        protected uint PrendreStat(uint iBase, uint Niveau)
        {
            uint bonus = (uint)Math.Floor(((decimal)Niveau) / 2);
            return iBase + bonus;
        }
        public uint PrendrePUI(uint Niveau)
        {
            return PrendreStat(bPUI, Niveau);
        }
        public uint PrendreDEF(uint Niveau)
        {
            return PrendreStat(bDEF, Niveau);
        }
        public uint PrendreVIT(uint Niveau)
        {
            return PrendreStat(bVIT, Niveau);
        }
        public uint PrendreMOV(uint Niveau)
        {
            return bMOV;
        }
        public uint PrendreMPC(uint Niveau)
        {
            return bMPC;
        }
        public uint PrendreMPV(uint Niveau)
        {
            return PrendreDEF(Niveau) + PrendreVIT(Niveau);
        }
        public override bool Equals(object o)
        {
            if (o == null)
                return false;
            Role r = o as Role;
            if ((object)r == null)
                return false;
            return r.ID == ID;
        }
        public bool Equals(Role r)
        {
            if ((object)r == null)
                return false;
            return r.ID == ID;
        }
        public int ID
        {
            get
            {
                return _id;
            }
        }
        public override int GetHashCode()
        {
            return Nom.GetHashCode()*2 + (int)PrendrePUI(1)*8 + (int)PrendreDEF(1)*6 + (int)PrendreVIT(1)*4;
        }
    }
    public class RoleJoueur : Role
    {
        public RoleJoueur(string Nom, Stats statA, Stats statB, Stats statC, Competence Base, Competence Speciale) : base(-1, Nom,
                                                                                    (statA == Stats.PUI ? (uint)10 : statB == Stats.PUI ? (uint)7 : statC == Stats.PUI ? (uint)5 : (uint)1),
                                                                                    (statA == Stats.DEF ? (uint)10 : statB == Stats.DEF ? (uint)7 : statC == Stats.DEF ? (uint)5 : (uint)1),
                                                                                    (statA == Stats.VIT ? (uint)10 : statB == Stats.VIT ? (uint)7 : statC == Stats.VIT ? (uint)5 : (uint)1),
                                                                                    Base,
                                                                                    Speciale)
        {
            Debug.LogWarning("Mysterole.RoleJoueur : Une update fut produite. Veuillez ajouter un ID pour le rôle.");
            Initialiser(Nom, statA, statB, statC, Base, Speciale);
        }
        public RoleJoueur(int ID, string Nom, Stats statA, Stats statB, Stats statC, Competence Base, Competence Speciale) : base(ID, Nom,
                                                                                    (statA == Stats.PUI ? (uint)10 : statB == Stats.PUI ? (uint)7 : statC == Stats.PUI ? (uint)5 : (uint)1),
                                                                                    (statA == Stats.DEF ? (uint)10 : statB == Stats.DEF ? (uint)7 : statC == Stats.DEF ? (uint)5 : (uint)1),
                                                                                    (statA == Stats.VIT ? (uint)10 : statB == Stats.VIT ? (uint)7 : statC == Stats.VIT ? (uint)5 : (uint)1),
                                                                                    Base,
                                                                                    Speciale)
        { Initialiser(Nom, statA, statB, statC, Base, Speciale); }
        private void Initialiser(string Nom, Stats statA, Stats statB, Stats statC, Competence Base, Competence Speciale) { 
            if (statA != Stats.PUI && statA != Stats.DEF && statA != Stats.VIT)
                throw new ArgumentException("Les statistiques valides à l'initiation sont PUI, DEF et VIT.");
            if (statB != Stats.PUI && statB != Stats.DEF && statB != Stats.VIT)
                throw new ArgumentException("Les statistiques valides à l'initiation sont PUI, DEF et VIT.");
            if (statC != Stats.PUI && statC != Stats.DEF && statC != Stats.VIT)
                throw new ArgumentException("Les statistiques valides à l'initiation sont PUI, DEF et VIT.");
            bool PUI = false;
            bool DEF = false;
            bool VIT = false;
            if (statA == Stats.PUI)
                PUI = true;
            if (statA == Stats.DEF)
                DEF = true;
            if (statA == Stats.VIT)
                VIT = true;
            if (statB == Stats.PUI)
            {
                if (PUI)
                    throw new ArgumentException("Impossible d'utiliser deux fois la même statistique.");
                PUI = true;
            }
            if (statB == Stats.DEF)
            {
                if (DEF)
                    throw new ArgumentException("Impossible d'utiliser deux fois la même statistique.");
                DEF = true;
            }
            if (statB == Stats.VIT)
            {
                if (VIT)
                    throw new ArgumentException("Impossible d'utiliser deux fois la même statistique.");
                VIT = true;
            }
            if (statC == Stats.PUI)
            {
                if (PUI)
                    throw new ArgumentException("Impossible d'utiliser deux fois la même statistique.");
                PUI = true;
            }
            if (statC == Stats.DEF)
            {
                if (DEF)
                    throw new ArgumentException("Impossible d'utiliser deux fois la même statistique.");
                DEF = true;
            }
            if (statC == Stats.VIT)
            {
                if (VIT)
                    throw new ArgumentException("Impossible d'utiliser deux fois la même statistique.");
                VIT = true;
            }

            if (!(PUI && DEF && VIT))
                throw new ArgumentException("Les trois statistiques PUI, DEF et VIT doivent être utilisées.");
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() + 1;
        }
    }
    /*public class RoleEnemie : Role
    {
        public RoleEnemie(string Nom, uint PUI, uint DEF, uint VIT) : base(Nom, PUI, DEF, VIT)
        {

        }
    }*/
}
