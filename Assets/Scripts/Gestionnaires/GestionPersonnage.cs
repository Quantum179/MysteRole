using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mysterole;

public class GestionPersonnage : MonoBehaviour {

	public Personnage monPersonnage;
	public Slider healthSlider;
	public GameObject CiblePersonnage{ get; set;}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.maxValue = monPersonnage.MPV;
		healthSlider.value = monPersonnage.PV;
		healthSlider.GetComponentInChildren<Text> ().text = healthSlider.value + "/" + healthSlider.maxValue;


		if (monPersonnage == null)
			return;
		if (monPersonnage.PV == 0) {
			GetComponent<PersonnageMouvement> ().caseActuel.GetComponent<GestionCases> ().SwitchOccupation (this.gameObject, false);
			Destroy (this.gameObject);
		}
	}

	public void ReduirePV(int Puissance){
		monPersonnage.PV -= Puissance;
	}
}
