using UnityEngine;
using System.Collections;
using Mysterole;

public class TriggerEventWorld : MonoBehaviour {

    public GameObject pnj;
    private bool isActive = false;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
        {
            //pnj.GetComponent<IA>().StartEvent();
            //other.gameObject.GetComponent<PlayerMovement>().CanMove = false;
            isActive = true;
        }

    }
}
