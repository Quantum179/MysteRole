using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurseurAnim : MonoBehaviour {
	private Animator animator;
	public string option{ get; set; }
	public bool ClickAccepte{ get; set; }
	public GameObject tourDuJoueurAJouer;
	private GameObject CombatGrid;

	public GameObject curseurCaseActuel;
	private bool m_isAxisUtiliseX = false;
	private bool m_isAxisUtiliseY = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		CombatGrid = GameObject.Find ("CombatGrid");
		ClickAccepte = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Accepter")) {
			if (ClickAccepte) {
				animator.SetBool ("isClick", true);
			} else {
				animator.SetBool ("isError", true);
			}
		} else if (Input.GetButtonUp ("Accepter")) {
			if (ClickAccepte) {
				animator.SetBool ("isClick", false);
			} else {
				animator.SetBool ("isError", false);
			}
		}

		// Si c'est une attaque special ligne on ne veut pas que le curseur bouge.
		if (option != "AttaqueSpecialLigne") {
			if (Input.GetAxis ("Horizontal") != 0) {
				if (option == "AttaqueSpecialZone") {curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (0,tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueSpeciale.Cible.Zone.Largeur, false, 0);}
				if (m_isAxisUtiliseX == false) {
					// Flèche droite
					if (Input.GetAxis ("Horizontal") > 0) {
						DeplacerCurseur (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineDroite);
					}
					// Flèche gauche
					if (Input.GetAxis ("Horizontal") < 0) {
						DeplacerCurseur (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineGauche);
					}
					m_isAxisUtiliseX = true;
				}
			} else {
				m_isAxisUtiliseX = false;
			}
			if (Input.GetAxis ("Vertical") != 0) {
				if (option == "AttaqueSpecialZone") {curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (0,tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale.Cible.Zone.Largeur, false, 0);}
				if (m_isAxisUtiliseY == false) {
					// Flèche haut
					if (Input.GetAxis ("Vertical") > 0) {
						DeplacerCurseur (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineHaut);
					}
					// Flèche bas
					if (Input.GetAxis ("Vertical") < 0) {
						DeplacerCurseur (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineBas);
					}
					m_isAxisUtiliseY = true;
				}
			} else {
				m_isAxisUtiliseY = false;
			}
			if (option == "AttaqueSpecialZone") {curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (0,tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale.Cible.Zone.Largeur, true, 0);
				tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().listCaseCible = CombatGrid.GetComponent<GenerateurGrid> ().TrouverCaseAttaque ();}
		} else {
			if (Input.GetAxis ("Horizontal") != 0) {
				if (m_isAxisUtiliseX == false) {
					// Flèche droite
					if (Input.GetAxis ("Horizontal") > 0) {
						tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (false);
						List<Vector2> lstVect = curseurCaseActuel.GetComponent<GestionCases> ().ListVectorAttaqueLigne (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueSpeciale,"droite");
						tourDuJoueurAJouer.GetComponent<GestionPersonnage>().listCaseCible = CombatGrid.GetComponent<GenerateurGrid> ().TrouverGameObjectCase (lstVect);
					}
					// Flèche gauche
					if (Input.GetAxis ("Horizontal") < 0) {
						tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (false);
						List<Vector2> lstVect = curseurCaseActuel.GetComponent<GestionCases> ().ListVectorAttaqueLigne (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueSpeciale,"gauche");
						tourDuJoueurAJouer.GetComponent<GestionPersonnage>().listCaseCible = CombatGrid.GetComponent<GenerateurGrid> ().TrouverGameObjectCase (lstVect);
					}
					m_isAxisUtiliseX = true;
				}
			} else {
				m_isAxisUtiliseX = false;
			}
			if (Input.GetAxis ("Vertical") != 0) {
				if (m_isAxisUtiliseY == false) {
					// Flèche haut
					if (Input.GetAxis ("Vertical") > 0) {
						tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (false);
						List<Vector2> lstVect = curseurCaseActuel.GetComponent<GestionCases> ().ListVectorAttaqueLigne (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueSpeciale,"haut");
						tourDuJoueurAJouer.GetComponent<GestionPersonnage>().listCaseCible = CombatGrid.GetComponent<GenerateurGrid> ().TrouverGameObjectCase (lstVect);
					}
					// Flèche bas
					if (Input.GetAxis ("Vertical") < 0) {
						tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (false);
						List<Vector2> lstVect = curseurCaseActuel.GetComponent<GestionCases> ().ListVectorAttaqueLigne (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueSpeciale,"bas");
						tourDuJoueurAJouer.GetComponent<GestionPersonnage>().listCaseCible = CombatGrid.GetComponent<GenerateurGrid> ().TrouverGameObjectCase (lstVect);
					}
					m_isAxisUtiliseY = true;
				}
			} else {
				m_isAxisUtiliseY = false;
			}
			tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (true);
		}
	}

	public void DeplacerCurseur(GameObject caseVoisine){
		if (caseVoisine) {
			transform.position = caseVoisine.transform.position;
			curseurCaseActuel = caseVoisine;
		}
	}

}
