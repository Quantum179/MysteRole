// Auteur : Jean-Michel
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Mysterole;

public class ErrorTests : MonoBehaviour {
    int i = 0;
    string[] noms = { "Switch01", "Testing" };
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(300, 50, 100, 25), "Test déclencheurs"))
        {
            string message = "Inversion du déclencheur : " + noms[i % 2] + " (" + DonneesJeu.Declencheurs.Inverser(noms[i % 2]).ToString() + ")";
            Erreurs.NouvelleErreur(message);
            i++;
            if (i == 3)
                i = 0;
        }
    }
}
