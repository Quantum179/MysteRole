using UnityEngine;
using System.Collections;

public class CurseurAnim : MonoBehaviour {
	private Animator animator;
	public string option{ get; set; }
	public bool ClickAccepte{ get; set; }
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			if (ClickAccepte) {
				animator.SetBool ("isClick", true);
			} else {
				animator.SetBool ("isError", true);
			}
		}
		else if(Input.GetKeyUp("space")){
			if (ClickAccepte) {
				animator.SetBool ("isClick", false);
			} else {
				animator.SetBool ("isError", false);
			}
		}
	}
}
