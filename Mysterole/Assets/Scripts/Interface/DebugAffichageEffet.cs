using UnityEngine;
using System.Collections;
using System;
using Mysterole;
using UnityEngine.UI;

public class DebugAffichageEffet : MonoBehaviour {

    public GameObject Scenarise;
    public GameObject Stat;
    public GameObject Degats;
    public GameObject Soins;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Retirer()
    {
        DestroyImmediate(gameObject);
    }

    public void Initier(Effet effet)
    {
        Scenarise.SetActive(false);
        Stat.SetActive(false);
        Degats.SetActive(false);
        Soins.SetActive(false);
        gameObject.SetActive(true);

        //Debug.Log("Type effet : " + effet.GetType());

        if (effet.GetType().Equals(typeof(Degats)))
        {
            Degats.SetActive(true);
            Dropdown d = Degats.GetComponentInChildren<Dropdown>();
            for (int i = 0; i < d.options.Count; i++)
            {
                if (d.options[i].text == (effet as Degats).Affecte.ToString())
                {
                    d.value = i;
                    break;
                }
            }
            d.RefreshShownValue();
        }
        else if (effet.GetType().Equals(typeof(Soins)))
        {
            Soins.SetActive(true);
            Dropdown d = Soins.GetComponentInChildren<Dropdown>();
            for (int i = 0; i < d.options.Count; i++)
            {
                if (d.options[i].text == (effet as Soins).Affecte.ToString())
                {
                    d.value = i;
                    break;
                }
            }
            d.RefreshShownValue();
        }
        else if (effet.GetType().Equals(typeof(EffetStat)))
        {
            Stat.SetActive(true);
            Dropdown d = Stat.GetComponentInChildren<Dropdown>();
            for (int i = 0; i < d.options.Count; i++)
            {
                if (d.options[i].text == (effet as EffetStat).Affecte.ToString())
                {
                    d.value = i;
                    break;
                }
            }
            d.RefreshShownValue();
            InputField Montant = Stat.GetComponentInChildren<InputField>();
            Montant.text = (effet as EffetStat).Montant.ToString();
        }
        else if (effet.GetType().Equals(typeof(EffetScenarise)))
        {
            Scenarise.SetActive(true);
            InputField i = Stat.GetComponentInChildren<InputField>();
            i.text = (effet as EffetScenarise).ID.ToString();
        }
        else
        {
            gameObject.SetActive(false);
            Erreurs.NouvelleErreur("Un effet inconnu fut fourni pour la liste.");
        }
    }
}
