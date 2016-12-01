using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mysterole;
using System.Collections.Generic;

public class DebugEtats : MonoBehaviour {

    public Dropdown ListeEtats;
    public InputField Nom;
    public GameObject ListeEffetsPerm;
    public GameObject ListeEffetsPeri;

    // Use this for initialization
    void Start () {
        ListeEtats.ClearOptions();
        Dropdown.OptionData od = new Dropdown.OptionData("- Nouvelle -");
        Dictionary<int, Etat>.Enumerator e = DonneesJeu.Etats.GetEnumerator();
        ListeEtats.options.Add(od);
        while(e.MoveNext())
        {
            od = new Dropdown.OptionData(e.Current.Key + " - " + e.Current.Value.Nom);
            ListeEtats.options.Add(od);
        }
        ListeEtats.value = 1;
        ListeEtats.RefreshShownValue();
        ActualiserEtats();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActualiserEtats()
    {
        if (ListeEtats.value == 0)
        {
            ListeEtats.value = 1;
            ListeEtats.RefreshShownValue();
        }
        int id = int.Parse(ListeEtats.captionText.text.Split(' ')[0]);
        Etat etat = DonneesJeu.Etats[id];
        Nom.text = etat.Nom;
        ListeEffetsPerm.GetComponent<DebugEffets>().Vider();
        ListeEffetsPeri.GetComponent<DebugEffets>().Vider();
        foreach (Effet effet in etat.EffetsPermanents)
        {
            ListeEffetsPerm.GetComponent<DebugEffets>().AjoutEffet(effet);
        }
        if (etat.GetType().Equals(typeof(EtatPeriodique)))
        {
            foreach(Effet effet in (etat as EtatPeriodique).EffetsParTour)
            {
                ListeEffetsPeri.GetComponent<DebugEffets>().AjoutEffet(effet);
            }
        }
    }
}
