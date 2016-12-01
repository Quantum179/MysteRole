using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mysterole;

public class StatuPersonnage : MonoBehaviour {

	public Slider PV;
	public Text txtPV;
	public Slider PC;
	public Text txtPC;
	public Text txtNom;
	public Personnage infoPerso;

	// Use this for initialization
	void Start () {
		txtNom.text = infoPerso.Nom;
	}
	
	// Update is called once per frame
	void Update () {
		PV.value = infoPerso.PV;
		PV.maxValue = infoPerso.MPV;
		txtPV.text = PV.value + "/" + PV.maxValue;

		PC.value = infoPerso.PC;
		PC.maxValue = infoPerso.MPC;
		txtPC.text = PC.value + "/" + PC.maxValue;
	}
}
