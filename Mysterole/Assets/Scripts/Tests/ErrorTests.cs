// Programme : Tests du logiciel
// Auteur : Jean-Michel Beauvais
// Scripts de tests pour le logiciel. Utilisé lorsque les menus actuels ne donnent pas les informations voulues ou pour forcer un évènement.

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Mysterole;

public class ErrorTests : MonoBehaviour {
    //public GameObject debug;
    // Use this for initialization
    public bool Debug;
    string message = "This is a test";
	void Start () {
        List<Dictionary<string, string>> stats = AccesBD.TrouverStats();

        message = "Inputs : ";
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnGUI()
    {
        if (Debug)
        {
            GUI.Label(new Rect(300, 20, 200, 500), message + Input.GetAxisRaw("Horizontal").ToString());
        }
    }
}
