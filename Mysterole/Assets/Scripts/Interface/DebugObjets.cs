using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Mysterole;

public class DebugObjets : MonoBehaviour {

    public Dropdown Liste;
    public InputField Nom;
    public GameObject Consommable;
    public Dropdown EffetObjet;
    public InputField ForceObjet;
    public GameObject Equipement;
    public GameObject Anneau;
    public GameObject ObjetCle;

    // Use this for initialization
    void Start () {
        Liste.ClearOptions();
        Dropdown.OptionData od = new Dropdown.OptionData("- Nouveau -");
        Liste.options.Add(od);
        Dictionary<int, Objets.Objet>.Enumerator e = Objets.Liste.GetEnumerator();
        while(e.MoveNext())
        {
            od = new Dropdown.OptionData(e.Current.Key + " - " + e.Current.Value.Nom);
            Liste.options.Add(od);
        }
        EffetObjet.ClearOptions();
        Dictionary<int, Competence>.Enumerator ec = DonneesJeu.Competences.GetEnumerator();
        while(ec.MoveNext())
        {
            od = new Dropdown.OptionData(ec.Current.Key + " - " + ec.Current.Value.Nom);
            EffetObjet.options.Add(od);
        }
        EffetObjet.RefreshShownValue();
        Liste.value = 1;
        Liste.RefreshShownValue();
	}

    public void AfficherObjet()
    {
        Objets.Objet o = Objets.TrouverObjet(int.Parse(Liste.captionText.text.Split(' ')[0]));
        Consommable.SetActive(false);
        Equipement.SetActive(false);
        Anneau.SetActive(false);
        ObjetCle.SetActive(false);

        Nom.text = o.Nom;

        if (o.GetType().Equals(typeof(Objets.Consommable)))
        {
            Consommable.SetActive(true);
            ForceObjet.text = (o as Objets.Consommable).PUI.ToString();
            for(int i = 0; i < EffetObjet.options.Count; i++)
            {
                if (int.Parse(EffetObjet.options[i].text.Split(' ')[0]) == (o as Objets.Consommable).Effet.ID)
                {
                    EffetObjet.value = i;
                    break;
                }
            }
            EffetObjet.RefreshShownValue();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
