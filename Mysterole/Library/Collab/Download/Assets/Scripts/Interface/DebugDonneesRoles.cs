using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mysterole;
using System;
using System.Collections.Generic;

public class DebugDonneesRoles : MonoBehaviour {
    public bool RoleJoueurs;
    public Dropdown Roles;
    public InputField Nom;
    public InputField Niveau;
    public InputField PUI;
    public InputField DEF;
    public InputField VIT;
    public InputField MOV;
    public InputField MPV;
    public InputField MPC;
    public Dropdown AtqBase;
    public Dropdown AtqSpeciale;

    private Color defaut;
    private Color modificationEnCours = Color.green;
    private Color modifie = Color.yellow;

    // Use this for initialization
    void Awake () {
        defaut = gameObject.GetComponent<Image>().color;
        Roles.ClearOptions();
        Dropdown.OptionData od;
        od = new Dropdown.OptionData(" - Nouveau - ");
        Roles.options.Add(od);
        if (RoleJoueurs)
        {
            Dictionary<int, RoleJoueur>.Enumerator er = DonneesJeu.Roles.GetEnumerator();
            while (er.MoveNext())
            {
                od = new Dropdown.OptionData(er.Current.Key.ToString() + " - " + er.Current.Value.Nom);
                Roles.options.Add(od);
            }
        }
        else
        {
            Dictionary<int, Role>.Enumerator em = DonneesJeu.Monstres.GetEnumerator();
            while(em.MoveNext())
            {
                od = new Dropdown.OptionData(em.Current.Key.ToString() + " - " + em.Current.Value.Nom);
                Roles.options.Add(od);
            }
        }
        Roles.value = 1;
        Roles.RefreshShownValue();

        AtqBase.ClearOptions();
        Dictionary<int, Competence>.Enumerator e = DonneesJeu.Competences.GetEnumerator();
        while (e.MoveNext())
        {
            if (e.Current.Value.AttaqueBasique)
            {
                od = new Dropdown.OptionData(e.Current.Key.ToString() + " - " + e.Current.Value.Nom);
                AtqBase.options.Add(od);
            }
        }

        AtqSpeciale.ClearOptions();
        e = DonneesJeu.Competences.GetEnumerator();
        while (e.MoveNext())
        {
            if (!e.Current.Value.AttaqueBasique)
            {
                od = new Dropdown.OptionData(e.Current.Key.ToString() + " - " + e.Current.Value.Nom);
                AtqSpeciale.options.Add(od);
            }
        }

        AfficherRole();
	}

    public void AfficherRole()
    {
        if (Roles.value == 0)
        {
            Roles.value = 1;
            Roles.RefreshShownValue();
        }
        uint niveau;
        try
        {
            niveau = (uint)Math.Max(1, int.Parse(Niveau.text));
        }
        catch
        {
            niveau = 1;
        }
        Role r = null;
        if (RoleJoueurs)
        {
            r = DonneesJeu.Roles[int.Parse(Roles.captionText.text.Split(' ')[0])];
        }
        else
        {
            r = DonneesJeu.Monstres[int.Parse(Roles.captionText.text.Split(' ')[0])];
        }
        Nom.text = r.Nom;
        PUI.text = r.PrendrePUI(niveau).ToString();
        DEF.text = r.PrendreDEF(niveau).ToString();
        VIT.text = r.PrendreVIT(niveau).ToString();
        MOV.text = r.PrendreMOV(niveau).ToString();
        MPV.text = r.PrendreMPV(niveau).ToString();
        MPC.text = r.PrendreMPC(niveau).ToString();

        Dictionary<int, Competence>.Enumerator e = DonneesJeu.Competences.GetEnumerator();
        int i = 0;
        int j = 0;
        while(e.MoveNext())
        {
            if (e.Current.Value.AttaqueBasique)
            {
                if (e.Current.Value == r.Base)
                    AtqBase.value = i;
                i++;
            }
            else
            {
                if (e.Current.Value == r.Speciale)
                    AtqSpeciale.value = j;
                j++;
            }
        }
        AtqBase.RefreshShownValue();
        AtqSpeciale.RefreshShownValue();
    }

    // Update is called once per frame
    void Update () {

	}
}
