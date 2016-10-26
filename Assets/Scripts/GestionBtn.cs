using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mysterole;

public class GestionBtn : MonoBehaviour {
    public UnityEngine.UI.Selectable Default;
    public void Start()
    {
        Default.Select();
    }
    public void NouvellePartieBtn(string NouvellePartie)
    {
        
        GestScene.ProchaineScene(NouvellePartie);
    }

    public void QuitterBtn()
    {
        Application.Quit();
    }

    public void CreerPersonnage(UnityEngine.UI.Text nom)
    {
        Joueur perso = new Joueur(nom.text, DonneesJeu.Roles[0], 5);
        DonneesJeu.Equipe.AjoutMembre(perso);
        GestScene.ProchaineSceneTransition("World");
    }
}
