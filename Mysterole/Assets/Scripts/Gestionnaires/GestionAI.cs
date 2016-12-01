using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Mysterole;

public class GestionAI : MonoBehaviour {

	Personnage monPersonnage;
	public GameObject CiblePersonnage{ get; set;}
	public List<GameObject> routeCourante = null;

	// Use this for initialization
	public void Start () {
		monPersonnage = GetComponent<GestionPersonnage> ().monPersonnage;
	}

	void Update () {
		/*if (routeCourante != null) {
			int currNode = 0;
			while (currNode < routeCourante.Count - 1) {
				Vector3 start = routeCourante[currNode].transform.position;
				Vector3 end = routeCourante[currNode+1].transform.position;
				Debug.DrawLine (start, end,Color.red);
				currNode++;
			}
		}*/
	}

	public void ObtenirCiblePlusDeVie(List<GameObject> listJoueur){
		if (listJoueur[0]) {
			listJoueur.Sort (((x, y) => x.GetComponent<GestionPersonnage> ().monPersonnage.PV.CompareTo (y.GetComponent<GestionPersonnage> ().monPersonnage.PV)));
			listJoueur.Reverse ();
			CiblePersonnage = listJoueur [0];
		}
	}



	public void CalculerRoute(){
		var maCase = GetComponent<PersonnageMouvement> ().caseActuel;
		var cibleCase = CiblePersonnage.GetComponent<PersonnageMouvement> ().caseActuel;

		routeCourante = GameObject.Find ("CombatGrid").GetComponent<GenerateurGrid> ().genererRoute (maCase, cibleCase);
	}

	public bool DeplacerEnemi (){
		if (routeCourante != null) {
			if (routeCourante.Count > monPersonnage.AttaqueBase.Cible.DistanceMax) {
				if (routeCourante.Count > monPersonnage.MOV) {
					GetComponent<PersonnageMouvement>().caseDestination = routeCourante[monPersonnage.MOV-1];
				} else {
					GetComponent<PersonnageMouvement>().caseDestination = routeCourante[routeCourante.Count-1-monPersonnage.AttaqueBase.Cible.DistanceMax];
				}
				return true;
			} else {
				return false;
			}
		} else {
			GetComponent<PersonnageMouvement> ().SetFiniMouvementTrue ();
			return true;
		}
	}

	public void AttaqueBase(){
		GetComponent<PersonnageMouvement> ().faitAttaque (CiblePersonnage.GetComponent<PersonnageMouvement> ().caseActuel);
		CiblePersonnage.GetComponent<GestionPersonnage> ().ReduirePV (monPersonnage.PUI);
	}
}