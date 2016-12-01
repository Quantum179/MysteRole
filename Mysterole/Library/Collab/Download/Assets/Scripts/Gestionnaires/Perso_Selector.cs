using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Perso_Selector : MonoBehaviour {

    public List<GameObject> PersoList;
    public int index = 0;
    public static string nom = null;

	// Use this for initialization
	void Start () {
        // Création de la liste des personnages
        GameObject[] Personnages = Resources.LoadAll<GameObject>("classes");
        foreach(GameObject c in Personnages) {

            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(this.transform);
            _char.transform.localPosition = Vector2.zero;
            _char.name = _char.name.Replace("(Clone)", "");

            PersoList.Add(_char);
            _char.SetActive(false);
            PersoList[index].SetActive(true);
            if (nom == null)
            {
                nom = _char.name;
            }
        }
	}	
    // affiche le personnage suivant en appuyant sur le boutton
	public void Suivant()
    {
        PersoList[index].SetActive(false);
        if(index == PersoList.Count -1) {
            index = 0;
        }
        else { 
            index++;
        }
        PersoList[index].SetActive(true);
        nom = PersoList[index].name;
    }
    // Affiche le personnage précédent en appuyant sur le boutton
    public void Precedent()
    {
        PersoList[index].SetActive(false);
        if (index == 0)
        {
            index =  PersoList.Count -1;
        }
        else
        {
            index--;
        }
        PersoList[index].SetActive(true);
        nom = PersoList[index].name;
    }
}
