using UnityEngine;
using System.Collections;
using Mysterole;

public class GestionCases : MonoBehaviour {

	public int PosX { get; set; }
	public int PosY { get; set;}
	public bool EstOccupee{ get; set; }
	public bool EstMouvement{ get; set;}
	public bool EstAttack{ get; set; }
	public Sprite caseAttaque;
	public Sprite caseMouvement;
	public Sprite caseNormal;
	// Liste de case voisine
	public GameObject CaseVoisineHaut{get;set;}
	public GameObject CaseVoisineBas{ get; set;}
	public GameObject CaseVoisineDroite{ get; set; }
	public GameObject CaseVoisineGauche{ get; set; }
	// Personnage associé a la case
	public GameObject Personnage{get;set;}
	private bool curseur;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (EstAttack) {
			GetComponent <SpriteRenderer> ().sprite = caseAttaque;
		} else if (EstMouvement) {
			GetComponent <SpriteRenderer> ().sprite = caseMouvement;
		} else {
			GetComponent <SpriteRenderer> ().sprite = caseNormal;
		}
	}

	public void changeeMouvement(int portee,bool modif){
		if (!EstOccupee) {
			EstMouvement = modif;
		}
		if (portee > 0) {
			if (CaseVoisineBas && !CaseVoisineBas.GetComponent<GestionCases>().EstOccupee) {
				CaseVoisineBas.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
			if (CaseVoisineDroite && !CaseVoisineDroite.GetComponent<GestionCases>().EstOccupee) {
				CaseVoisineDroite.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
			if (CaseVoisineGauche && !CaseVoisineGauche.GetComponent<GestionCases>().EstOccupee) {
				CaseVoisineGauche.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
				if (CaseVoisineHaut && !CaseVoisineHaut.GetComponent<GestionCases>().EstOccupee) {
				CaseVoisineHaut.GetComponent<GestionCases> ().changeeMouvement (portee-1,modif);
			}
		}
	}

	public void changeeAttaque(Competence atk, bool modif,int parcourDistance){
		if (parcourDistance >= atk.Cible.DistanceMin) {
			EstAttack = modif;
		}
		if (parcourDistance < atk.Cible.DistanceMax) {
			if (CaseVoisineBas) {
				CaseVoisineBas.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
			if (CaseVoisineDroite) {
				CaseVoisineDroite.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
			if (CaseVoisineGauche) {
				CaseVoisineGauche.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
			if (CaseVoisineHaut) {
				CaseVoisineHaut.GetComponent<GestionCases> ().changeeAttaque (atk, modif,parcourDistance+1);
			}
		}
	}

	public void SwitchOccupation(GameObject perso,bool modif)
	{
		EstOccupee = modif;
		Personnage = modif ? perso : null;
	}
}
