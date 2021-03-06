using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GestionTour : MonoBehaviour {

	public GameObject gestionCombat;
	public List<Text> tourJoueurAttente;

	private List<GameObject> listPersonnage = new List<GameObject>();
	private GameObject tourAjouer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Text t in tourJoueurAttente) {t.text = "";}


		if (gestionCombat.GetComponent<GestionCombat> ().tourDuJoueurAJouer) {
			listPersonnage = gestionCombat.GetComponent<GestionCombat> ().listPersonnage;
			tourAjouer = gestionCombat.GetComponent<GestionCombat> ().tourDuJoueurAJouer;
			for (int i=0; i < tourJoueurAttente.Count; i++) {
				if(i+listPersonnage.FindIndex(a=>a==tourAjouer) < listPersonnage.Count){
					tourJoueurAttente [i].text = listPersonnage [i + listPersonnage.FindIndex (a => a == tourAjouer)].GetComponent<GestionPersonnage> ().monPersonnage.Nom;
				}
			}
		}
	}
}
