// Auteur : Jean-Michel
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Mysterole;

public class ErrorTests : MonoBehaviour {
    public GameObject debug;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(300, 50, 100, 25), "Nom Joueur"))
        {
            string message = "Nom du joueur : " + DonneesJeu.Equipe.Membres[0].Nom;
            Erreurs.NouvelleErreur(message);
        }
    }
}
