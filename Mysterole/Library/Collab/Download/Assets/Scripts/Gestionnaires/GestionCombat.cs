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
	public GameObject status;
	public GameObject panelOption;
	public GameObject panelCompetences;
	public GameObject panelInfo;
	public GameObject panelItems;
	public GameObject panelScrollView;
	public GameObject panelFinCombat;
	public GameObject panelAffichageExp;
	public GameObject joueurExpPrefab;
	public GameObject objetPrefab;

	public List<GameObject> listPersonnage = new List<GameObject>();
	private List<GameObject> listJoueur = new List<GameObject>();
	private List<GameObject> listEnemi = new List<GameObject> ();
	private List<GameObject> listAttaque = new List<GameObject> ();
	private Objets.Consommable item;
	private GameObject curseur;
	private GameObject CombatGrid;
	private GameObject EventSystem;

	private string navigationPos;
	private bool tourTermine = true;
	public GameObject tourDuJoueurAJouer{ get; set; }
	private GameObject lastSelectedGameObject;
	private int prochainJoueur = 100;
	private bool AnnulerAppuye;
	private bool AccepterAppuye;
	private bool CombatFini = false;
	private bool Affiche = false;
	private float tick;
	private int ExperienceGagnee;
	private Dictionary<int,int> listObjetGagnee = new Dictionary<int,int>();


	public void Bouger_Click(){
		ActiverCurseur ("Mouvement");
		curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeMouvement (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.MOV,true);
	}

	public void Competence_Click(){
		navigationPos = "panelCompetence";
		panelOption.SetActive (false);
		panelCompetences.SetActive (true);
		GameObject b1;
		GameObject b2;
		b1 = Instantiate (buttonPrefab,panelCompetences.transform,false) as GameObject;
		b2 = Instantiate (buttonPrefab, panelCompetences.transform, false) as GameObject;
		b1.GetComponentInChildren<Text>().text = tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Nom;
		b2.GetComponentInChildren<Text> ().text = tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale.Nom;
		b1.GetComponent<Button>().onClick.AddListener(()=>Attaquer_Click());
		b2.GetComponent<Button> ().onClick.AddListener (() => AttaqueSpecial_Click ());
		b1.transform.Translate (new Vector2 (0, 20));
		b2.transform.Translate(new Vector2 (0, -30));
		Navigation nav = new Navigation ();
		nav.mode = Navigation.Mode.Explicit;
		nav.selectOnDown = b2.GetComponent<Button> ();
		b1.GetComponent<Button> ().navigation = nav;
		nav.selectOnUp = b1.GetComponent<Button> ();
		b2.GetComponent<Button> ().navigation = nav;
		listAttaque.Add (b1);
		listAttaque.Add (b2);
		EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (panelCompetences.transform.GetChild (0).gameObject);
		if (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.PC == 0) {
			b2.GetComponent<Button> ().interactable = false;
			b2.GetComponentInChildren<Text> ().color = Color.grey;
		}
	}

	public void Item_Click(){
		navigationPos = "panelItem";
		panelOption.SetActive(false);
		panelItems.SetActive (true);
		List<GameObject> lstItem = new List<GameObject> ();
		Dictionary<int, int>.Enumerator e = DonneesJeu.Equipe.Inventaire.Liste.GetEnumerator ();
		while(e.MoveNext()) {
			GameObject item = null;
			if (Objets.TrouverObjet(e.Current.Key).GetType ().Equals (typeof(Objets.Consommable))) {
				Objets.Consommable Cons = Objets.TrouverObjet (e.Current.Key) as Objets.Consommable;
				item = Instantiate (buttonPrefab) as GameObject;
				item.transform.parent = panelScrollView.transform;
				item.GetComponentInChildren<Text> ().text = Cons.Nom + " x" + e.Current.Value;
				item.GetComponent<Button> ().onClick.AddListener (() => UtiliseItem_Click (Cons));
				item.AddComponent<LayoutElement> ();
				item.GetComponent<LayoutElement> ().flexibleWidth = 1;
				item.GetComponent<LayoutElement> ().preferredHeight = 30;
			}
			lstItem.Add (item);
		}

		for (int i = 0; i < lstItem.Count; i++) {
			Navigation nav = new Navigation ();
			nav.mode = Navigation.Mode.Explicit;
			if (i > 0) {
				nav.selectOnUp = lstItem[i-1].GetComponent<Button>();
			}
			if (i < lstItem.Count-1) {
				nav.selectOnDown = lstItem [i + 1].GetComponent<Button>();
			}
			lstItem [i].GetComponent<Button> ().navigation = nav;
		}
		EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (lstItem[0].gameObject);
	}

	public void UtiliseItem_Click(Objets.Consommable itemUtilisee){
		AccepterAppuye = true;
		item = itemUtilisee;
		ActiverCurseur ("panelItem");
	}

	public void Fuir_Click(){
		GestScene.ProchaineSceneTransition("Monde");
	}

	public void Attaquer_Click(){
		var perso = tourDuJoueurAJouer.GetComponent<GestionPersonnage> ();
		ActiverCurseur ("Attaque");
		curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (perso.monPersonnage.AttaqueBase.Cible.DistanceMin,perso.monPersonnage.AttaqueBase.Cible.DistanceMax,true,0);
	}

	public void AttaqueSpecial_Click(){
		AccepterAppuye = true;
		Competence atqSpecial = tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale;
		if (atqSpecial.Cible.Zone is Mysterole.ZoneLigne) {
			ActiverCurseur ("AttaqueSpecialLigne");
			List<Vector2> lstVect = curseur.GetComponent<CurseurAnim> ().curseurCaseActuel.GetComponent<GestionCases> ().ListVectorAttaqueLigne (atqSpecial, "gauche");
			tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().listCaseCible = CombatGrid.GetComponent<GenerateurGrid> ().TrouverGameObjectCase (lstVect);
			tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (true);
		} else{
			ActiverCurseur ("AttaqueSpecialZone");
			tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().changeeAttaquePotentiel (tourDuJoueurAJouer,atqSpecial.Cible.DistanceMin,atqSpecial.Cible.DistanceMax,true, 0);
		}
	}

	private void bougeCurseur(){
		// Option du Curseur selon le menu.
		if (curseur.GetComponent<CurseurAnim> ().option == "Mouvement") {
			if (Input.GetButtonUp("Annuler")) {
				AnnulerAppuye = true;
				menuCombat.SetActive (true);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases>().changeeMouvement (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.MOV, false);
				if (curseur) {
					Destroy (curseur);
				}
			}
			if (Input.GetButtonDown("Accepter")) {
				if (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().EstOccupee || !curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().EstMouvement) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				}
			}
			if (Input.GetButtonUp("Accepter") && curseur.GetComponent<CurseurAnim> ().ClickAccepte) {
				Destroy (curseur);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases>().changeeMouvement (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.MOV, false);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseDestination = curseur.GetComponent<CurseurAnim>().curseurCaseActuel;
				StartCoroutine (AttendFinMouvement());
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "Attaque") {
			if (Input.GetButtonUp("Annuler")) {
				menuCombat.SetActive (true);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().changeeAttaque (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, false, 0);
				if (curseur) {
					Destroy (curseur);
				}
			}
			if (Input.GetButtonDown("Accepter")) {
				if (!curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().EstOccupee||
					!curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().EstAttack ||
					!listEnemi.Contains(curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases>().Personnage)){
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				}
			}
			if (Input.GetButtonUp("Accepter") && curseur.GetComponent<CurseurAnim> ().ClickAccepte) {
				Destroy (curseur);
				panelOption.SetActive (true);
				panelCompetences.SetActive (false);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().changeeAttaque (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, false, 0);
				if (tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().modeleAnimator == "ArcherAC" || tourDuJoueurAJouer.GetComponent<PersonnageMouvement>().modeleAnimator == "PrêtreAC") {
					tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().faitAttaqueDistance (curseur);	
				} else {
					tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().faitAttaque (curseur.GetComponent<CurseurAnim> ().curseurCaseActuel);
				}
				curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().Personnage.GetComponent<GestionPersonnage> ().ReduirePV (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.PUI);
				StartCoroutine (AttendFinAttaque());
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "AttaqueSpecialLigne") {
			if (Input.GetButtonUp ("Annuler")) {
				menuCombat.SetActive (true);
				tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (false);
				if (curseur) {
					Destroy (curseur);
				}
			}
			if (Input.GetButtonDown ("Accepter")) {
				List<GameObject> lst = VerifierListEnemi ();
				if (lst.Count != 0) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				}
			}
			if(!AccepterAppuye && Input.GetButtonUp("Accepter") && curseur.GetComponent<CurseurAnim>().ClickAccepte){
				Destroy (curseur);
				panelOption.SetActive (true);
				panelCompetences.SetActive (false);

				List<GameObject> lst = VerifierListEnemi ();
				foreach (GameObject goPerso in lst) {
					goPerso.GetComponent<GestionPersonnage> ().ReduirePV (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.PUI);
				}
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().faitAttaqueSpecial ();
				tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (false);
				tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.PC -= 1;
				StartCoroutine (AttendFinAttaque ());
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "AttaqueSpecialZone") {
			if (Input.GetButtonUp("Annuler")) {
				menuCombat.SetActive (true);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().changeeAttaquePotentiel (tourDuJoueurAJouer,tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale.Cible.DistanceMin,tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueSpeciale.Cible.DistanceMax, false, 0);
				curseur.GetComponent<CurseurAnim> ().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (0,tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale.Cible.Zone.Largeur, false, 0);
				if (curseur) {
					Destroy (curseur);
				}
			}
			if(Input.GetButtonDown("Accepter")){
				List<GameObject> lst = VerifierListEnemi ();
				if (lst != null && lst.Count != 0 && curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases>().EstPossibleAttack) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				}
			}
			if(!AccepterAppuye && Input.GetButtonUp("Accepter") && curseur.GetComponent<CurseurAnim>().ClickAccepte){
				if (tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().modeleAnimator == "MageAC") {
					tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().faitAttaqueZone (curseur);
				} else {
					tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().faitArcherSpecial (curseur);
				}
				tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.PC -= 1;
				StartCoroutine (AttendFinSpecialAttaque ());
				tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().ChangeeAttaque (false);
				tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().changeeAttaquePotentiel (tourDuJoueurAJouer,tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale.Cible.DistanceMin,tourDuJoueurAJouer.GetComponent<GestionPersonnage>().monPersonnage.AttaqueSpeciale.Cible.DistanceMax, false, 0);
				Destroy (curseur);
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "infoPersonnage") {
			if (Input.GetButtonUp("Annuler")) {
				AnnulerAppuye=true;
				menuCombat.SetActive (true);
				if (curseur) {
					Destroy (curseur);
				}
			}
			if (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().Personnage){
				var perso = curseur.GetComponent<CurseurAnim> ().curseurCaseActuel.GetComponent<GestionCases> ().Personnage;
				if (listPersonnage.Contains (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().Personnage)) {
					curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (perso.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,perso.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, true, 0);
					AfficherInfo ();

				}
				if (Input.GetButtonUp("Annuler")) {
					curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (perso.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,perso.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, false, 0);
					panelInfo.gameObject.SetActive (false);
				}
				if (Input.GetAxis("Vertical")>0) {
					if (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineHaut) {
						curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (perso.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,perso.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, false, 0);
						panelInfo.gameObject.SetActive (false);
						}
					}
				if (Input.GetAxis("Vertical")<0) {
					if (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineBas) {
						curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (perso.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,perso.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, false, 0);
						panelInfo.gameObject.SetActive (false);
						}
					}
				if (Input.GetAxis("Horizontal")<0) {
					if (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineGauche) {
						curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (perso.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,perso.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, false, 0);
						panelInfo.gameObject.SetActive (false);
						}
					}
				if (Input.GetAxis("Horizontal")>0) {
					if (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().CaseVoisineDroite) {
						curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases> ().changeeAttaque (perso.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueBase.Cible.DistanceMin,perso.GetComponent<GestionPersonnage>().monPersonnage.AttaqueBase.Cible.DistanceMax, false, 0);
						panelInfo.gameObject.SetActive (false);
						}
				}
			}
		}
		if (curseur.GetComponent<CurseurAnim> ().option == "panelItem") {
			if (Input.GetButtonUp("Annuler")) {
				AnnulerAppuye=true;
				menuCombat.SetActive (true);
				if (curseur) {
					Destroy (curseur);
				}
			}
			if(Input.GetButtonDown("Accepter")){
				if (curseur.GetComponent<CurseurAnim>().curseurCaseActuel.GetComponent<GestionCases>().Personnage) {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = true;
				} else {
					curseur.GetComponent<CurseurAnim> ().ClickAccepte = false;
				}
			}
			if (!AccepterAppuye && Input.GetButtonUp ("Accepter") && curseur.GetComponent<CurseurAnim> ().ClickAccepte) {
				List<Personnage> cibles = new List<Personnage> ();
				cibles.Add (curseur.GetComponent<CurseurAnim> ().curseurCaseActuel.GetComponent<GestionCases> ().Personnage.GetComponent<GestionPersonnage> ().monPersonnage);
				item.Utiliser (cibles);
				GameObject effetSpecial;
				effetSpecial = Instantiate (Resources.Load ("Prefab/Attaques/item")) as GameObject;
				effetSpecial.transform.localPosition = curseur.GetComponent<CurseurAnim>().curseurCaseActuel.transform.localPosition;
				DonneesJeu.Equipe.Inventaire.Retirer (item.ID, 1);
				StartCoroutine (AttendFinAttaque ());
				foreach (Transform enfant in panelScrollView.transform) {
					Destroy (enfant.gameObject);
				}
				panelItems.SetActive (false);
				Destroy (curseur);
			}
		}
	}

	// Use this for initialization
	void Start () {
		RenderSettings.fog = true;
		GetComponent<AudioSource> ().clip = Resources.Load ("Music/foretCombat") as AudioClip;
		GetComponent<AudioSource> ().Play ();

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
			j.GetComponent<PersonnageMouvement>().AssocierCaseDepartRand(1,hauteurGrid,3,largeurGrid-5);
			listPersonnage.Add (j);
			listJoueur.Add(j);
		});
		DonneesJeu.Adversaires.Membres.ForEach (delegate(Personnage obj) {
			GameObject j;
			j = Instantiate (Resources.Load ("Prefab/prefabAI")) as GameObject;
			j.GetComponent<GestionPersonnage> ().monPersonnage = obj;
			j.GetComponent<PersonnageMouvement>().AssocierCaseDepartRand(1,hauteurGrid,1,3);
			j.GetComponent<PersonnageMouvement>().SetDirection(1);
			listPersonnage.Add (j);
			listEnemi.Add(j);
			ExperienceGagnee += (int)(obj.NIV/listJoueur[0].GetComponent<GestionPersonnage>().monPersonnage.NIV)*5;
		});
		int i=0;
		listJoueur.ForEach (delegate(GameObject obj) {
			GameObject statuJoueur;
			statuJoueur = Instantiate (Resources.Load ("Prefab/StatuPersonnage"),status.transform,false) as GameObject;
			statuJoueur.GetComponent<StatuPersonnage>().infoPerso = obj.GetComponent<GestionPersonnage>().monPersonnage;
			statuJoueur.transform.localPosition = new Vector2(0,i);
			i-=30;
		});

		DonneesJeu.Adversaires.Inventaire.Ajouter (0, Random.Range(0,2));
		DonneesJeu.Adversaires.Inventaire.Ajouter (1, Random.Range(0,4));
		listObjetGagnee = DonneesJeu.Adversaires.Inventaire.Liste;
	}
	
	// Update is called once per frame
	void Update () {
		// Teste si le bouton escape a ete appuyer
		AccepterAppuye = false;
		AnnulerAppuye = false;
		// On vérifie s'il y a des personnages qui ont été détruit dans la list de personnage
		listPersonnage.RemoveAll(perso => perso == null);
		listJoueur.RemoveAll (perso => perso == null);
		listEnemi.RemoveAll (perso => perso == null);

		if (CombatFini) {
			if (listEnemi.Count == 0 && !Affiche) {
				Affiche = true;
				panelFinCombat.SetActive (true);
				panelFinCombat.GetComponentInChildren<Text> ().text = "Victoire";
				panelFinCombat.GetComponentInChildren<Text> ().color = Color.green;
				DonneesJeu.Equipe.FinCombat ();
				StartCoroutine (AttendFinCombat ());
			}
			if (listJoueur.Count == 0 && !Affiche) {
				Affiche = true;
				panelFinCombat.SetActive (true);
				panelFinCombat.GetComponentInChildren<Text> ().text = "Défaite";
				panelFinCombat.GetComponentInChildren<Text> ().color = Color.red;
				DonneesJeu.Vider ();
				StartCoroutine (AttendFinCombat ());
			}
		} else {
			if (listEnemi.Count == 0 || listJoueur.Count == 0) {CombatFini = true;}
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
				listAttaque.ForEach (delegate(GameObject obj) {
					Destroy (obj);
				});

				// Tour à la prochaine personne
				tourDuJoueurAJouer = listPersonnage [prochainJoueur];
				tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.DebutTour ();

				// Le menu est activé si le joueur est jouable.
				if (listJoueur.Contains (tourDuJoueurAJouer)) {
					navigationPos = "menuCombat";
					menuCombat.SetActive (true);
					panelOption.SetActive (true);
					// TODO : arranger le premier bouton hightlighter
					EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (panelOption.transform.GetChild (0).gameObject);
				} else {
					//obtenirCible();
					tourDuJoueurAJouer.GetComponent<GestionAI> ().ObtenirCiblePlusDeVie (listJoueur);
					tourDuJoueurAJouer.GetComponent<GestionAI> ().CalculerRoute ();
					if (!tourDuJoueurAJouer.GetComponent<GestionAI> ().DeplacerEnemi ()) {
						tourDuJoueurAJouer.GetComponent<GestionAI> ().AttaqueBase ();
						StartCoroutine (AttendFinAttaque ());
					} else {
						StartCoroutine (AttendFinMouvement ());
					}
				}
			}


			// Touche de navigation avec ESC pour revenir en arrière
			if (menuCombat.activeSelf && Input.GetButtonUp ("Annuler") && !AnnulerAppuye) {
				switch (navigationPos) {
				case "panelCompetence":
					panelCompetences.SetActive (false);
					panelOption.SetActive (true);
					navigationPos = "menuCombat";
					EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (panelOption.transform.GetChild (0).gameObject);
					foreach (Transform enfant in panelCompetences.transform) {
						Destroy (enfant.gameObject);
					}
					break;
				case "panelItem":
					panelItems.SetActive (false);
					panelOption.SetActive (true);
					navigationPos = "menuCombat";
					EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (panelOption.transform.GetChild (0).gameObject);
					foreach (Transform enfant in panelScrollView.transform) {
						Destroy (enfant.gameObject);
					}
					break;
				case "menuCombat":
					menuCombat.SetActive (false);
					curseur = Instantiate (curseurPrefab, tourDuJoueurAJouer.transform.position, Quaternion.identity) as GameObject;
					curseur.GetComponent<CurseurAnim> ().curseurCaseActuel = tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel;
					curseur.GetComponent<SpriteRenderer> ().sortingOrder = 1;
					curseur.GetComponent<CurseurAnim> ().option = "infoPersonnage";
					break;
				}
			}
		}

		if (!EventSystem.GetComponent<EventSystem> ().currentSelectedGameObject) {
			EventSystem.GetComponent<EventSystem> ().SetSelectedGameObject (lastSelectedGameObject);
		} else {
			lastSelectedGameObject = EventSystem.GetComponent<EventSystem> ().currentSelectedGameObject;
		}
	}

	private IEnumerator AttendFinMouvement(){
		yield return new WaitUntil (() => tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().GetFiniMouvement () == true);
		prochainJoueur++;
		tourTermine = true;
	}

	private IEnumerator AttendFinCombat (){
		yield return new WaitForSeconds (3);
		if (listEnemi.Count == 0) {
			panelFinCombat.SetActive (false);
			panelAffichageExp.SetActive (true);
			int i = -10;
			int j = 0;
			int expAncien = 0;
			int expNouveau = 0;
			List<GameObject> joueurExp = new List<GameObject> ();
			foreach (Joueur p in DonneesJeu.Equipe.Membres) {
				joueurExp.Add(Instantiate (joueurExpPrefab, panelAffichageExp.GetComponent<AffichageExp> ().panelExp.transform) as GameObject);
				joueurExp[j].transform.localPosition = new Vector2 (joueurExp[j].transform.localPosition.x, i);
				joueurExp[j].GetComponent<Text> ().text = p.Nom;
				joueurExp[j].GetComponent<JoueurExpPrefab> ().txtExp.text = p.EXP.ToString();
				expAncien = (int)p.EXP;
				expNouveau = (int)p.EXP + ExperienceGagnee;
				Debug.Log (ExperienceGagnee);
				joueurExp[j].GetComponent<JoueurExpPrefab> ().txtLvl.text = p.NIV.ToString();
				i -= 60;
				j++;
			}
			while (expAncien < expNouveau) {
				j = 0;
				foreach(Joueur p in DonneesJeu.Equipe.Membres){
					p.AjoutExp (1);
					joueurExp [j].GetComponent<JoueurExpPrefab> ().txtExp.text =p.EXP.ToString ();
					if(p.NiveauSuivant){
						p.EffectuerNiveauSuivant();
					}
					joueurExp [j].GetComponent<JoueurExpPrefab> ().txtLvl.text = p.NIV.ToString ();
					j++;
				}
				expAncien++; 
				yield return new WaitForSeconds (0.1f);
			}
			int posItem = 0;
			Dictionary<int, int>.Enumerator e = listObjetGagnee.GetEnumerator ();
			while(e.MoveNext()) {
				GameObject obj;
				Objets.Objet itemGagne = Objets.TrouverObjet (e.Current.Key);
				obj = Instantiate(objetPrefab,panelAffichageExp.GetComponent<AffichageExp>().panelObjet.transform) as GameObject;
				obj.transform.localPosition = new Vector2 (0, posItem);
				obj.GetComponent<Text>().text = itemGagne.Nom;
				obj.GetComponent<objObtenuPrefab>().txtQte.text = e.Current.Value.ToString();
				DonneesJeu.Equipe.Inventaire.Ajouter(itemGagne.ID,e.Current.Value);
				posItem -= 20;
			}

			StartCoroutine (AttendFinExp ());
		} else {
			GestScene.ProchaineScene ("Menu_Principal");
		}
	}

	private IEnumerator AttendFinExp(){
		yield return new WaitForSeconds (3);
		GestScene.ProchaineScene ("Monde");
	}

	private IEnumerator AttendFinAttaque(){
		yield return new WaitForSeconds(1);
		prochainJoueur++;
		tourTermine = true;
	}

	private IEnumerator AttendFinSpecialAttaque(){
		yield return new WaitForSeconds(2.5f);
		prochainJoueur++;
		tourTermine = true;
		panelOption.SetActive (true);
		panelCompetences.SetActive (false);
		List<GameObject> lst = VerifierListEnemi ();
		List<Personnage> lstPerso = new List<Personnage> ();
		foreach (GameObject go in lst) {
			lstPerso.Add(go.GetComponent<GestionPersonnage>().monPersonnage);
		}
		tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.EffectuerAttaqueSpeciale (lstPerso);
	}

	private void ActiverCurseur (string cursOption){
		menuCombat.SetActive (false);
		curseur = Instantiate(curseurPrefab,tourDuJoueurAJouer.transform.position,Quaternion.identity) as GameObject;
		var cur = curseur.GetComponent<CurseurAnim> ();
		cur.curseurCaseActuel = tourDuJoueurAJouer.GetComponent<PersonnageMouvement> ().caseActuel;
		cur.tourDuJoueurAJouer = tourDuJoueurAJouer;
		curseur.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		cur.option = cursOption;
	}

	public List<GameObject> VerifierListEnemi(){
		if (tourDuJoueurAJouer.GetComponent<GestionPersonnage>().listCaseCible!=null && tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().listCaseCible.Count != 0) {
			List<GameObject> listReceveur = new List<GameObject>();
			foreach (GameObject goCaseCible in tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().listCaseCible) {
				if (tourDuJoueurAJouer.GetComponent<GestionPersonnage> ().monPersonnage.AttaqueSpeciale.Negatif == true) {
					if (goCaseCible.GetComponent<GestionCases> ().Personnage && listEnemi.Contains (goCaseCible.GetComponent<GestionCases> ().Personnage)) {
						listReceveur.Add (goCaseCible.GetComponent<GestionCases> ().Personnage);
					}
				} else {
					if (goCaseCible.GetComponent<GestionCases> ().Personnage && listJoueur.Contains (goCaseCible.GetComponent<GestionCases> ().Personnage)) {
						listReceveur.Add (goCaseCible.GetComponent<GestionCases> ().Personnage);
					}
				}
			}
			return listReceveur;
		} else {
			return null;
		}
	}

	public void AfficherInfo(){
		panelInfo.SetActive (true);
		var perso = curseur.GetComponent<CurseurAnim> ().curseurCaseActuel.GetComponent<GestionCases> ().Personnage;
		panelInfo.GetComponent<infoPerso>().txtNom.text = perso.GetComponent<GestionPersonnage> ().monPersonnage.Nom;
		panelInfo.GetComponent<infoPerso>().txtPV.text = perso.GetComponent<GestionPersonnage> ().monPersonnage.PV.ToString();
		panelInfo.GetComponent<infoPerso>().txtForce.text = perso.GetComponent<GestionPersonnage> ().monPersonnage.PUI.ToString();
		panelInfo.GetComponent<infoPerso>().txtDef.text = perso.GetComponent<GestionPersonnage> ().monPersonnage.DEF.ToString();
		panelInfo.GetComponent<infoPerso>().txtVit.text = perso.GetComponent<GestionPersonnage> ().monPersonnage.VIT.ToString();
	}
}