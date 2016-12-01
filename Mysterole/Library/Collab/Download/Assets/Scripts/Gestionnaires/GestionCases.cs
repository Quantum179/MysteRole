using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mysterole;

public class GestionCases : MonoBehaviour {

	public int PosX { get; set; }
	public int PosY { get; set;}
	public bool EstOccupee{ get; set; }
	public bool EstMouvement{ get; set;}
	public bool EstAttack{ get; set; }
	public bool EstPossibleAttack{ get; set; }
	public Sprite caseAttaque;
	public Sprite caseMouvement;
	public Sprite caseNormal;
	public Sprite casePossibleAttack;
	// Liste de case voisine
	public GameObject CaseVoisineHaut{get;set;}
	public GameObject CaseVoisineBas{ get; set;}
	public GameObject CaseVoisineDroite{ get; set; }
	public GameObject CaseVoisineGauche{ get; set; }
	// Personnage associé a la case
	public GameObject Personnage{get;set;}
	private bool curseur;
	public List<GameObject> voisins = new List<GameObject> ();
	public int gCost;
	public int hCost;
	public GameObject parent;

	public int fCost {get {return gCost + hCost;}}

	// Use this for initialization
	void Start () {
		voisins.Add (CaseVoisineBas);
		voisins.Add (CaseVoisineDroite);
		voisins.Add (CaseVoisineGauche);
		voisins.Add (CaseVoisineHaut);
	}
	
	// Update is called once per frame
	void Update () {
		if (EstAttack) {
			GetComponent <SpriteRenderer> ().sprite = caseAttaque;
		} else if (EstPossibleAttack) {
			GetComponent<SpriteRenderer> ().sprite = casePossibleAttack;
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

	public void changeeAttaque(int distMin,int distMax, bool modif,int parcourDistance){
		if (parcourDistance >= distMin) {
			EstAttack = modif;
		}
		if (parcourDistance < distMax) {
			if (CaseVoisineBas) {
				CaseVoisineBas.GetComponent<GestionCases> ().changeeAttaque (distMin,distMax, modif,parcourDistance+1);
			}
			if (CaseVoisineDroite) {
				CaseVoisineDroite.GetComponent<GestionCases> ().changeeAttaque (distMin,distMax, modif,parcourDistance+1);
			}
			if (CaseVoisineGauche) {
				CaseVoisineGauche.GetComponent<GestionCases> ().changeeAttaque (distMin,distMax, modif,parcourDistance+1);
			}
			if (CaseVoisineHaut) {
				CaseVoisineHaut.GetComponent<GestionCases> ().changeeAttaque (distMin,distMax, modif,parcourDistance+1);
			}
		}
	}

	public void changeeAttaquePotentiel(GameObject origin,int distMin,int distMax,bool modif,int parcourDistance){
		if (parcourDistance >= distMin) {
			EstPossibleAttack = modif;
		}
		if (parcourDistance < distMax) {
			foreach (GameObject voisin in voisins) {
				if (voisin != null) {
					voisin.GetComponent<GestionCases> ().changeeAttaquePotentiel (origin,distMin, distMax, modif, parcourDistance + 1);
				}
			}
		}
		if (parcourDistance == 0) {
			DesactiverAttaquePotentiel (0, distMin, false, 0);
		}
	}

	public void DesactiverAttaquePotentiel(int distMin,int distMax,bool modif,int parcourDistance){
		if (parcourDistance >= distMin) {
			EstPossibleAttack = modif;
		}
		if (parcourDistance < distMax) {
			foreach (GameObject voisin in voisins) {
				if (voisin != null) {
					voisin.GetComponent<GestionCases> ().DesactiverAttaquePotentiel (distMin, distMax, modif, parcourDistance + 1);
				}
			}
		}
	}

	public List<Vector2> ListVectorAttaqueLigne(Competence atk,string direction){
		List<Vector2> listVector =new List<Vector2>();
		if (direction == "gauche") {
			for (int i = atk.Cible.DistanceMin; i < atk.Cible.DistanceMin+atk.Cible.Zone.Longueur; i++) {
				listVector.Add (new Vector2 (PosX - i, PosY));
				listVector.Add (new Vector2 (PosX - i, PosY - (atk.Cible.Zone.Largeur-1)/2));
				listVector.Add (new Vector2 (PosX - i, PosY + (atk.Cible.Zone.Largeur-1)/2));
			}
		}
		if (direction == "droite") {
			for (int i = atk.Cible.DistanceMin; i < atk.Cible.DistanceMin+atk.Cible.Zone.Longueur; i++) {
				listVector.Add (new Vector2 (PosX + i, PosY));
				listVector.Add (new Vector2 (PosX + i, PosY - (atk.Cible.Zone.Largeur-1)/2));
				listVector.Add (new Vector2 (PosX + i, PosY + (atk.Cible.Zone.Largeur-1)/2));
			}
		}
		if (direction == "haut") {
			for (int i = atk.Cible.DistanceMin; i < atk.Cible.DistanceMin+atk.Cible.Zone.Longueur; i++) {
				listVector.Add (new Vector2 (PosX,PosY-i));
				listVector.Add (new Vector2 (PosX-(atk.Cible.Zone.Largeur-1)/2,PosY-i));
				listVector.Add (new Vector2 (PosX+(atk.Cible.Zone.Largeur-1)/2,PosY-i));
			}
		}
		if (direction == "bas") {
			for (int i = atk.Cible.DistanceMin; i < atk.Cible.DistanceMin+atk.Cible.Zone.Longueur; i++) {
				listVector.Add (new Vector2 (PosX,PosY+i));
				listVector.Add (new Vector2 (PosX-(atk.Cible.Zone.Largeur-1)/2,PosY+i));
				listVector.Add (new Vector2 (PosX+(atk.Cible.Zone.Largeur-1)/2,PosY+i));
			}
		}

		return listVector;
	}
		

	public void SwitchOccupation(GameObject perso,bool modif)
	{
		EstOccupee = modif;
		Personnage = modif ? perso : null;
	}

}
