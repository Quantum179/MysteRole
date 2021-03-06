﻿// Programme : Chargement du Menu de Jeu
// Auteur : Jean-Michel Beauvais
// Permet la modification du Menu de Jeu sans modifier la « scène » Monde.
// Évite les erreurs dans la gestion des versions sans empêche l'utilisation du Menu.

using UnityEngine;
using System.Collections;
using Mysterole;

public class ChargerMenu : MonoBehaviour {
    public GameObject PrefabMenu;
    private GameObject Menu = null;

	// Use this for initialization
	void Start () {
        FaireMenu();
    }
	
	// Update is called once per frame
	void Update () {
        if (Menu == null)
            FaireMenu();
        if (Input.GetButtonUp("Menu"))
        {
            //Debug.Log("MENU!");
            if (Menu.activeSelf)
            {
                Menu.SetActive(false);
                JoueurMonde.PeutAgir = true;
            }
            else if (!Menu.activeSelf && JoueurMonde.PeutAgir)
            {
                Menu.SetActive(true);
                JoueurMonde.PeutAgir = false;
            }
        }
        Menu.transform.Translate(Vector2.zero);
    }

    void FaireMenu()
    {
        Menu = Instantiate(PrefabMenu, gameObject.transform.parent) as GameObject;
        /*Menu.transform.position = new Vector2(0,0);
        Menu.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        Menu.GetComponent<RectTransform>().anchorMax = Vector2.one;*/
        /*Menu.transform.up = Vector2.zero;
        Menu.transform.right = Vector2.zero;*/
    }
}
