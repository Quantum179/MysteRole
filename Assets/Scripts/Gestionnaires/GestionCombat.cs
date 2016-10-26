using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mysterole;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GestionCombat : MonoBehaviour {

	public GameObject curseurPrefab;
	public GameObject buttonPrefab;
	public GameObject menuCombat;
	public GameObject panelOption;
	public GameObject panelCompetences;
	private string navigationPos;
	private List<GameObject> listPersonnage = new List<GameObject>();
	private List<GameObject> listJoueur = new List<GameObject>();
	private List<GameObject> listEnemi = new List<GameObject> ();
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
		curseurCaseActuel.GetComponent<GestionCases> ().changeeMouvement (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.MOV,true);
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
			if (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineDroite) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineDroite.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineDroite;
			}
		}
		// Flèche gauche
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			if (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineGauche) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineGauche.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineGauche;
			}
		}
		// Flèche haut
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineHaut) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineHaut.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineHaut;
			}
		}
		// Flèche bas
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if (curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineBas) {
				curseur.transform.position = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineBas.transform.position;
				curseurCaseActuel = curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineBas;
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "Mouvement") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (curseurCaseActuel.GetComponent<GestionCases> ().EstOccupee || !curseurCaseActuel.GetComponent<GestionCases> ().EstMouvement) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				}
			}
			if (Input.GetKeyUp (KeyCode.Space) && curseur.GetComponent<CurseurAnim> ().ClickAccepte) {
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases>().changeeMouvement (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.MOV, false);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseDestination = curseurCaseActuel;
				StartCoroutine (AttendFinMouvement());
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "Attaque") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (!curseurCaseActuel.GetComponent<GestionCases> ().EstOccupee||
					!curseurCaseActuel.GetComponent<GestionCases> ().EstAttack ||
					!listEnemi.Contains(curseurCaseActuel.GetComponent<GestionCases>().Personnage)){
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
				curseurCaseActuel.GetComponent<GestionCases> ().Personnage.GetComponent<GestionPersonnage> ().ReduirePV (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.PUI);
				Destroy (curseur);
				StartCoroutine (AttendFinAttaque());
			}
		}
	}

	// Use this for initialization
	void Start () {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Combat"));
        menuCombat.SetActive (false);
		EventSystem = GameObject.Find ("EventSystem");
		// Aller chercher les données nécéssaire de la Grid générée.
		CombatGrid = GameObject.Find ("CombatGrid");
		int largeurGrid = CombatGrid.GetComponent<GenerateurGrid> ().LargeurGrid;
		int hauteurGrid = CombatGrid.GetComponent<GenerateurGrid> ().HauteurGrid;

		// Instantier les joueur avec un foreach ensuite les monstre avec un autre foreach et les mettre dans une list de joueur sur le plateau présent.
		DonneesJeu.Equipe.Membres.ForEach (delegate(Personnage obj) {
			GameObject j;
			j = Instantiate (Resources.Load ("Prefab/prefabPersonnage")) as GameObject;
			j.GetComponent<GestionPersonnage>().monPersonnage = obj;
			j.GetComponent<PersonnageMouvement>().AssocierCaseDepartRand(1,hauteurGrid,largeurGrid-2,largeurGrid);
			listPersonnage.Add (j);
			listJoueur.Add(j);
		});
		DonneesJeu.Adversaires.Membres.ForEach (delegate(Personnage obj) {
			GameObject j;
			j = Instantiate (Resources.Load ("Prefab/prefabPersonnage")) as GameObject;
			j.GetComponent<GestionPersonnage> ().monPersonnage = obj;
			j.GetComponent<PersonnageMouvement>().AssocierCaseDepartRand(1,hauteurGrid,1,3);
			j.GetComponent<PersonnageMouvement>().SetDirection(1);
			listPersonnage.Add (j);
			listEnemi.Add(j);
		});
	}
	
	// Update is called once per frame
	void Update () {
		// On vérifie s'il y a des personnages qui ont été détruit dans la list de personnage
		listPersonnage.RemoveAll(perso => perso == null);
		listJoueur.RemoveAll (perso => perso == null);
		listEnemi.RemoveAll (perso => perso == null);

		if (listPersonnage.Count == 0) {
			GestScene.ProchaineScene ("Menu_Principal");
		}
		if (curseur) {
			bougeCurseur ();
		}
		if (prochainJoueur >= listPersonnage.Count) {
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

			// Tour à la prochaine personne
			tourDuJoueurAJouer = listPersonnage [prochainJoueur];


			// On s'assure que le curseur est détruit
			Destroy (curseur);


			// Le menu est activé si le joueur est jouable.
			if (listJoueur.Contains (tourDuJoueurAJouer)) {
				navigationPos = "menuCombat";
				menuCombat.SetActive (true);
				// TODO : arranger le premier bouton hightlighter
				EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (panelOption.transform.GetChild (0).gameObject);
			} else {
				//obtenirCible();
				if (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.Role.Nom == "Squelette") {
					listEnemi.ForEach (delegate(GameObject obj) {
						if (obj.GetComponent<GestionPersonnage> ().monPersonnage.Role.Nom == "Orque") {
							tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().CiblePersonnage = obj.GetComponent<GestionPersonnage> ().CiblePersonnage;
						} else {
							tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().CiblePersonnage = listJoueur [0];
						}
					});
				} else {
					tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().CiblePersonnage = listJoueur [0];
				}

				// Variable répéter de GetComponent
				bool attaqueIA = false;
				GameObject cible = tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().CiblePersonnage;
				Personnage tourIA = tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage;
				GameObject tempCase = tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel;
                GameObject tempCaseInit = tempCase;
				int distanceDeCibleX =cible.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().PosX - tempCase.GetComponent<GestionCases> ().PosX;
				int distanceDeCibleY =cible.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().PosY - tempCase.GetComponent<GestionCases> ().PosY;
				int distanceObjectifX = (int)Mathf.Sqrt(Mathf.Pow(distanceDeCibleX,2)) - tourIA.AttaqueBase.Cible.DistanceMax;
				int distanceObjectifY = (int)Mathf.Sqrt(Mathf.Pow(distanceDeCibleY,2)) - tourIA.AttaqueBase.Cible.DistanceMax;

				if (tourIA.PV < (tourIA.MPV * 25) / 100) {
					for (int i = 0; i < tourIA.MOV; i++) {
						bool rienChange = false;
						while (!rienChange) {
							if (Random.Range (0, 2) == 0 && distanceDeCibleX != 0) {
								if (distanceDeCibleX > 0) {
									if (tempCase.GetComponent<GestionCases> ().CaseVoisineDroite && !tempCase.GetComponent<GestionCases> ().CaseVoisineDroite.GetComponent<GestionCases> ().EstOccupee) {
										tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineGauche;
										distanceDeCibleX--;
										rienChange = true;
									}
								} else if (distanceDeCibleX < 0) {
									if (tempCase.GetComponent<GestionCases> ().CaseVoisineGauche && !tempCase.GetComponent<GestionCases> ().CaseVoisineGauche.GetComponent<GestionCases> ().EstOccupee) {
										tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineDroite;
										distanceDeCibleX++;
										rienChange = true;
									}
								}
							} else if (distanceDeCibleY != 0) {
								if (distanceDeCibleY < 0) {
									if (tempCase.GetComponent<GestionCases> ().CaseVoisineHaut && !tempCase.GetComponent<GestionCases> ().CaseVoisineHaut.GetComponent<GestionCases> ().EstOccupee) {
										tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineBas;
										distanceDeCibleY++;
										rienChange = true;
									}
								} else if (distanceDeCibleY > 0) {
									if (tempCase.GetComponent<GestionCases> ().CaseVoisineBas && !tempCase.GetComponent<GestionCases> ().CaseVoisineBas.GetComponent<GestionCases> ().EstOccupee) {
										tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineHaut;
										distanceDeCibleY--;
										rienChange = true;
									}
								}
							}
						}
					}
				}else{	
					if (distanceObjectifX > 0 || distanceObjectifY > 0 || distanceObjectifX == distanceObjectifY) {
						if (distanceObjectifX >= tourIA.MOV || distanceObjectifY >= tourIA.MOV) {
							for (int i = 0; i < tourIA.MOV; i++) {
								bool rienChange = false;
								while (!rienChange) {
									if (Random.Range (0, 2) == 0 && distanceDeCibleX != 0) {
										if (distanceDeCibleX > 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineDroite && !tempCase.GetComponent<GestionCases> ().CaseVoisineDroite.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineDroite;
												distanceDeCibleX--;
												rienChange = true;
											}
										} else if (distanceDeCibleX < 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineGauche && !tempCase.GetComponent<GestionCases> ().CaseVoisineGauche.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineGauche;
												distanceDeCibleX++;
												rienChange = true;
											}
										}
									} else if (distanceDeCibleY != 0) {
										if (distanceDeCibleY < 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineHaut && !tempCase.GetComponent<GestionCases> ().CaseVoisineHaut.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineHaut;
												distanceDeCibleY++;
												rienChange = true;
											}
										} else if (distanceDeCibleY > 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineBas && !tempCase.GetComponent<GestionCases> ().CaseVoisineBas.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineBas;
												distanceDeCibleY--;
												rienChange = true;
											}
										}
									}
								}
							}
						} else {
							for (int i = 0; i < Mathf.Max (Mathf.Abs (distanceDeCibleX), Mathf.Abs (distanceDeCibleY)); i++) {
								bool rienChange = false;
								while (!rienChange) {
									if (Random.Range (0, 2) == 0 && distanceDeCibleX != 0) {
										if (distanceDeCibleX > 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineDroite && !tempCase.GetComponent<GestionCases> ().CaseVoisineDroite.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineDroite;
												distanceDeCibleX--;
												rienChange = true;
											}
										} else if (distanceDeCibleX < 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineGauche && !tempCase.GetComponent<GestionCases> ().CaseVoisineGauche.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineGauche;
												distanceDeCibleX++;
												rienChange = true;
											}
										}
									} else if (distanceDeCibleY != 0) {
										if (distanceDeCibleY < 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineHaut && !tempCase.GetComponent<GestionCases> ().CaseVoisineHaut.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineHaut;
												distanceDeCibleY++;
												rienChange = true;
											}
										} else if (distanceDeCibleY > 0) {
											if (tempCase.GetComponent<GestionCases> ().CaseVoisineBas && !tempCase.GetComponent<GestionCases> ().CaseVoisineBas.GetComponent<GestionCases> ().EstOccupee) {
												tempCase = tempCase.GetComponent<GestionCases> ().CaseVoisineBas;
												distanceDeCibleY--;
												rienChange = true;
											}
										}
									}
								}
							}
						}
					} else {
						attaqueIA = true;
					}
				}

				if (attaqueIA) {
					attaqueIA = false;
					tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().faitAttaque (cible);
					cible.GetComponent<GestionPersonnage> ().ReduirePV (tourIA.PUI);
					StartCoroutine (AttendFinAttaque ());
				} else {
					if (tempCaseInit == tempCase) {
						prochainJoueur++;
						tourTermine = true;
					} else {
						tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseDestination = tempCase;
						StartCoroutine (AttendFinMouvement ());
					}
				}
			}
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
		yield return new WaitForSeconds(1);
		prochainJoueur++;
		tourTermine = true;
	}
}