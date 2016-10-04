using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Mysterole;

public class ErrorTests : MonoBehaviour {
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(300, 50, 100, 25), "Test"))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("unity"))
            {
                SceneManager.LoadScene("Test");
                Erreurs.NouvelleErreur("LOADING NEXT SCENE : Test");
            }
            else
            {
                SceneManager.LoadScene("unity");
                Erreurs.NouvelleErreur("LOADING NEXT SCENE : unity");
            }
        }
    }
}
