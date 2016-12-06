// Programme : Objets
// Auteur : Jean-Michel Beauvais
// Classes et fonctions pour les objets utilisables par le joueur.

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
        static public Equipement CreerEquipement(int idObjet, string strNom, EffetStat[] sEffets, EquipPos Position)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "L'équipement existe déjà.");
            }
            Equipement e = new Equipement(idObjet, strNom, sEffets, Position);
            lstObjets.Add(idObjet, e);
            return e;
        }
        static public EquipAnneau CreerAnneau(int idObjet, string strNom, EffetStat[] sEffets, Competence attaqueBase, Competence attaqueSpeciale, Role Restriction)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "L'anneau existe déjà.");
            }
            EquipAnneau ea = new EquipAnneau(idObjet, strNom, sEffets, attaqueBase, attaqueSpeciale, Restriction);
            lstObjets.Add(idObjet, ea);
            return ea;
        }
        static public Consommable CreerConsommable(int idObjet, string strNom, int PUI, Competence Effet)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "Le consommable existe déjà.");
            }
            Consommable c = new Consommable(idObjet, strNom, PUI, Effet);
            lstObjets.Add(idObjet, c);
            return c;
        }
        // TODO DELETE FOLLOWING FUNCTION WHEN ALL CODE UPDATED
        static public Consommable CreerConsommable(int idObjet, string strNom, Competence Effet)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "Le consommable existe déjà.");
            }

            Erreurs.NouvelleErreur("La fonction 'CreerConsommable' requière maintenant 4 paramètres pour inclure la puissance. Cette valeur fut placée à 5 par défaut. Veuillez mettre à jour votre code.");
            Consommable c = new Consommable(idObjet, strNom, 5, Effet);
            lstObjets.Add(idObjet, c);
            return c;
        }
        static public ObjetCle CreerObjetCle(int idObjet, string strNom)
        {
            if (Existe(idObjet))
            {
                throw new DoublonException(idObjet, "Le consommable existe déjà.");
            }
            ObjetCle oc = new ObjetCle(idObjet, strNom);
            lstObjets.Add(idObjet, oc);
            return oc;
        }
        static public Objet TrouverObjet(int id)
        {
            if (!Existe(id))
            {
                throw new ManquantException(id, "L'objet n'existe pas.");
            }

            return lstObjets[id];
        }

        internal static void Charger()
        {
            /*Dictionary<int, Objet> objets =*/ AccesBD.TrouverObjets();/*
            Dictionary<int, Objet>.Enumerator e = objets.GetEnumerator();
            while (e.MoveNext())
            {
                Type tObjet = e.Current.Value.GetType();
                if (tObjet.Equals(typeof(Equipement)))
                {
                    if (((Equipement)e.Current.Value).Position == EquipPos.Anneau)
                    {
                        EquipAnneau a = e.Current.Value as EquipAnneau;
                        CreerAnneau(a.ID, a.Nom, a.Effets, a.AttaqueBase, a.AttaqueSpeciale, a.Restreint);
                    }
                    else
                    {
                        Equipement eq = e.Current.Value as Equipement;
                        CreerEquipement(eq.ID, eq.Nom, eq.Effets, eq.Position);
                    }
                }
                else if (tObjet.Equals(typeof(Consommable)))
                {
                    Consommable c = e.Current.Value as Consommable;
                    CreerConsommable(c.ID, c.Nom, c.PUI, c.Effet);
                }
                else
                {
                    ObjetCle oc = e.Current.Value as ObjetCle;
                    CreerObjetCle(oc.ID, oc.Nom);
                }
            }*/
        }

        public static Dictionary<int, Objet> Liste { get { return lstObjets; } }

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
            public int PUI { get; private set; }
            public Consommable(int idObjet, string strNom, int PUI, Competence Effet) : base(idObjet, strNom)
            {
                this.Effet = Effet;
                this.PUI = PUI;
            }
            
            public Portee Portee { get { return Effet.Cible; } }

            public void Utiliser(List<Personnage> cibles)
            {
                Effet.Effectuer(PUI, cibles);
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