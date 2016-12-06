// Programme : Menu de débogage : Données des compétences
// Auteur : Jean-Michel Beauvais
// Gère l'affichage et l'actualisation des données sur les compétences existantes dans le système.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mysterole;
using System.Collections.Generic;
using System;

public class DebugCompetences : MonoBehaviour {

    public Dropdown ListeCompetences;
    public InputField Nom;
    public Dropdown Etats;
    public Toggle AtkBasique;
    public Toggle Negatif;
    public Dropdown TypeZone;
    public InputField Rayon;
    public InputField Longueur;
    public InputField NbrCases;
    public Dropdown TypePortee;
    public InputField Min;
    public InputField Max;
    public Toggle Croix;
    public Toggle Diago;
    public Toggle CibleCase;
    public GameObject ListeEffets;

	// Use this for initialization
	void Awake () {
        ListeCompetences.ClearOptions();
        Dropdown.OptionData od = new Dropdown.OptionData("- Nouvelle -");
        ListeCompetences.options.Add(od);
        Dictionary<int, Competence>.Enumerator e = DonneesJeu.Competences.GetEnumerator();
        while (e.MoveNext())
        {
            od = new Dropdown.OptionData(e.Current.Key + " - " + e.Current.Value.Nom);
            ListeCompetences.options.Add(od);
        }
        ListeCompetences.value = 1;
        ListeCompetences.RefreshShownValue();
        Etats.ClearOptions();
        od = new Dropdown.OptionData("- Aucun -");
        Etats.options.Add(od);
        Dictionary<int, Etat>.Enumerator et = DonneesJeu.Etats.GetEnumerator();
        while (et.MoveNext())
        {
            od = new Dropdown.OptionData(et.Current.Key + " - " + et.Current.Value.Nom);
            Etats.options.Add(od);
        }
        Etats.value = 0;
        Etats.RefreshShownValue();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AfficherCompetence(Int32 index)
    {
        if (index == ListeCompetences.value)
        {
            if (index == 0) return;
        }
        else
        {
            index = ListeCompetences.value;
        }
        ListeEffets.GetComponent<DebugEffets>().Vider();
        int id = int.Parse(ListeCompetences.options[index].text.Split(' ')[0]);
        Competence comp = DonneesJeu.Competences[id];
        Nom.text = comp.Nom;
        AtkBasique.isOn = comp.AttaqueBasique;
        Negatif.isOn = comp.Negatif;
        Etats.value = 0;
        if (comp.GetType().Equals(typeof(CompEtat)))
        {
            for(int i = 1; i < Etats.options.Count; i++)
            {
                string text = (comp as CompEtat).Etat.ID + " - " + (comp as CompEtat).Etat.Nom;
                if (Etats.options[i].text == text)
                {
                    Etats.value = i;
                    break;
                }
            }
        }
        Etats.RefreshShownValue();

        Portee p = comp.Cible;
        if (p.GetType().Equals(typeof(PorteeCercle)))
        {
            TypePortee.value = 2;
        }
        else if (p.GetType().Equals(typeof(PorteePleine)))
        {
            TypePortee.value = 1;
        }
        else
        {
            TypePortee.value = 0;
        }
        Min.text = p.DistanceMin.ToString();
        Max.text = p.DistanceMax.ToString();
        Diago.isOn = p.EnDiag;
        Croix.isOn = p.EnLigne;
        CibleCase.isOn = !p.CibleUnite;
        TypePortee.RefreshShownValue();

        Zone z = p.Zone;
        Rayon.text = z.Largeur.ToString();
        Longueur.text = z.Longueur.ToString();
        NbrCases.text = z.nbrCases.ToString();
        if (z.GetType().Equals(typeof(ZonePoint)))
        {
            TypeZone.value = 0;
            Rayon.text = " ";
            Longueur.text = " ";
        }
        else if (z.GetType().Equals(typeof(ZoneLosange)))
        {
            TypeZone.value = 1;
            Longueur.text = " ";
        }
        else
        {
            TypeZone.value = 3;
        }
        TypeZone.RefreshShownValue();

        foreach(Effet effet in comp.Effets)
        {
            ListeEffets.GetComponent<DebugEffets>().AjoutEffet(effet);
        }
    }
}
