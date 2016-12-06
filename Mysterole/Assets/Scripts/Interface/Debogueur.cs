// Programme : Menu de débogage
// Auteur : Jean-Michel Beauvais
// Gestion des éléments de débogage possible dans le système.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class Debogueur : MonoBehaviour {
    public Dropdown ChoixMenu;
    private Dictionary<string, GameObject> ListeChoix;

	// Use this for initialization
    void Awake()
    {
        ListeChoix = new Dictionary<string, GameObject>();
    }

    void OnEnable()
    {
        Mysterole.JoueurMonde.PeutAgir = false;
    }
	void Start () {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("DebugDropdown"))
        {
            ListeChoix.Add(go.name, go);
            Dropdown.OptionData od = new Dropdown.OptionData(go.name);
            ChoixMenu.options.Add(od);
            go.SetActive(false);
        }
        ChoixMenu.RefreshShownValue();
        ChoixMenu.value = 0;
        ChangerMenuChoisi();
        gameObject.SetActive(false);
	}
   

    public void ChangerMenuChoisi()
    {
        Dictionary<string, GameObject>.Enumerator e = ListeChoix.GetEnumerator();
        while (e.MoveNext())
        {
            if (ChoixMenu.options[ChoixMenu.value].text == e.Current.Key)
                e.Current.Value.SetActive(true);
            else
                e.Current.Value.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnDisable()
    {
        if (!Mysterole.JoueurMonde.PeutAgir)
        {
            Mysterole.JoueurMonde.PeutAgir = true;
        }
    }

    public void FermerDebogueur()
    {
        Mysterole.JoueurMonde.PeutAgir = true;
        gameObject.SetActive(false);
    }
}
