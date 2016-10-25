// Auteur : Jean-Michel
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Mysterole;

public class ErrorTests : MonoBehaviour {
    public string message;
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
        //GUI.Label(new Rect(0, 0, 200, 400), message);
    }
}
