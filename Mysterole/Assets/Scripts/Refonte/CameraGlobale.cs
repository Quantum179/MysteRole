using UnityEngine;
using System.Collections;

namespace Mysterole
{

	public class CameraGlobale : MonoBehaviour 
	{


		private Camera cameraCurr;
		public JoueurMonde joueurMonde;
		private float z = -20;




		void Start () {
			cameraCurr = GetComponent<Camera> ();

			joueurMonde = GameObject.Find("player").GetComponent<JoueurMonde>(); //Faire ça automatiquement depuis GestionMonde
			transform.position = new Vector3(GestionMonde.CarteActive.transform.position.x + 25, GestionMonde.CarteActive.transform.position.y -25, z);


			

			//Hardcode


		}




		void Update () {

		}
	}


}

