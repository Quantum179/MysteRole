using UnityEngine;
using System.Collections;

public class Fleche : MonoBehaviour {
	
	public Vector3 cible;

	private Vector3 direction = new Vector3();
	private Vector3 distance = new Vector3 ();
	private float rot;
	private float speed = 15f;
	// Use this for initialization
	void Start () {
		
		distance = transform.position - cible;
		direction = -distance / distance.magnitude;
		rot = (float)Mathf.Atan2 (direction.x, direction.y);
		transform.rotation = Quaternion.Euler (0f,0f,rot * Mathf.Rad2Deg+90 );
	}

	// Update is called once per frame
	void Update () {
		distance = transform.position - cible;
		if (distance.magnitude >= 0.10f) {
			transform.position += direction * speed * Time.deltaTime;
		} else {
			Destroy (this.gameObject);
		}
			
	}
}
