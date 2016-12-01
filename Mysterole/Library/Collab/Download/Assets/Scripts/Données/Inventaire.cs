using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
//using System.Threading.Tasks;

namespace Mysterole
{
    public class Inventaire
    {
        private Dictionary<int, int> lstObjets;
        public Inventaire()
        {
            lstObjets = new Dictionary<int, int>();
            //nbrObjets = new Dictionary<int, int>();
        }
        public void Ajouter(int idObjet, int Montant)
        {
            if (Montant <= 0)
            {
                Debug.LogWarning("Ajout d'un objet x0.");
                return;
            }
            Objets.Objet ajout;
            try
            {
                ajout = Objets.TrouverObjet(idObjet);
            }
            catch (Objets.ManquantException e)
            {
                Erreurs.NouvelleErreur(e.Message + " (ID " + e.id + ")");
                return;
            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Acquisition de l'objet a échoué. (" + e.GetType().ToString() + ") : " + e.Message);
                return;
            }
            try
            { 
                if (lstObjets.ContainsKey(idObjet))
                {
                    lstObjets[idObjet] += Montant;
                }
                else
                {
                    lstObjets.Add(idObjet, Montant);
                }
            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Erreur en ajoutant un objet dans l'inventaire : (" + e.GetType().ToString() + ") : " + e.Message);
                return;
            }
        }
        public void Retirer(int idObjet, int Montant)
        {
            if (Montant <= 0)
            {
                Debug.LogWarning("Retrait d'un objet x0.");
                return;
            }
            Objets.Objet o;
            try
            {
                o = Objets.TrouverObjet(idObjet);
            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Erreur en retirant un objet de l'inventaire, l'acquisition de l'objet a échoué : (" + e.GetType().ToString() + ") : " + e.Message);
                return;
            }
            if (lstObjets.ContainsKey(o.ID))
            {
                int newnbr = lstObjets[idObjet] - Montant;
                if (newnbr <= 0)
                {
                    lstObjets.Remove(idObjet);
                }
                else
                {
                    lstObjets[idObjet] -= Montant;
                }
            }

            //Erreurs.NouvelleErreur("Retrait de l'objet \"" + o.Nom + "\" x" + Montant.ToString() + " fois.");
        }
        public Dictionary<int, int> Liste
        {
            get
            {
                return lstObjets;
            }
        }
    }
}
