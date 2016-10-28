// Auteur : Jean-Michel
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Mysterole;

public class ErrorTests : MonoBehaviour {
    //public GameObject debug;
	// Use this for initialization
	void Start () {
        /*Dictionary<string, string>[] retour = { };
        retour = BDUtils.AccesBD.SELECT();
        message = "";

        //for (int i=0; i < retour; i++)
        System.Collections.IEnumerator ie = retour.GetEnumerator();
        while (ie.MoveNext())
        {
            Dictionary<string, string>.Enumerator e = (ie.Current as Dictionary<string, string>).GetEnumerator();
            while (e.MoveNext())
            {
                message += e.Current.Key + " : " + e.Current.Value + " // ";
            }
            message += '\n';
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnGUI()
    {
        /*if (GUI.Button(new Rect(300, 50, 100, 25), "Débogueur"))
        {
            string message = "Changer valeur Déclencheur \"Test1\" : " + DonneesJeu.Declencheurs.EstActif("Test1").ToString();
            Erreurs.NouvelleErreur(message);
            DonneesJeu.Declencheurs.Inverser("Test1");
        }
        GUI.Label(new Rect(300, 200, 200, 40), "En Transition : " + GestTransition.EnTransition.ToString());*/
    }
}
