using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Debogueur : MonoBehaviour {
    public GameObject ChoixMenu;
    private Dropdown Choix;
    private Dictionary<string, GameObject> ListeChoix;
	// Use this for initialization
	void Start () {
        ListeChoix = new Dictionary<string, GameObject>();
        Choix = ChoixMenu.GetComponent<Dropdown>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("DebugDropdown"))
        {
            ListeChoix.Add(go.name, go);
            Dropdown.OptionData od = new Dropdown.OptionData(go.name);
            Choix.options.Add(od);
            go.SetActive(false);
        }
        Choix.RefreshShownValue();
        Choix.value = 0;
        ChangerMenuChoisi();
        gameObject.SetActive(false);
	}

    public void ChangerMenuChoisi()
    {
        Dictionary<string, GameObject>.Enumerator e = ListeChoix.GetEnumerator();
        while (e.MoveNext())
        {
            if (Choix.options[Choix.value].text == e.Current.Key)
                e.Current.Value.SetActive(true);
            else
                e.Current.Value.SetActive(false);
        } //while (e.MoveNext());
    }
	
	// Update is called once per frame
	void Update () {
	}
}
