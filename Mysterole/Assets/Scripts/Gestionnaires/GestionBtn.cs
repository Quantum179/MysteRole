using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mysterole;
using System.Collections.Generic;
using UnityEngine.UI;

public class GestionBtn : MonoBehaviour
{
    public Selectable MenuP;
    public Selectable MenuCr;
    public Selectable MenuO;
    public Selectable MenuCh;
    public Toggle Son;
    public Toggle debogage;
    public Toggle PleinEcran;
    public Dropdown Res;


    private string NavPos;
    private string Nom;


    public GameObject selectables;
    public GameObject Creation_Personnage;
    public GameObject Charger_Partie;
    public GameObject Option;

    public void Start()
    {
        // Mettre la page du menu princiaple active
        selectables.SetActive(true);
        Creation_Personnage.SetActive(false);
        Charger_Partie.SetActive(false);
        Option.SetActive(false);

        // Sélection du premier élément par défaut dans le menu principale
        MenuP.Select();

        // on set la résolution par default
        Screen.SetResolution(int.Parse(DonneesJeu.Options.Resolution.Split('x')[0]), int.Parse(DonneesJeu.Options.Resolution.Split('x')[1]), DonneesJeu.Options.PleinEcran);

    }

    public void Newpartie_Click(GameObject perso)
    {
        selectables.SetActive(false);
        perso.SetActive(true);

        // Sélection du premier élément dans le menu création personnage
        MenuCr.Select();
    }

    public void ChargerPartie(GameObject Charger_Partie)
    {
        selectables.SetActive(false);
        Charger_Partie.SetActive(true);

        // Sélection du premier élément dans le menu création personnage
        MenuCh.Select();
    }

    public void Retour_Click(GameObject Panel)
    {
        Panel.SetActive(false);
        selectables.SetActive(true);
        // Sélection du premier élément par défaut dans le menu principale
        MenuP.Select();
    }

    public void Option_Click(GameObject Option)
    {
        selectables.SetActive(false);
        Option.SetActive(true);

        MenuO.Select();
    }

    public void Son_toggle()
    {
        if(Son.isOn == true)
        {
            AudioListener.volume = 1f ;
            Debug.Log("son Actif");
            DonneesJeu.Options.Muet = false;
        }
        else
        {
            AudioListener.volume = 0f;
            Debug.Log("son désactivé");
            DonneesJeu.Options.Muet = true;
        }
    }

    public void Debug_toggle()
    {
        if(debogage.isOn == true)
        {
            DonneesJeu.Options.Debogage = true;
            Debug.Log("débogage actif");
        }
        else
        {
            DonneesJeu.Options.Debogage = false;
            Debug.Log("débogage inactif");
        }
    }

    public void PleinEcran_toggle()
    {
        if(PleinEcran.isOn == true)
        {
            DonneesJeu.Options.PleinEcran = true;
            Debug.Log("FS");
            ChangerResolution();
        }
        else
        {
            DonneesJeu.Options.PleinEcran = false;
            Debug.Log("NO FS");
            ChangerResolution();
        }
    }

    public void ChangerResolution()
    {
        Debug.Log(Res.value);
        switch (Res.value)
        {
            case 0:
                Screen.SetResolution(640, 480, DonneesJeu.Options.PleinEcran);
                Debug.Log("res 640x480" + DonneesJeu.Options.PleinEcran);
                break;
            case 1:
                Screen.SetResolution(1280, 720, DonneesJeu.Options.PleinEcran);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, DonneesJeu.Options.PleinEcran);
                break;
        }
    }

    public void QuitterBtn()
    {
        Application.Quit();
    }

    public void CreerPersonnage()
    {
        Dictionary<int, RoleJoueur>.Enumerator e = DonneesJeu.Roles.GetEnumerator();

        Joueur perso = null;

        while(e.MoveNext())
        {
            if (e.Current.Value.Nom == Perso_Selector.nom)
            {
                perso = new Joueur(Nom, e.Current.Value, 5);
                break;
            }
        }

        DonneesJeu.Equipe.AjoutMembre(perso);
        GestScene.ProchaineSceneTransition("Monde");
    }
    public void changerNom(InputField nom)
    {
        Nom = nom.text;
    }
}
