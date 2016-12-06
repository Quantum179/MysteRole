// Programme : Menu de débogage : Données des Effets
// Auteur : Jean-Michel Beauvais
// Gère l'affichage et l'actualisation des données sur les effets d'une compétence ou d'un état.

using UnityEngine;
using System.Collections;
using Mysterole;
using System.Collections.Generic;

public class DebugEffets : MonoBehaviour {

    public GameObject Liste;
    public GameObject PrefabEffet;

    private List<GameObject> Effets = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AjoutEffet(Effet effet)
    {
        GameObject nouvEffet = Instantiate(PrefabEffet, Liste.transform) as GameObject;
        nouvEffet.GetComponent<DebugAffichageEffet>().Initier(effet);
        Effets.Add(nouvEffet);
    }

    public void Vider()
    {
        foreach (GameObject go in Effets)
        {
            go.GetComponent<DebugAffichageEffet>().Retirer(); ;
        }
        Effets = new List<GameObject>();
    }
}
