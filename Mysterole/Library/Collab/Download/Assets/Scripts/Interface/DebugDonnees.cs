using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DebugDonnees : MonoBehaviour {

    public Dropdown Menu;
    public GameObject Roles;
    public GameObject Monstres;
    public GameObject EquipesMonstres;
    public GameObject Competences;
    public GameObject Etats;
    public GameObject Objets;

    // Use this for initialization
    void Awake ()
    {
        RendreActif(0);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ChangerActif()
    {
        RendreActif(Menu.value);
    }

    void RendreActif(int index)
    {
        RendreTousInactif();
        switch(index)
        {
            case 1:
                Monstres.SetActive(true);
                break;
            case 2:
                EquipesMonstres.SetActive(true);
                break;
            case 3:
                Competences.SetActive(true);
                break;
            case 4:
                Etats.SetActive(true);
                break;
            case 5:
                Objets.SetActive(true);
                break;
            case 0:
            default:
                Roles.SetActive(true);
                break;
        }
    }

    private void RendreTousInactif()
    {
        Roles.SetActive(false);
        Monstres.SetActive(false);
        EquipesMonstres.SetActive(false);
        Competences.SetActive(false);
        Etats.SetActive(false);
        Objets.SetActive(false);
    }
}
