using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mysterole;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu_Jeu : MonoBehaviour
{

    public GameObject OptionsM;
    public GameObject QuitterM;
    public GameObject MenuJeu;
    public GameObject PersoM;

    public Selectable MenuP;
    public Selectable SousMenuO;
    public Selectable SousMenuQ;
    public Selectable SousMenuPerso;

    public Text nomPerso;
    public Text lvlPerso;
    public Text PVPerso;
    public Text PuiPerso;
    public Text VitPerso;

    public string t = "";

    private EventSystem EventSystem;

    // Use this for initialization
    public void Start()
    {

        //Par default
        EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        MenuP.Select();
        MenuJeu.SetActive(false);
        OptionsM.SetActive(false);
        QuitterM.SetActive(false);
        PersoM.SetActive(false);

    }

    public void Option_Click(GameObject Panel)
    {
        // On met le dernier utiliser a false
        Panel.SetActive(false);

        OptionsM.SetActive(true);

       SousMenuO.Select();
    }

    public void retourMenu(GameObject Panel)
    {
        // Se bouton ferme le panel et reviens en focus sur la bar du menu
        Panel.SetActive(false);

        MenuP.Select();
    }

    public void retour_jeu()
    {
        // On ferme nous meme le menu de jeu
        MenuJeu.SetActive(false);
        JoueurMonde.PeutAgir = true;
    }

    public void  Quitter_Click()
    {
        QuitterM.SetActive(true);
        SousMenuQ.Select();
    }

    public void Quitter_Jeu()
    {
        // On retourne simplement a la scène du menu
        GestScene.ProchaineSceneTransition("Menu_Principal");
        DonneesJeu.Vider();
    }

    public void Personnage_Click(GameObject Panel)
    {
        // On met le dernier utiliser a false
        Panel.SetActive(false);

        PersoM.SetActive(true);

        // On met les données du personnage
       nomPerso.text = DonneesJeu.Equipe.Membres[0].Nom;

       lvlPerso.text = DonneesJeu.Equipe.Membres[0].NIV.ToString();

       PVPerso.text = DonneesJeu.Equipe.Membres[0].PV.ToString();

       PuiPerso.text = DonneesJeu.Equipe.Membres[0].PUI.ToString();

       VitPerso.text = DonneesJeu.Equipe.Membres[0].VIT.ToString();

        SousMenuPerso.Select();


    }

    public void OuvrirQuetes()
    {
        // On ferme nous meme le menu de jeu
        MenuJeu.SetActive(false);

        EcranQuete.OuvrirEcranQuete(t);
    }


    void Update()
    {
        if(!EventSystem.currentSelectedGameObject)
        {
            MenuP.Select();
        }
        
    }

}
