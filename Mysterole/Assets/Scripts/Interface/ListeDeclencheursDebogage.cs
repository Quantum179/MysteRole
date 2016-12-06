// Programme : Menu de débogage : Liste des Déclencheurs (Variables)
// Auteur : Jean-Michel Beauvais
// Actualise la liste des variables dynamiques de l'application et permet leur modification en temps réel.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ListeDeclencheursDebogage : MonoBehaviour {

    public GameObject ZoneListe;
    public GameObject TexteVide;
    public GameObject PrefabVariable;
    private Dictionary<string, bool> Liste;
    private Dictionary<string, GameObject> Interface;

    // Use this for initialization
    void Awake()
    {
        /*DonneesJeu.Declencheurs.RendreInactif("Test1");
        DonneesJeu.Declencheurs.RendreActif("Test2");
        DonneesJeu.Declencheurs.RendreInactif("Test4");
        DonneesJeu.Declencheurs.RendreActif("Test3");
        DonneesJeu.Declencheurs.RendreInactif("Test5");
        DonneesJeu.Declencheurs.RendreActif("Test6");
        DonneesJeu.Declencheurs.RendreInactif("Test7");
        DonneesJeu.Declencheurs.RendreActif("Test8");
        DonneesJeu.Declencheurs.RendreInactif("Test9");
        DonneesJeu.Declencheurs.RendreActif("Test10");
        DonneesJeu.Declencheurs.RendreInactif("Test11");
        DonneesJeu.Declencheurs.RendreActif("Test12");
        DonneesJeu.Declencheurs.RendreActif("Test13");
        DonneesJeu.Declencheurs.RendreInactif("Test14");
        DonneesJeu.Declencheurs.RendreActif("Test15");
        DonneesJeu.Declencheurs.RendreActif("Test16");
        DonneesJeu.Declencheurs.RendreInactif("Test17");
        DonneesJeu.Declencheurs.RendreActif("Test18");
        DonneesJeu.Declencheurs.RendreActif("Test19");
        DonneesJeu.Declencheurs.RendreInactif("Test20");
        DonneesJeu.Declencheurs.RendreActif("Test21");
        DonneesJeu.Declencheurs.RendreInactif("Pouette");*/
        //DonneesJeu.Declencheurs.RendreActif("Anticonstitutionnellement");
        Interface = new Dictionary<string, GameObject>();
    }
    void OnEnable() { 
        Rafraichir();
	}
	
	// Update is called once per frame
	void LateUpdate() {
        bool sale = false;
        Dictionary<string, bool>.Enumerator e1 = DonneesJeu.Declencheurs.RecupererListe().GetEnumerator();
        while (e1.MoveNext())
        {
            if (e1.Current.Value != Liste[e1.Current.Key])
            {
                sale = true;
            }
        }

        if (sale)
            Rafraichir();

        Dictionary<string, GameObject>.Enumerator e2 = Interface.GetEnumerator();
        while (e2.MoveNext())
        {
            bool currVal = e2.Current.Value.GetComponentInChildren<Toggle>().isOn;
            if (currVal != Liste[e2.Current.Key])
            {
                DonneesJeu.Declencheurs.Inverser(e2.Current.Key);
            }
        }
    }

    void Rafraichir()
    {
        Liste = DonneesJeu.Declencheurs.RecupererListe();
        if (Liste.Count <= 0)
        {
            TexteVide.SetActive(true);
            return;
        }
        TexteVide.SetActive(false);
        Dictionary<string, bool>.Enumerator e = Liste.GetEnumerator();
        int pos = 0;
        while (e.MoveNext())
        {
            if (!Interface.ContainsKey(e.Current.Key))
            {
                Interface.Add(e.Current.Key, Instantiate(PrefabVariable, ZoneListe.transform) as GameObject);
                Interface[e.Current.Key].name = e.Current.Key;
                Interface[e.Current.Key].GetComponentInChildren<Text>().text = e.Current.Key;
            }
            
            Interface[e.Current.Key].GetComponentInChildren<Toggle>().isOn = e.Current.Value;
            pos++;
        }
    }
}
