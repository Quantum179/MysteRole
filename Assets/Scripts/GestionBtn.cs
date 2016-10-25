using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GestionBtn : MonoBehaviour {
    public UnityEngine.UI.Button Default;
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
}
