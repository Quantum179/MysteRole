using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mysterole;
using System.Collections.Generic;

public class GestionPersonnage : MonoBehaviour {

	public Personnage monPersonnage;
	public Slider healthSlider;
	public List<GameObject> listCaseCible = new List<GameObject>();

	// Use this for initialization
	void Start () {
		listCaseCible = null;
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.maxValue = monPersonnage.MPV;
		healthSlider.value = monPersonnage.PV;
		int iPV = (int)((healthSlider.value/healthSlider.maxValue)*100);
		healthSlider.GetComponentInChildren<Text> ().text = iPV.ToString ()+"%";
			


		if (monPersonnage == null)
			return;
		if (monPersonnage.PV <= 0) {
			GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().SwitchOccupation (this.gameObject, false);
			StartCoroutine (AttendMourir ());
			GetComponent<Animator> ().SetBool ("Mort", true);
		}
	}

	public void ReduirePV(int Puissance){
		monPersonnage.PV -= Puissance;
	}

	public void ChangeeAttaque(bool modif){
		foreach (GameObject go in listCaseCible) {
				go.GetComponent<GestionCases> ().EstAttack = modif;
		}
	}

	public IEnumerator AttendMourir(){
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}
