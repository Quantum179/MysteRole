using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mysterole;
using System.Collections.Generic;

public class GestionCombat : MonoBehaviour {

	public GameObject curseurPrefab;
	public GameObject buttonPrefab;
	public GameObject menuCombat;
	public GameObject panelOption;
	public GameObject panelCompetences;
	private string navigationPos;
	private List<GameObject> listPersonnage = new List<GameObject>();
	private List<GameObject> listJoueur = new List<GameObject>();
	private GameObject curseur;
	private GameObject curseurCaseActuel;
	private List<GameObject> listAttaque = new List<GameObject> ();
	private GameObject CombatGrid;
	private GameObject EventSystem;
	private bool tourTermine = true;
	private GameObject tourDuJoueurAJouer;
	private int prochainJoueur = 0;

	public void Bouger_Click(){
		menuCombat.SetActive (false);
		curseurCaseActuel = tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel;
		curseur = Instantiate(curseurPrefab,tourDuJoueurAJouer.transform.position,Quaternion.identity) as GameObject;
		curseur.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		curseur.GetComponent<CurseurAnim>().option = "Mouvement";
		curseurCaseActuel.GetComponent<GestionCases> ().changeeMouvement (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.MOV+5,true);
	}

	public void Competence_Click(){
		navigationPos = "panelCompetence";
		panelOption.SetActive (false);
		panelCompetences.SetActive (true);
		GameObject b1;
		b1 = Instantiate (buttonPrefab, panelCompetences.transform) as GameObject;
		b1.GetComponentInChildren<Text>().text = tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Nom;
		b1.GetComponent<Button>().onClick.AddListener(()=>Attaquer_Click());
		listAttaque.Add (b1);
		EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (panelCompetences.transform.GetChild (0).gameObject);
	}

	public void Fuir_Click(){
		GestScene.ProchaineScene ("World");
	}

