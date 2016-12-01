using UnityEngine;
using System.Collections;

public class PersonnageMouvement : MonoBehaviour {

	public GameObject caseActuel{ get; set;}
	public GameObject caseDepart{ get; set;}
	public GameObject caseDestination{ get; set;}
	public string modeleAnimator;
	private Animator animator;
	private Vector3 direction = new Vector3(0,0,0);
	private Vector3 distance;
	private float speed = 1.5f;
	private bool finiMouvement = false;
	public AudioClip marcheGazon;
	public AudioClip attaque;
	private AudioSource audio;
	public GameObject projectilePrefab;

	public void AssocierCaseDepartRand(int minHauteur,int maxHauteur,int minLargeur,int maxLargeur){
		do {
			caseDepart = GameObject.Find ("CombatGrid/Colonne" + Random.Range(minLargeur,maxLargeur) + "/case" + Random.Range(minHauteur,maxHauteur));

		} while(caseDepart.GetComponent<GestionCases> ().EstOccupee);
		caseDepart.GetComponent<GestionCases> ().SwitchOccupation (this.gameObject,true);
	}
	
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
		modeleAnimator = GetComponent<GestionPersonnage>().monPersonnage.Role.Nom+"AC";
		animator.runtimeAnimatorController = Resources.Load ("Animator/"+modeleAnimator) as RuntimeAnimatorController;
		transform.position = caseDepart.transform.position;
		GetComponent<SpriteRenderer> ().sortingOrder = caseDepart.GetComponent<GestionCases> ().PosY+1;
		caseActuel = caseDepart;

		if (modeleAnimator == "MageAC") {
			projectilePrefab = Resources.Load ("Prefab/Attaques/Meteor") as GameObject;
		}
		if (modeleAnimator == "ArcherAC") {
			projectilePrefab = Resources.Load("Prefab/Attaques/Fleche") as GameObject;
		}
		if (modeleAnimator == "PrêtreAC") {
			projectilePrefab = Resources.Load ("Prefab/Attaques/boule")as GameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		// Si le personnage reçoit une case destination
		if (caseDestination) {
			caseActuel.GetComponent<GestionCases> ().SwitchOccupation (this.gameObject,false);
			animator.SetBool ("Neutre", false);
			distance = transform.position - caseDestination.transform.position;
			direction = -distance / distance.magnitude;
			GetComponent<SpriteRenderer> ().sortingOrder = caseDestination.GetComponent<GestionCases> ().PosY+1;
			if (distance.magnitude >= 0.10f) {
				if (!audio.isPlaying) {
					audio.PlayOneShot (marcheGazon);
				}
				transform.position += direction * speed * Time.deltaTime;
			}else {
				transform.position = caseDestination.transform.position;
				caseActuel = caseDestination;
				caseActuel.GetComponent<GestionCases> ().SwitchOccupation (this.gameObject,true);
				caseDestination = null;
				finiMouvement = true;
			}
		} else {
			finiMouvement = false;
			animator.SetBool ("Neutre", true);
			if (direction.x !=0) {
				animator.SetFloat ("faceDroite", direction.x);
			}
		}

		// Indique la direction ou le personnage doit agir ( mouvement ou attaque)
		animator.SetFloat ("x", direction.x);
		animator.SetFloat ("y", direction.y);
	}

	public bool GetFiniMouvement(){
		return finiMouvement;
	}
	public void SetFiniMouvementTrue(){
		finiMouvement = true;
	}

	public void SetDirection(float Dirx){
		direction.x = Dirx;
	}

	public void faitAttaque(GameObject curseur){
		distance = transform.position - curseur.transform.position;
		direction = -distance / distance.magnitude;
		animator.SetTrigger ("Attaque");
		audio.PlayOneShot (attaque);
	}

	public void faitAttaqueSpecial(){
		GameObject cible = new GameObject();
		foreach (GameObject go in caseActuel.GetComponent<GestionCases>().voisins) {
			if (go.GetComponent<GestionCases> ().EstAttack) {
				cible = go;
			}
		}
		distance = transform.position-cible.transform.position;
		direction = -distance / distance.magnitude;
		animator.SetTrigger ("Attaque");
		GameObject projectileClone;
		projectileClone = Instantiate (projectilePrefab) as GameObject;
		projectileClone.transform.parent = transform;
		projectileClone.transform.position = transform.position;
		projectileClone.GetComponent<projectile> ().direction = direction;
	}
	public void faitAttaqueZone(GameObject curseur){
		distance = transform.position-curseur.transform.position;
		direction = -distance / distance.magnitude;
		animator.SetTrigger ("SpecAttaque");
		GameObject projectileClone;
		projectileClone = Instantiate (projectilePrefab) as GameObject;
		projectileClone.transform.parent = transform;
		projectileClone.transform.position = new Vector2 (transform.position.x,transform.position.y+75);
		projectileClone.GetComponent<Meteor> ().cible = curseur;
	}

	public void faitAttaqueDistance(GameObject curseur){
		Vector3 v3 = new Vector3 (curseur.transform.position.x, curseur.transform.position.y, curseur.transform.position.z);

		distance = transform.position - curseur.transform.position;
		direction = -distance / distance.magnitude;
		animator.SetTrigger ("Attaque");
		if (modeleAnimator == "PrêtreAC") {
			GameObject go;
			go = Instantiate (Resources.Load ("Prefab/Attaques/boulePrep"), transform) as GameObject;
			go.transform.position = transform.position;
			StartCoroutine(AttendFinAttaqueDistance(v3,go));
		} else {
			StartCoroutine (AttendFinAttaqueDistance (v3));
		}
	}

	public void faitArcherSpecial(GameObject curseur){
		distance = transform.position - curseur.transform.position;
		direction = -distance / distance.magnitude;
		animator.SetTrigger ("SpecAttaque");
		GameObject ps;
		if (modeleAnimator == "PrêtreAC") {
			ps = Instantiate (Resources.Load ("Prefab/Attaques/Guerisson")) as GameObject;
			ps.transform.parent = transform;
			ps.transform.position = new Vector2 (curseur.transform.position.x, curseur.transform.position.y);
		} else {
			ps = Instantiate (Resources.Load ("Prefab/Attaques/FlechePluis")) as GameObject;
			ps.transform.parent = transform;
			ps.transform.position = new Vector2 (curseur.transform.position.x, curseur.transform.position.y + 20);
		}
	}

	public IEnumerator AttendFinAttaqueDistance(Vector3 tCible){
		yield return new WaitForSeconds (0.5f);
		GameObject projectileClone;
		projectileClone = Instantiate (projectilePrefab) as GameObject;
		projectileClone.transform.parent = transform;
		projectileClone.transform.position = new Vector2 (transform.position.x,transform.position.y);
		projectileClone.GetComponent<Fleche> ().cible = tCible;
	}

	public IEnumerator AttendFinAttaqueDistance(Vector3 tCible,GameObject go){
		yield return new WaitForSeconds (0.7f);
		Destroy (go);
		GameObject projectileClone;
		projectileClone = Instantiate (projectilePrefab) as GameObject;
		projectileClone.transform.parent = transform;
		projectileClone.transform.position = new Vector2 (transform.position.x,transform.position.y);
		projectileClone.GetComponent<Fleche> ().cible = tCible;
	}
}
