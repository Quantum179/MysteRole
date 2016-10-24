using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Objets
    {
        private static Dictionary<int,Objet> lstObjets = new Dictionary<int, Objet>();
        static private bool Existe(int id)
        {
            try
            {
                return lstObjets.ContainsKey(id);
            }
            catch
            {
                return false;
            }
        }
        static public void CreerEquipement(int idObjet, string strNom, EffetStat[] sEffets, EquipPos Position)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "L'équipement existe déjà.");
            }

            lstObjets.Add(idObjet, new Equipement(idObjet, strNom, sEffets, Position));
        }
        static public void CreerAnneau(int idObjet, string strNom, EffetStat[] sEffets, Competence attaqueBase, Competence attaqueSpeciale, Role Restriction)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "L'anneau existe déjà.");
            }

            lstObjets.Add(idObjet, new EquipAnneau(idObjet, strNom, sEffets, attaqueBase, attaqueSpeciale, Restriction));
        }
        static public void CreerConsommable(int idObjet, string strNom, Competence Effet)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "Le consommable existe déjà.");
            }

            lstObjets.Add(idObjet, new Consommable(idObjet, strNom, Effet));
        }
        static public Objet TrouverObjet(int id)
        {
            if (!Existe(id))
            {
                throw new ManquantException(id, "L'objet n'existe pas.");
            }

            return lstObjets[id];
        }

        private Objets()
        {

        }

        // SOUS-CLASSES --------------------------

        public abstract class Objet
        {
            public int ID { get; private set; }
            public string Nom { get; private set; }
            public int Quantite = 0;
            protected Objet(int idObjet, string strNom)
            {
                ID = idObjet;
                Nom = strNom;
            }
        }

        public class Equipement : Objet
        {
            public EffetStat[] Effets { get; private set; }
            public EquipPos Position { get; private set; }
            private Role _restreint;
            public Role Restreint
            {
                get
                {
                    return _restreint;
                }
                set
                {
                    if (_restreint == null)
                    {
                        _restreint = value;
                    }
                }
            }
            public Equipement(int idObjet, string strNom, EffetStat[] sEffets, EquipPos Position) : base (idObjet, strNom)
            {
                Effets = sEffets;
                this.Position = Position;
            }
        }
        public class EquipAnneau : Equipement
        {
            public Competence AttaqueBase { get; private set; }
            public Competence AttaqueSpeciale { get; private set; }
            public EquipAnneau(int idObjet, string strNom, EffetStat[] sEffets, Competence attaqueBase, Competence attaqueSpeciale, Role Restriction) : base(idObjet, strNom, sEffets, EquipPos.Anneau)
            {
                AttaqueBase = attaqueBase;
                AttaqueSpeciale = attaqueSpeciale;
                Restreint = Restriction;
            }
        }
        public class Consommable : Objet
        {
            public Competence Effet { get; private set; }
            public Consommable(int idObjet, string strNom, Competence Effet) : base(idObjet, strNom)
            {
                this.Effet = Effet;
            }
        }
        public class ObjetCle : Objet
        {
            public ObjetCle(int idObjet, string strNom) : base(idObjet, strNom)
            {

            }
        }

        // EXCEPTIONS -------------------------

        public class CreationObjetException : Exception
        {
            public CreationObjetException() : base()
            {

            }
            public CreationObjetException(string message) : base(message)
            {
                
            }
        }
        public class DoublonException : CreationObjetException
        {
            public int id { get; private set; }
            public DoublonException(int id) : base()
            {
                this.id = id;
            }
            public DoublonException(int id, string message) : base(message)
            {
                this.id = id;
            }
        }
        public class AccesObjetException : Exception
        {
            public AccesObjetException() : base()
            {

            }
            public AccesObjetException(string message) : base(message)
            {

            }
        }
        public class ManquantException : AccesObjetException
        {
            public int id;
            public ManquantException(int id) : base()
            {
                this.id = id;
            }
            public ManquantException(int id, string message) : base(message + " (" + id.ToString() + ")")
            {
                this.id = id;
            }
        }
    }
}