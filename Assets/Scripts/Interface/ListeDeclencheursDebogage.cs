using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ListeDeclencheursDebogage : MonoBehaviour {

    public GameObject ZoneListe;
    public GameObject TexteVide;
    public GameObject PrefabDeclencheur;
    private Dictionary<string, bool> Liste;
    private Dictionary<string, GameObject> Interface;

	// Use this for initialization
	void Start ()
    {
        DonneesJeu.Declencheurs.RendreInactif("Test1");
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
        DonneesJeu.Declencheurs.RendreInactif("Pouette");
        DonneesJeu.Declencheurs.RendreActif("Anticonstitutionnellement");
        Interface = new Dictionary<string, GameObject>();

        Rafraichir();
	}
	
	// Update is called once per frame
	void OnGUI() {
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
                Interface.Add(e.Current.Key, Instantiate(PrefabDeclencheur, ZoneListe.transform) as GameObject);
                Interface[e.Current.Key].name = e.Current.Key;
                Interface[e.Current.Key].GetComponentInChildren<Text>().text = e.Current.Key;
            }

            /*float x = 205 * (pos % 2);
            float y = -20 * Mathf.FloorToInt(pos / 2.0f);
            Debug.Log(e.Current.Key + " - X : " + x.ToString() + " / Y : " + y.ToString());
            Interface[e.Current.Key].transform.localPosition = new Vector3(x, y);*/
            Interface[e.Current.Key].GetComponentInChildren<Toggle>().isOn = e.Current.Value;
            pos++;
        }
    }
}
