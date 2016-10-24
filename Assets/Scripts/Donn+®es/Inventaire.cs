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
        private Dictionary<int, Objets.Objet> lstObjets;
        public Inventaire()
        {
            lstObjets = new Dictionary<int, Objets.Objet>();
            //nbrObjets = new Dictionary<int, int>();
        }
        public void Ajouter(int idObjet, int Montant)
        {
            Objets.Objet ajout;
            try
            {
                ajout = Objets.TrouverObjet(idObjet);
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
                    lstObjets[idObjet].Quantite += Montant;
                }
                else
                {
                    lstObjets.Add(idObjet, ajout);
                    lstObjets[idObjet].Quantite = Montant;
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
            if (lstObjets.ContainsValue(o))
            {
                int newnbr = lstObjets[idObjet].Quantite - Montant;
                if (newnbr <= 0)
                {
                    lstObjets.Remove(idObjet);
                }
                else
                {
                    lstObjets[idObjet].Quantite -= Montant;
                }
            }

            Erreurs.NouvelleErreur("Retrait de l'objet \"" + o.Nom + "\" x" + Montant.ToString() + " fois.");
        }
        public List<Objets.Objet> Liste
        {
            get
            {
                return lstObjets.Values.ToList();
            }
        }
    }
}
