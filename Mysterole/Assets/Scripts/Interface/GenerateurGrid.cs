using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GenerateurGrid : MonoBehaviour {

	public int espaceEntreCase;
	public int LargeurGrid;
	public int HauteurGrid;
	public GameObject cases;
	private GameObject caseClone;
	private GameObject[,] listeCases;

	// Use this for initialization
	void Start () {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Combat"));
        int carteChoisi = Random.Range(0, 2) ;
		if(carteChoisi==1){
			Instantiate (GameObject.Find ("CartePrefab").GetComponent<Prefab> ().carteCombat1prefab, GameObject.Find("Carte").transform);
			LargeurGrid=20;
			HauteurGrid=10;

			transform.position = new Vector3 (125,-250,0);
        }
        else
        {
            Instantiate(GameObject.Find("CartePrefab").GetComponent<Prefab>().carteCombat2prefab);
            LargeurGrid = 25;
            HauteurGrid = 7;

            transform.position = new Vector3(125, -250, 0);
        }
		listeCases = new GameObject[LargeurGrid+1, HauteurGrid+1];

		for (var i = 1; i < LargeurGrid+1; i++) {
			var go = new GameObject ();
			go.name = "Colonne" + i;
			go.transform.parent = gameObject.transform;
			for (var j =1; j < HauteurGrid+1; j++) {
				caseClone = Instantiate (cases,new Vector3(transform.position.x+(32*i)+(espaceEntreCase*i),transform.position.y-(32*j)-(espaceEntreCase*j),0), Quaternion.identity) as GameObject;
				caseClone.transform.parent = go.transform;
				caseClone.name = "case"+j;
				caseClone.GetComponent<GestionCases> ().PosX = i;
				caseClone.GetComponent<GestionCases> ().PosY = j;
				listeCases [i, j] = caseClone;
			}
		}
		for(var i= 1;i < LargeurGrid+1; i++){
			for(var j=1;j<HauteurGrid+1;j++){
				if (j != 1) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineHaut = listeCases[i,j-1];
				}
				if (j != HauteurGrid) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineBas = listeCases [i, j + 1];
				}
				if (i != 1) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineGauche = listeCases [i - 1, j];
				}
				if (i != LargeurGrid) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineDroite = listeCases [i + 1, j];
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
