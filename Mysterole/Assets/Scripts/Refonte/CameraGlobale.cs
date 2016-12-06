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


            if(Input.GetKeyDown(KeyCode.UpArrow))
                transform.position = new Vector3(transform.position.x, transform.position.y + 50, z);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.position = new Vector3(transform.position.x, transform.position.y - 50, z);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.position = new Vector3(transform.position.x - 50, transform.position.y, z);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.position = new Vector3(transform.position.x + 50, transform.position.y, z);

        }
    }


}

