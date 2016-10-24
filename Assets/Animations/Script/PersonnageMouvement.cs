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
	private int speed = 96;
	private bool finiMouvement = false;

	public void AssocierCaseDepartRand(int minHauteur,int maxHauteur,int minLargeur,int maxLargeur){
		do {
			caseDepart = GameObject.Find ("CombatGrid/Colonne" + Random.Range(minLargeur,maxLargeur) + "/case" + Random.Range(minHauteur,maxHauteur));
		} while(caseDepart.GetComponent<GestionCases> ().estOccupee);
		caseDepart.GetComponent<GestionCases> ().SwitchOccupation (this.gameObject,true);
	}
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		modeleAnimator = GetComponent<GestionPersonnage>().monPersonnage.Role.Nom+"AC";
		animator.runtimeAnimatorController = Resources.Load ("Animator/"+modeleAnimator) as RuntimeAnimatorController;
		transform.position = caseDepart.transform.position;
		GetComponent<SpriteRenderer> ().sortingOrder = caseDepart.GetComponent<GestionCases> ().RangeeCalque;
		caseActuel = caseDepart;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Si le personnage reçoit une case destination
		if (caseDestination) {
			caseActuel.GetComponent<GestionCases> ().SwitchOccupation (this.gameObject,false);
			animator.SetBool ("Neutre", false);
			distance = transform.position - caseDestination.transform.position;
			direction = -distance / distance.magnitude;
			GetComponent<SpriteRenderer> ().sortingOrder = caseDestination.GetComponent<GestionCases> ().RangeeCalque;
			if (distance.magnitude >= 1) {
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

	public void SetDirection(float Dirx){
		direction.x = Dirx;
	}

	public void faitAttaque(GameObject curseur){
		distance = transform.position - curseur.transform.position;
		direction = -distance / distance.magnitude;
		animator.SetTrigger ("Attaque");
		finiMouvement = true;
	}
}
