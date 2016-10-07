using UnityEngine;
using System.Collections;

public class GestionCases : MonoBehaviour {

	private bool curseur;
	public bool estOccupee{ get; set; }
	public bool estMouvement{ get; set;}
	public bool estAttack{ get; set; }
	public Sprite caseAttaque;
	public Sprite caseMouvement;
	public Sprite caseNormal;
	public int RangeeCalque{ get; set;}
	// Liste de case voisine
	public GameObject caseVoisineHaut{get;set;}
	public GameObject caseVoisineBas{ get; set;}
	public GameObject caseVoisineDroite{ get; set; }
	public GameObject caseVoisineGauche{ get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (estAttack) {
			GetComponent <SpriteRenderer> ().sprite = caseAttaque;
		} else if (estMouvement) {
			GetComponent <SpriteRenderer> ().sprite = caseMouvement;
		} else {
			GetComponent <SpriteRenderer> ().sprite = caseNormal;
		}
	}
}
