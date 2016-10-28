﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EcranDialogue : MonoBehaviour {

    static GameObject Main;
    public Text nomPersonnage;
    static Text np;
    public Text textZone;
    static Text tz;

	// Use this for initialization
	void Start () {
        Main = gameObject;
        np = nomPersonnage;
        tz = textZone;
        gameObject.SetActive(false);
	}
	

    static public void openDialog()
    {
        Main.SetActive(true);
    }

    static public void closeDialog()
    {
        Main.SetActive(false);
    }

    static public void NewDialog(string nomP, string message)
    {
        np.text = nomP;
        tz.text = message;
        Main.SetActive(true);
    }



    //Faire Update en surveillant la touche Accepter
    //Renvoyer en réponse la validation de l'événement, permettant l'activation de la suivante



    //void OnGUI()
    //{
    //    if (change)
    //    {
    //        Main.GetComponentInChildren<Scrollbar>().value = 0;
    //        change = false;
    //    }
    //}
    //static public void NouvelleErreur(string message)
    //{
    //    tz.text += message + '\n';
    //    Main.SetActive(true);
    //    change = true;
    //}

    //public void OnClick()
    //{
    //    gameObject.SetActive(false);
    //}
}
