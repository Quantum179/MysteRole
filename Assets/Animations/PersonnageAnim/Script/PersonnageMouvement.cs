using UnityEngine;
using System.Collections;

public class PersonnageMouvement : MonoBehaviour {

	public GameObject caseActuel{ get; set;}
	public GameObject caseDepart{ get; set;}
	public GameObject caseDestination{ get; set;}
	private Animator animator;
	private Vector3 direction = new Vector3(0,0,0);
	private int speed = 32;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		caseDepart.GetComponent<GestionCases> ().estOccupee = true;
		transform.position = caseDepart.transform.position;
		GetComponent<SpriteRenderer> ().sortingOrder = caseDepart.GetComponent<GestionCases> ().RangeeCalque;
		caseActuel = caseDepart;
	}
	
	// Update is called once per frame
	void Update () {
		if (caseDestination) {
			caseActuel.GetComponent<GestionCases> ().estOccupee = false;
			animator.SetBool ("Neutre", false);
			var distance = transform.position - caseDestination.transform.position;
			direction = -distance / distance.magnitude;
			GetComponent<SpriteRenderer> ().sortingOrder = caseDestination.GetComponent<GestionCases> ().RangeeCalque;
			if (distance != Vector3.zero) {
				transform.position += direction * speed * Time.deltaTime;
				if (distance.magnitude <= 1) {
					transform.position = caseDestination.transform.position;
					caseActuel = caseDestination;
					caseActuel.GetComponent<GestionCases> ().estOccupee = true;
					caseDestination = null;
				}
			}
		} else {
			animator.SetBool ("Neutre", true);
			if(direction.x !=0){
				animator.SetFloat("faceDroite",direction.x);
			}
		}

		animator.SetFloat ("x", direction.x);
		animator.SetFloat ("y", direction.y);
	}
}
