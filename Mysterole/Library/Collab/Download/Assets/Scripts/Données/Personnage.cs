using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Personnage
    {
        public string Nom;
        public uint NIV;
        private uint pv = 1;
        public List<Etat> Etats { get; private set; }
        public int PV
        {
            get
            {
                return (int)pv;
            }
            set
            {
                pv = (uint)Math.Max(Math.Min(MPV,value), 0);
            }
        }
        public int MPV
        {
            get
            {
                return Math.Max((int)Role.PrendreMPV(NIV) + TrouverBonusEquipement(Stats.MPV) + tempModMPV, 5);
            }
            set
            {
                tempModMPV = MPV - tempModMPV - value;
            }
        }
        private uint pc = 0;
        public int PC {
            get
            {
                return (int)pc;
            }
            set
            {
                pc = (uint)Math.Max(Math.Min(MPC, value), 0);
            }
        }
        public int MPC
        {
            get
            {
                return Math.Max((int)Role.PrendreMPC(NIV) + TrouverBonusEquipement(Stats.MPC),1); // TODO
            }
            private set
            {
                ;
            }
        }
        public int PUI
        {
            get
            {
                return Math.Max((int)Role.PrendrePUI(NIV) + (int)bPUI + TrouverBonusEquipement(Stats.PUI) + tempModPUI, 1);
            }
            set
            {
                tempModPUI = PUI - tempModPUI - value;
            }
        }
        public int DEF
        {
            get
            {
                return Math.Max((int)Role.PrendreDEF(NIV) + (int)bDEF + TrouverBonusEquipement(Stats.DEF) + tempModDEF, 1);
            }
            set
            {
                tempModDEF = DEF - tempModDEF - value;
            }
        }
        public int VIT
        {
            get
            {
                return Math.Max((int)Role.PrendreVIT(NIV)+(int)bVIT+TrouverBonusEquipement(Stats.VIT) + tempModVIT, 1);
            }
            set
            {
                tempModVIT = VIT - tempModVIT - value;
            }
        }
        public int MOV
        {
            get
            {
                return Math.Max((int)Role.PrendreMOV(NIV) + TrouverBonusEquipement(Stats.MOV) + tempModMOV, 0);
            }
            set
            {
                tempModMOV = MOV - tempModMOV - value;
            }
        }
        public Competence AttaqueBase
        {
            get
            {
                if (Anneau != null && Anneau.AttaqueBase != null)
                    return Anneau.AttaqueBase;
                return Role.Base;
            }
        }
        public Competence AttaqueSpeciale
        {
            get
            {
                if (Anneau != null && Anneau.AttaqueSpeciale != null)
                    return Anneau.AttaqueSpeciale;
                return Role.Speciale;
            }
        }
        public uint bPUI { get; set; }
        public uint bDEF { get; set; }
        public uint bVIT { get; set; }
        private int tempModPUI { get; set; }
        private int tempModDEF { get; set; }
        private int tempModVIT { get; set; }
        private int tempModMPV { get; set; }
        private int tempModMOV { get; set; }
        public Role Role { get; private set; }
        public Objets.Equipement Casque { get; private set; }
        public Objets.Equipement Armure { get; private set; }
        public Objets.Equipement Arme { get; private set; }
        public Objets.Equipement Jambes { get; private set; }
        public Objets.EquipAnneau Anneau { get; private set; }
        public Personnage(string Nom, Role Role, uint Niveau)
        {
            bPUI = 0;
            bDEF = 0;
            bVIT = 0;
            if (Nom == "")
                throw new ArgumentException("Le nom ne peut être vide");
            this.Nom = Nom;
            this.Role = Role;
            NIV = Math.Max(Niveau, 1);
            FinCombat();
            Guerir();
            Etats = new List<Etat>();
        }
        public Personnage(Personnage perso)
        {
            bPUI = 0;
            bDEF = 0;
            bVIT = 0;
            Nom = perso.Nom;
            Role = perso.Role;
            NIV = perso.NIV;
            FinCombat();
            Guerir();
            Etats = perso.Etats;
        }

        public void Guerir()
        {
            PV = MPV;
            PC = MPC;
        }

        public int PrendreStat(Stats stat)
        {
            switch (stat)
            {
                case Stats.DEF:
                    return DEF;
                case Stats.MOV:
                    return MOV;
                case Stats.MPC:
                    return MPC;
                case Stats.MPV:
                    return MPV;
                case Stats.PC:
                    return PC;
                case Stats.PUI:
                    return PUI;
                case Stats.PV:
                    return PV;
                case Stats.VIT:
                    return VIT;
                default:
                    return 0;
            }
        }
        public void ChangerStat(Stats stat, int nouvVal)
        {
            switch (stat)
            {
                case Stats.DEF:
                    DEF += nouvVal;
                    break;
                case Stats.MOV:
                    MOV += nouvVal;
                    break;
                case Stats.MPC:
                    MPC += nouvVal;
                    break;
                case Stats.MPV:
                    MPV += nouvVal;
                    break;
                case Stats.PC:
                    PC += nouvVal;
                    break;
                case Stats.PUI:
                    PUI += nouvVal;
                    break;
                case Stats.PV:
                    PV += nouvVal;
                    break;
                case Stats.VIT:
                    VIT += nouvVal;
                    break;
            }
        }

        public void EffectuerAttaqueBase(List<Personnage> cibles)
        {
            AttaqueBase.Effectuer(PUI, cibles);
        }

        public void EffectuerAttaqueSpeciale(List<Personnage> cibles)
        {
            AttaqueSpeciale.Effectuer(PUI, cibles);
        }

        public List<Dictionary<Stats, int>> VerifierAttaqueBase(List<Personnage> cibles)
        {
            return AttaqueBase.Verifier(PUI, cibles);
        }

        public List<Dictionary<Stats, int>> VerifierAttaqueSpeciale(List<Personnage> cibles)
        {
            return AttaqueSpeciale.Verifier(PUI, cibles);
        }
        private int TrouverBonusEquipement(Stats Stat)
        {
            int retrun = 0;
            if (Casque != null)
                Casque.Effets.ToList().ForEach((EffetStat effet) =>
                {
                    if (effet.Affecte == Stat)
                        retrun += (int)effet.Montant * (effet.Negatif ? -1 : 1);
                });
            if (Armure != null)
                Armure.Effets.ToList().ForEach((EffetStat effet) =>
                {
                    if (effet.Affecte == Stat)
                        retrun += (int)effet.Montant * (effet.Negatif ? -1 : 1);
                });
            if (Arme != null)
                Arme.Effets.ToList().ForEach((EffetStat effet) =>
                {
                    if (effet.Affecte == Stat)
                        retrun += (int)effet.Montant * (effet.Negatif ? -1 : 1);
                });
            if (Jambes != null)
                Jambes.Effets.ToList().ForEach((EffetStat effet) =>
                {
                    if (effet.Affecte == Stat)
                        retrun += (int)effet.Montant * (effet.Negatif ? -1 : 1);
                });
            if (Anneau != null)
                Anneau.Effets.ToList().ForEach((EffetStat effet) =>
                {
                    if (effet.Affecte == Stat)
                        retrun += (int)effet.Montant * (effet.Negatif ? -1 : 1);
                });
            return retrun;
        }
        public void FinCombat()
        {
            tempModPUI = 0;
            tempModDEF = 0;
            tempModVIT = 0;
            tempModMOV = 0;
            tempModMPV = 0;
            Etats = new List<Etat>();
        }
        public void DebutTour()
        {
            List<Etat> toRemove = new List<Etat>();
            foreach(Etat etat in Etats)
            {
                if (!etat.Effectuer(this))
                {
                    toRemove.Add(etat);
                }
            }
            foreach (Etat etat in toRemove)
            {
                Etats.Remove(etat);
            }
        }
        /// <summary>
        /// Fonction à appeler lorsque que l'acte d'équiper un objet est CONFIRMÉ.
        /// </summary>
        /// <param name="nouveau">La pièce d'équipement à donner.</param>
        /// <param name="vieux">Va placer dans cette variable la pièce d'équipement à renvoyer dans l'inventaire.</param>
        /// <returns>Indique si l'objet fut équipé ou non.</returns>
        public bool Equiper(Objets.Equipement nouveau, out Objets.Equipement vieux)
        {
            if (nouveau.Restreint != null && nouveau.Restreint != Role)
            {
                vieux = nouveau;
                return false;
            }
            switch (nouveau.Position)
            {
                case EquipPos.Tete:
                    vieux = Casque;
                    Casque = nouveau;
                    return true;
                case EquipPos.Armure:
                    vieux = Armure;
                    Armure = nouveau;
                    return true;
                case EquipPos.Jambes:
                    vieux = Jambes;
                    Jambes = nouveau;
                    return true;
                case EquipPos.Anneau:
                    if (nouveau is Objets.EquipAnneau) { 
                        vieux = Anneau;
                        Anneau = (Objets.EquipAnneau)nouveau;
                        return true;
                    } else {
                        throw new ArgumentException("L'objet ID" + nouveau.ID + " est considéré un anneau, mais n'en est pas un.");
                    }
                case EquipPos.Arme:
                    vieux = Arme;
                    Arme = nouveau;
                    return true;
            }
            vieux = nouveau;
            return false;
        }
        /// <summary>
        /// Fonction à appeler pour connaître les DIFFÉRENCES avec l'équipement actuel.
        /// </summary>
        /// <param name="test">L'équipement à comparer</param>
        /// <param name="differences">Liste associative des statistiques qui vont changer.</param>
        /// <returns>Indique si l'objet peut être équipé.</returns>
        public bool Equipable(Objets.Equipement test, out Dictionary<Stats, int> differences)
        {
            Dictionary<Stats, int> diffs = new Dictionary<Stats, int>();

            if (test.Restreint != null && test.Restreint != Role)
            {
                differences = new Dictionary<Stats, int>();
                return false;
            }

            List<EffetStat> vieux = new List<EffetStat>();
            switch(test.Position)
            {
                case EquipPos.Tete:
                    if (Casque == null)
                        break;
                    vieux = Casque.Effets.ToList();
                    break;
                case EquipPos.Armure:
                    vieux = Armure.Effets.ToList();
                    break;
                case EquipPos.Jambes:
                    vieux = Jambes.Effets.ToList();
                    break;
                case EquipPos.Arme:
                    vieux = Arme.Effets.ToList();
                    break;
                case EquipPos.Anneau:
                    vieux = Anneau.Effets.ToList();
                    break;
            }
            List<EffetStat> nouveau = test.Effets.ToList();
            
            diffs.Add(Stats.PUI, 0);
            diffs.Add(Stats.DEF, 0);
            diffs.Add(Stats.VIT, 0);
            diffs.Add(Stats.MPV, 0);
            diffs.Add(Stats.MOV, 0);

            nouveau.ForEach((effet) =>
            {
                diffs[effet.Affecte] += (int)effet.Montant * (effet.Negatif ? -1 : 1);
            });
            vieux.ForEach((effet) =>
            {
                diffs[effet.Affecte] -= (int)effet.Montant * (effet.Negatif ? -1 : 1);
            });

            differences = diffs;
            return true;
        }
    }
    public class Joueur : Personnage
    {
        public double EXP { get; private set; }
        public bool NiveauSuivant { get { return EXP >= 100; } }
        public Joueur(string Nom, RoleJoueur Role, uint Niveau) : base(Nom, Role, Niveau)
        {
            
        }
        public void AjoutExp(double Montant)
        {
            EXP = EXP + Montant;
            // TODO Gérer la progression au niveau suivant ?
        }
        public void EffectuerNiveauSuivant()
        {
            if (NiveauSuivant)
            {
                EXP -= 100;
                NIV += 1;
            }
            else
            {
                Erreurs.NouvelleErreur("Erreur : Le système tente une progression de niveau sans le montant d'expérience requis pour " + Nom + ".");
            }
        }
    }
    /*public class Enemie : Personnage
    {
        public Enemie(string Nom, Stats Stats, Role Role, int Niveau) : base(Nom, Role, Niveau)
        {
            // TODO
        }
    }*/
}
