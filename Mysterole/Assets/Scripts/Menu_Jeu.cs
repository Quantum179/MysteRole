using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mysterole;
using System.Collections.Generic;
using UnityEngine.UI;


public class Menu_Jeu : MonoBehaviour
{

    public GameObject OptionsM;
    public GameObject QuitterM;
    public GameObject MenuJeu;
    public GameObject PersoM;

    public Selectable MenuP;
    public Selectable SousMenuO;
    public Selectable SousMenuQ;

    public Text nomPerso;

    

    // Use this for initialization
    public void Start()
    {

        //Par default
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

    public void Personnage_Click()
    {
        //infoPerso perso;
        //perso = DonneesJeu.Adversaires
        //Debug.Log(perso.txtNom);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
            {
            MenuP.Select();
            }
    }

}
