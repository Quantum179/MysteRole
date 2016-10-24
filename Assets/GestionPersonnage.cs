using UnityEngine;
using System.Collections;
using Mysterole;

public class GestionPersonnage : MonoBehaviour {

	public Personnage monPersonnage;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (monPersonnage == null)
			return;
		if (monPersonnage.PV == 0) {
			Destroy (this);
		}

	}
}
