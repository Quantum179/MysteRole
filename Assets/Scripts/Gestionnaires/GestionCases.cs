using UnityEngine;
using System.Collections;
using Mysterole;

public class GestionCases : MonoBehaviour {

	public bool estOccupee{ get; set; }
	public bool estMouvement{ get; set;}
	public bool estAttack{ get; set; }
	public Sprite caseAttaque;
	public Sprite caseMouvement;
	public Sprite caseNormal;
	public int RangeeCalque{ get; set;}
	// Liste de case voisine
	public GameObject caseVoisineHaut{get;set;}
	public GameObject caseVoisineBas{ get; set;}
	public GameObject caseVoisineDroite{ get; set; }
	public GameObject caseVoisineGauche{ get; set; }
	// Personnage associé a la case
	public GameObject personnage{get;set;}
	private bool curseur;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (estAttack) {
			GetComponent <SpriteRenderer> ().sprite = caseAttaque;
		} else if (estMouvement) {
			GetComponent <SpriteRenderer> ().sprite = caseMouvement;
		} else {
			GetComponent <SpriteRenderer> ().sprite = caseNormal;
		}
	}

	public void changeeMouvement(int portee,bool modif){
		if (!estOccupee) {
			estMouvement = modif;
		}
		if (portee > 0) {
			if (caseVoisineBas && !caseVoisineBas.GetComponent<GestionCases>().estOccupee) {
				caseVoisineBas.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
			if (caseVoisineDroite && !caseVoisineDroite.GetComponent<GestionCases>().estOccupee) {
				caseVoisineDroite.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
			if (caseVoisineGauche && !caseVoisineGauche.GetComponent<GestionCases>().estOccupee) {
				caseVoisineGauche.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
				if (caseVoisineHaut && !caseVoisineHaut.GetComponent<GestionCases>().estOccupee) {
				caseVoisineHaut.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
		}
	}

	public void changeeAttaque(Competence atk, bool modif,int parcourDistance){
		if (parcourDistance >= atk.Cible.DistanceMin) {
			estAttack = modif;
		}
		if (parcourDistance < atk.Cible.DistanceMax) {
			if (caseVoisineBas) {
				caseVoisineBas.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
			if (caseVoisineDroite) {
				caseVoisineDroite.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
			if (caseVoisineGauche) {
				caseVoisineGauche.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
			if (caseVoisineHaut) {
				caseVoisineHaut.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
		}
	}

	public void SwitchOccupation(GameObject perso,bool modif)
	{
		estOccupee = modif;
		personnage = modif ? perso : null;
	}
}
