using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateurGrid : MonoBehaviour {

	public int espaceEntreCase;
	public int LargeurGrid;
	public int HauteurGrid;
	public GameObject cases;
	private GameObject caseClone;
	private GameObject[,] listeCases;


	// Use this for initialization
	void Start () {
		listeCases = new GameObject[LargeurGrid+1, HauteurGrid+1];

		for (var i = 1; i < LargeurGrid+1; i++) {
			var go = new GameObject ();
			go.name = "Colonne" + i;
			go.transform.parent = gameObject.transform;
			for (var j =1; j < HauteurGrid+1; j++) {
				caseClone = Instantiate (cases,new Vector3(transform.position.x+(32*i)+(espaceEntreCase*i),transform.position.y-(32*j)-(espaceEntreCase*j),0), Quaternion.identity) as GameObject;
				caseClone.transform.parent = go.transform;
				caseClone.name = "case"+j;
				caseClone.GetComponent<GestionCases> ().RangeeCalque = j+1;
				listeCases [i, j] = caseClone;
			}
		}
		for(var i= 1;i < LargeurGrid+1; i++){
			for(var j=1;j<HauteurGrid+1;j++){
				if (j != 1) {
					listeCases [i, j].GetComponent<GestionCases> ().caseVoisineHaut = listeCases[i,j-1];
				}
				if (j != HauteurGrid) {
					listeCases [i, j].GetComponent<GestionCases> ().caseVoisineBas = listeCases [i, j + 1];
				}
				if (i != 1) {
					listeCases [i, j].GetComponent<GestionCases> ().caseVoisineGauche = listeCases [i - 1, j];
				}
				if (i != LargeurGrid) {
					listeCases [i, j].GetComponent<GestionCases> ().caseVoisineDroite = listeCases [i + 1, j];
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
