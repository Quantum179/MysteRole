// Programme : Menu de débogage : Données des Équipes de Monstres
// Auteur : Jean-Michel Beauvais
// Gère l'affichage et l'actualisation des données sur les équipe de monstres existants dans le système.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Mysterole;
using System;

public class DebogueEquipesMonstre : MonoBehaviour {

    public Dropdown listeEquipesMonstres;
    public GameObject Monstre1;
    public GameObject Monstre2;
    public GameObject Monstre3;
    public GameObject Monstre4;
    // Use this for initialization

    public Dropdown lstMonstre1 { get { return Monstre1.GetComponentInChildren<Dropdown>(); } }
    public Dropdown lstMonstre2 { get { return Monstre2.GetComponentInChildren<Dropdown>(); } }
    public Dropdown lstMonstre3 { get { return Monstre3.GetComponentInChildren<Dropdown>(); } }
    public Dropdown lstMonstre4 { get { return Monstre4.GetComponentInChildren<Dropdown>(); } }
    void Start () {
        Dictionary<int, Equipe>.Enumerator e = DonneesJeu.EquipesMonstre.GetEnumerator();
        listeEquipesMonstres.ClearOptions();
        Dropdown.OptionData od = new Dropdown.OptionData(" - Nouvelle - ");
        listeEquipesMonstres.options.Add(od);
        while (e.MoveNext())
        {
            od = new Dropdown.OptionData(e.Current.Key.ToString() + " - " + e.Current.Value.Nom);
            listeEquipesMonstres.options.Add(od);
        }
        Dictionary<int, Role>.Enumerator em = DonneesJeu.Monstres.GetEnumerator();
        lstMonstre1.ClearOptions();
        lstMonstre2.ClearOptions();
        lstMonstre3.ClearOptions();
        lstMonstre4.ClearOptions();
        od = new Dropdown.OptionData("(Aucun)");
        lstMonstre2.options.Add(od);
        lstMonstre3.options.Add(od);
        lstMonstre4.options.Add(od);
        while (em.MoveNext())
        {
            od = new Dropdown.OptionData(em.Current.Key.ToString() + " - " + em.Current.Value.Nom);
            lstMonstre1.options.Add(od);
            lstMonstre2.options.Add(od);
            lstMonstre3.options.Add(od);
            lstMonstre4.options.Add(od);
        }
        listeEquipesMonstres.value = 1;
        listeEquipesMonstres.RefreshShownValue();
	}

    public void RafraichirEquipe()
    {
        if (listeEquipesMonstres.value == 0)
            return;
        Equipe monstres = DonneesJeu.EquipesMonstre[int.Parse(listeEquipesMonstres.captionText.text.Split(' ')[0])];
        Role r = monstres.Membres[0].Role;
        Dictionary<int, Role>.Enumerator e = DonneesJeu.Monstres.GetEnumerator();
        int i = 0;
        while(e.MoveNext())
        {
            if (r.ID == e.Current.Value.ID)
            {
                lstMonstre1.value = i;
            }
            i++;
        }
        Monstre1.GetComponentInChildren<InputField>().text = monstres.Membres[0].NIV.ToString();
        lstMonstre1.RefreshShownValue();
        if (monstres.Membres.Count >= 2)
        {
            r = monstres.Membres[1].Role;
            e = DonneesJeu.Monstres.GetEnumerator();
            i = 0;
            while (e.MoveNext())
            {
                if (r.ID == e.Current.Value.ID)
                {
                    lstMonstre2.value = i+1;
                }
                i++;
            }
            Monstre2.GetComponentInChildren<InputField>().text = monstres.Membres[1].NIV.ToString();
        }
        else
        {
            lstMonstre2.value = 0;
            Monstre2.GetComponentInChildren<InputField>().text = "";
        }
        lstMonstre2.RefreshShownValue();
        if (monstres.Membres.Count >= 3)
        {
            r = monstres.Membres[2].Role;
            e = DonneesJeu.Monstres.GetEnumerator();
            i = 0;
            while (e.MoveNext())
            {
                if (r.ID == e.Current.Value.ID)
                {
                    lstMonstre3.value = i+1;
                }
                i++;
            }
            Monstre3.GetComponentInChildren<InputField>().text = monstres.Membres[2].NIV.ToString();
        }
        else
        {
            lstMonstre3.value = 0;
            Monstre3.GetComponentInChildren<InputField>().text = "";
        }
        lstMonstre3.RefreshShownValue();
        if (monstres.Membres.Count >= 4)
        {
            r = monstres.Membres[3].Role;
            e = DonneesJeu.Monstres.GetEnumerator();
            i = 0;
            while (e.MoveNext())
            {
                if (r.ID == e.Current.Value.ID)
                {
                    lstMonstre4.value = i+1;
                }
                i++;
            }
            Monstre4.GetComponentInChildren<InputField>().text = monstres.Membres[3].NIV.ToString();
        }
        else
        {
            lstMonstre4.value = 0;
            Monstre4.GetComponentInChildren<InputField>().text = "";
        }
        lstMonstre4.RefreshShownValue();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
