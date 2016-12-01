using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	private int i;
	public Vector2 direction;
	private float rot;
	private float speed = 0.20f;
	private Vector2 distanceParcouru;

	// Use this for initialization
	void Start () {
		rot = (float)Mathf.Atan2 (direction.x, direction.y);
		transform.rotation = Quaternion.Euler (0f,-180f,rot * Mathf.Rad2Deg-90 );
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(direction.x*speed+transform.position.x,direction.y*speed+transform.position.y);
		distanceParcouru = new Vector2 (distanceParcouru.x + transform.position.x, distanceParcouru.y + transform.position.y);

		if (distanceParcouru.magnitude > 300) {
			Destroy (this.gameObject);
		}
	}
}
