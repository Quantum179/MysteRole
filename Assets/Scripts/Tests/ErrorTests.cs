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
        if (GUI.Button(new Rect(300, 50, 100, 25), "Débogueur"))
        {
            string message = "Changer valeur Déclencheur \"Test1\" : " + DonneesJeu.Declencheurs.EstActif("Test1").ToString();
            Erreurs.NouvelleErreur(message);
            DonneesJeu.Declencheurs.Inverser("Test1");
        }
    }
}
