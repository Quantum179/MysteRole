using UnityEngine;
using System.Collections;
using Mysterole;

public class EventTrigger : MonoBehaviour {

    //public IA ia;
    public bool isActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isActive)
        {
            //other.GetComponent<PlayerMovement>().CanMove = !ia.StartEvent();
            isActive = true;
        }

    }

}