	public void Attaquer_Click(){
		navigationPos = "optionAttaque";
		menuCombat.SetActive (false);
		curseurCaseActuel = tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel;
		curseur = Instantiate (curseurPrefab, tourDuJoueurAJouer.transform.position, Quaternion.identity) as GameObject;
		curseur.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		curseur.GetComponent<CurseurAnim> ().option = "Attaque";
		curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase,true,0);
	}

	private void bougeCurseur(){
		// Flèche droite
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			if (curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineDroite) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineDroite.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineDroite;
			}
		}
		// Flèche gauche
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if (curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineGauche) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineGauche.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineGauche;
			}
		}
		// Flèche haut
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if (curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineHaut) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineHaut.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineHaut;
			}
		}
		// Flèche bas
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if (curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineBas) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineBas.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().caseVoisineBas;
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "Mouvement") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (curseurCaseActuel.GetComponent<GestionCases> ().estOccupee || !curseurCaseActuel.GetComponent<GestionCases> ().estMouvement) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				}
			}
			if (Input.GetKeyUp (KeyCode.Space) && curseur.GetComponent<CurseurAnim> ().ClickAccepte) {
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases>().changeeMouvement (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.MOV+5, false);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseDestination = curseurCaseActuel;
				Destroy (curseur);
				StartCoroutine (AttendFinMouvement());
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "Attaque") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (!curseurCaseActuel.GetComponent<GestionCases> ().estOccupee || !curseurCaseActuel.GetComponent<GestionCases> ().estAttack) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				}
			}
			if (Input.GetKeyUp (KeyCode.Space) && curseur.GetComponent<CurseurAnim> ().ClickAccepte) {
				panelOption.SetActive (true);
				panelCompetences.SetActive (false);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().changeeAttaque (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase, false, 0);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().faitAttaque (curseurCaseActuel);
				Destroy (curseur);
				StartCoroutine (AttendFinAttaque());
			}
		}
	}

	// Use this for initialization
	void Start () {
		menuCombat.SetActive (false);
		EventSystem = GameObject.Find ("EventSystem");
		// Aller chercher les données nécéssaire de la Grid générée.
		CombatGrid = GameObject.Find ("CombatGrid");
		int hauteurGrid = CombatGrid.GetComponent<GenerateurGrid> ().HauteurGrid;
		int largeurGrid = CombatGrid.GetComponent<GenerateurGrid> ().LargeurGrid;
		// Instantier les joueur avec un foreach ensuite les monstre avec un autre foreach et les mettre dans une list de joueur sur le plateau présent.
		DonneesJeu.Equipe.Membres.ForEach (delegate(Personnage obj) {
			GameObject j;
			j = Instantiate (Resources.Load ("Prefab/prefabPersonnage")) as GameObject;
			j.GetComponent<GestionPersonnage>().monPersonnage = obj;
			j.GetComponent<PersonnageMouvement>().AssocierCaseDepartRand(1,hauteurGrid,largeurGrid-1,largeurGrid);
			listPersonnage.Add (j);
			listJoueur.Add(j);
		});
		DonneesJeu.EquipeMonstre[0].Membres.ForEach (delegate(Personnage obj) {
			GameObject j;
			j = Instantiate (Resources.Load ("Prefab/prefabPersonnage")) as GameObject;
			j.GetComponent<GestionPersonnage> ().monPersonnage = obj;
			j.GetComponent<PersonnageMouvement>().AssocierCaseDepartRand(1,hauteurGrid,1,3);
			j.GetComponent<PersonnageMouvement>().SetDirection(1);
			listPersonnage.Add (j);
		});
	}
	
	// Update is called once per frame
	void Update () {
		if (curseur) {
			bougeCurseur ();
		}
		if (prochainJoueur == listPersonnage.Count) {
			prochainJoueur = 0;
			// On réorganise la List en ordre de Vitesse des personnages au début du tour et le .Reverse le met en ordre décroissant (VIT plus haut en premier)
			listPersonnage.Sort (((x, y) => x.GetComponent<GestionPersonnage> ().monPersonnage.VIT.CompareTo (y.GetComponent<GestionPersonnage> ().monPersonnage.VIT)));
			listPersonnage.Reverse ();
		}
		if (tourTermine) {
			// On remet le tourTermine a false puisqu'on commence un nouveau tour en avancant dans la liste pour le tour du prochain joueur
			// Et la liste D'attaque dans le menu du personnage est détruite
			tourTermine = false;
			listAttaque.ForEach (delegate(GameObject obj) {Destroy(obj);});

			tourDuJoueurAJouer = listPersonnage [prochainJoueur];

			// On s'assure que le curseur est détruit
			Destroy (curseur);

			// Le menu est activé si le joueur est jouable.
			navigationPos = "menuCombat";
			menuCombat.SetActive (true);
			// TODO : arranger le premier bouton hightlighter
			EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (panelOption.transform.GetChild (0).gameObject);
		}


		// Touche de navigation avec ESC pour revenir en arrière
		if (Input.GetKeyUp (KeyCode.Escape)) {
			switch (navigationPos) {
			case "panelCompetence":
				panelCompetences.SetActive (false);
				panelOption.SetActive (true);
				navigationPos = "menuCombat";
				break;
			case "menuCombat":
				menuCombat.SetActive (false);
				curseurCaseActuel = tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel;
				curseur = Instantiate (curseurPrefab, tourDuJoueurAJouer.transform.position, Quaternion.identity) as GameObject;
				curseur.GetComponent<SpriteRenderer> ().sortingOrder = 1;
				curseur.GetComponent<CurseurAnim> ().option = "infoPersonnage";
				break;
			}
		}
	}

	private IEnumerator AttendFinMouvement(){
		yield return new WaitUntil (() => tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().GetFiniMouvement() == true);
		prochainJoueur++;
		tourTermine = true;
	}

	private IEnumerator AttendFinAttaque(){
		yield return new WaitUntil (() => tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().GetFiniMouvement () == true);
		prochainJoueur++;
		tourTermine = true;
	}
}
