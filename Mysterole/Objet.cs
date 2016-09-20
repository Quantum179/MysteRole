using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Objet
    {
        static List<int> IDs = new List<int>();
        public int ID { get; private set; }
        public string Nom { get; private set; }
        protected Objet(int idObjet, string strNom)
        {
            // TODO
        }
    }

    public class Equipement : Objet
    {
        public EffetStat[] Effets { get; private set; }
        public Equipement(int idObjet, string strNom, EffetStat[] sEffets) : base (idObjet, strNom)
        {
            // TODO
        }
    }
    /*public class EquipTete : Equipement
    {
        public EquipTete(int idObjet, string strNom, EffetStatPermanent[] sEffets) : base(idObjet, strNom, sEffets)
        {
            // TODO
        }
    }
    public class EquipArme : Equipement
    {
        public EquipArme(int idObjet, string strNom, EffetStatPermanent[] sEffets) : base(idObjet, strNom, sEffets)
        {
            // TODO
        }
    }
    public class EquipArmure : Equipement
    {
        public EquipArmure(int idObjet, string strNom, EffetStatPermanent[] sEffets) : base(idObjet, strNom, sEffets)
        {
            // TODO
        }
    }
    public class EquipJambes : Equipement
    {
        public EquipJambes(int idObjet, string strNom, EffetStatPermanent[] sEffets) : base(idObjet, strNom, sEffets)
        {
            // TODO
        }
    }*/
    public class EquipAnneau : Equipement
    {
        public Competence AttaqueBase { get; private set; }
        public Competence AttaqueSpeciale { get; private set; }
        public EquipAnneau(int idObjet, string strNom, EffetStat[] sEffets) : base(idObjet, strNom, sEffets)
        {
            // TODO
        }
    }
    public class Consommable : Objet
    {
        public Competence Effet { get; private set; }
        public Consommable(int idObjet, string strNom, Competence Effet) : base(idObjet, strNom)
        {
            // TODO
        }
    }
}