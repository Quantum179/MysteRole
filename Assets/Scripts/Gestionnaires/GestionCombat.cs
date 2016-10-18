using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GestionCombat : MonoBehaviour {

	public GameObject curseurPrefab;
	public GameObject Menu;
	public GameObject personnagePrefab;
	private GameObject joueur1;
	private int personnageMove = 2;
	private GameObject curseur;
	private GameObject caseActuel;
	private GameObject EventSystem;

	public void instantierCurseur_Click(GameObject menu){
		menu.SetActive (false);
		caseActuel = joueur1.GetComponent<PersonnageMouvement> ().caseActuel;
		curseur = Instantiate(curseurPrefab,joueur1.transform.position,Quaternion.identity) as GameObject;
		curseur.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		curseur.GetComponent<CurseurAnim>().option = "Mouvement";
		caseActuel.GetComponent<GestionCases> ().changeeMouvement (personnageMove,true);
	}

	public void Fuir_Click(){
		GestScene.ProchaineScene ("World");
	}

	private void bougeCurseur(){
		// Flèche droite
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineDroite) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineDroite.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineDroite;
			}
		}
		// Flèche gauche
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineGauche) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineGauche.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineGauche;
			}
		}
		// Flèche haut
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineHaut) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineHaut.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineHaut;
			}
		}
		// Flèche bas
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if (caseActuel.GetComponent<GestionCases> ().caseVoisineBas) {
				curseur.transform.position = caseActuel.GetComponent<GestionCases> ().caseVoisineBas.transform.position;
				caseActuel = caseActuel.GetComponent<GestionCases> ().caseVoisineBas;
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "Mouvement") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (caseActuel.GetComponent<GestionCases> ().estOccupee || !caseActuel.GetComponent<GestionCases> ().estMouvement) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				}
			}
			if (Input.GetKeyUp (KeyCode.Space) && curseur.GetComponent<CurseurAnim> ().ClickAccepte) {
				joueur1.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases>().changeeMouvement (personnageMove, false);
				joueur1.GetComponent<PersonnageMouvement> ().caseDestination = caseActuel;
				Destroy (curseur);
			}
		}
	}

	// Use this for initialization
	void Start () {
		joueur1 = Instantiate (personnagePrefab) as GameObject;
		joueur1.GetComponent<PersonnageMouvement> ().caseDepart = GameObject.Find ("CombatGrid/Colonne2/case3");
		EventSystem = GameObject.Find ("EventSystem");
		Cursor.visible = false;
		Menu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (curseur) {
			bougeCurseur ();
		}
		if (Input.GetKey(KeyCode.M)) {
			Destroy (curseur);
			Menu.SetActive (true);
			EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (Menu.transform.FindChild("Panel").GetChild(0).gameObject);
		}
	}
}