using UnityEngine;
using System.Collections;
using Mysterole;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DebugJoueur : MonoBehaviour {

    public int ID;
    private Joueur joueur = null;
    //private bool JoueurDonne = false;
    private Color couleurModif = Color.yellow;
    private Color couleurOk = Color.green;

    public InputField Nom;
    public InputField Niveau;
    public Dropdown listRoles;
    public Dropdown listActions;
    public Button btnCreer;
    public Button btnExecuter;

    // Use this for initialization
    void Awake () {
        if (ID <= 0)
            ID = int.Parse(name[name.Length-1].ToString());
        Initier();
        /*if (DonneesJeu.Equipe.Membres.Count >= ID)
        {
            joueur = DonneesJeu.Equipe.Membres[ID - 1] as Joueur;
            AppliquerJoueur();
        }*/
        //Debug.Log("ID" + ID.ToString() + " éveillé. " + (DonneesJeu.Equipe.Membres.Count+1 >= ID).ToString());
    }

    public void ModifierFait()
    {
        gameObject.GetComponent<Image>().color = couleurOk;
    }

    public void ModifierAFaire()
    {
        gameObject.GetComponent<Image>().color = couleurModif;
    }

    private void AppliquerJoueur()
    {
        //Debug.Log(ID.ToString() + " - Application Joueur (" + joueur.Nom + ")");
        Nom.text = joueur.Nom;
        Nom.interactable = false;
        Niveau.text = joueur.NIV.ToString();
        btnExecuter.gameObject.SetActive(true);
        btnCreer.gameObject.SetActive(false);
        Dictionary<int, RoleJoueur>.Enumerator e = DonneesJeu.Roles.GetEnumerator();
        int i = 0;
        while(e.MoveNext())
        {
            if (e.Current.Key == joueur.Role.ID)
                listRoles.value = i+1;
            i++;
        }
        listRoles.RefreshShownValue();
        listRoles.interactable = false;
        listActions.value = 0;
        listActions.RefreshShownValue();
        listActions.gameObject.SetActive(true);
        ModifierFait();
    }

    private void Initier()
    {
        Nom.text = "";
        Nom.interactable = true;
        Niveau.text = "";
        btnExecuter.gameObject.SetActive(false);
        btnCreer.gameObject.SetActive(true);
        Dropdown.OptionData od = new Dropdown.OptionData("- Role...");
        listRoles.options.Clear();
        listRoles.options.Add(od);
        Dictionary<int, RoleJoueur>.Enumerator e = DonneesJeu.Roles.GetEnumerator();
        while(e.MoveNext())
        {
            od = new Dropdown.OptionData(FaireChaineRole(e.Current.Value));
            listRoles.options.Add(od);
        }
        listRoles.value = 0;
        listRoles.RefreshShownValue();
        listRoles.interactable = true;
        listActions.value = 0;
        listActions.RefreshShownValue();
        listActions.gameObject.SetActive(false);
        foreach (Text t in gameObject.GetComponentsInChildren<Text>())
        {
            if (t.gameObject.name == "ID")
                t.text = ID.ToString();
        }
        ModifierFait();
    }

    string FaireChaineRole(RoleJoueur r)
    {
        return r.ID + " - " + r.Nom;
    }

    void OnEnable()
    {
        
	}

    void OnDisable()
    {
        
    }

    public void VerifierEquipe()
    {
        if (DonneesJeu.Equipe.Membres.Count+1 >= ID)
        {
            //Debug.Log(ID.ToString() + " actif VS " + DonneesJeu.Equipe.Membres.Count.ToString());
            gameObject.SetActive(true);

            if (joueur == null && ID <= DonneesJeu.Equipe.Membres.Count)
            {
                joueur = DonneesJeu.Equipe.Membres[ID - 1] as Joueur;
                AppliquerJoueur();
            }
            else if (ID <= DonneesJeu.Equipe.Membres.Count)
            {
                if (DonneesJeu.Equipe.Membres[ID - 1].Nom != joueur.Nom)
                {
                    //Debug.Log("Wrong Character...");
                    joueur = DonneesJeu.Equipe.Membres[ID - 1] as Joueur;
                    AppliquerJoueur();
                }
            }
            else if (ID > DonneesJeu.Equipe.Membres.Count && joueur != null)
            {
                joueur = null;
                Vider();
            }
        }
        else
        {
            if (gameObject.activeSelf)
                Vider();
            joueur = null;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        VerifierEquipe();
    }

    public void Retirer() // Retirer le joueur
    {
        DonneesJeu.Equipe.RetirerMembre(joueur);
        joueur = null;
        Vider();
        //Debug.Log("Joueur retiré. Restant : " + DonneesJeu.Equipe.Membres.Count.ToString());
    }

    private void Vider() {
        Nom.text = "";
        Nom.interactable = true;
        Niveau.text = "";
        btnExecuter.gameObject.SetActive(false);
        btnCreer.gameObject.SetActive(true);
        listRoles.value = 0;
        listRoles.RefreshShownValue();
        listRoles.interactable = true;
        listActions.value = 0;
        listActions.RefreshShownValue();
        listActions.gameObject.SetActive(false);
        ModifierFait();
    }

    public void Creer()
    {
        joueur = null;
        string nom = "";
        RoleJoueur r = null;
        int niveau = 0;


        string role = listRoles.options[listRoles.value].text;
        Dictionary<int, RoleJoueur>.Enumerator e = DonneesJeu.Roles.GetEnumerator();
        while(e.MoveNext())
        {
            if (FaireChaineRole(e.Current.Value) == role)
            {
                r = e.Current.Value;
                break;
            }
        }
        nom = Nom.text;
        niveau = int.Parse(Niveau.text);

        if (r != null && nom != "" && niveau > 0)
        {
            listRoles.interactable = false;
            listActions.gameObject.SetActive(true);
            Nom.interactable = false;
            btnExecuter.gameObject.SetActive(true);
            btnCreer.gameObject.SetActive(false);

            joueur = new Joueur(nom, r, (uint)niveau);
            DonneesJeu.Equipe.AjoutMembre(joueur);
            if (ID < 4)
                gameObject.transform.parent.GetChild(ID).GetComponent<DebugJoueur>().VerifierEquipe();
            //Debug.Log("Ajout personnage " + joueur.Nom + " (" + DonneesJeu.Equipe.Membres.Count.ToString() + ").");
            ModifierFait();
        }
    }

    public void Guerir()
    {
        if (joueur != null)
        {
            int index = DonneesJeu.Equipe.Membres.IndexOf(joueur);
            DonneesJeu.Equipe.Membres[index].Guerir();
        }
    }

    public void Executer()
    {
        switch (listActions.value)
        {
            case 0: // Guérir
                Guerir();
                break;
            case 1: // Changer Niveau
                ChangerNiveau();
                break;
            case 2: // Retirer personnage
                Retirer();
                break;
        }
    }

    private void ChangerNiveau()
    {
        int index = DonneesJeu.Equipe.Membres.IndexOf(joueur);
        joueur.NIV = (uint)int.Parse(Niveau.text);
        ModifierFait();
    }
}
