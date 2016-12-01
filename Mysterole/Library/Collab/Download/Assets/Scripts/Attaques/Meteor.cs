using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

	public GameObject cible;
	public Transform fume;
	public AudioClip feu;
	public AudioClip explose;
	private AudioSource audioS;
	private Vector3 ciblePos;
	private Vector3 direction = new Vector3();
	private Vector3 distance = new Vector3 ();
	private float rot;
	private float speed = 30f;
	int i;

	// Use this for initialization
	void Start () {
		audioS = GetComponent<AudioSource> ();
		audioS.clip = feu;
		audioS.Play ();
		ciblePos = cible.transform.position;
		distance = transform.position - ciblePos;
		direction = -distance / distance.magnitude;
		rot = (float)Mathf.Atan2 (direction.x, direction.y);
		transform.rotation = Quaternion.Euler (0f,0f,rot * Mathf.Rad2Deg );
	}
	
	// Update is called once per frame
	void Update () {
		distance = transform.position - ciblePos;
		if (distance.magnitude >= 2f) {
			transform.position += direction * speed * Time.deltaTime;
		} else {
			audioS.clip = explose;
			if (!audioS.isPlaying) {
				audioS.Play ();
			}
			fume.GetComponent<ParticleSystem> ().Emit (1);
			i++;
		}

		if (i == 100) {
			Destroy (this.gameObject);
		}
	}
}
