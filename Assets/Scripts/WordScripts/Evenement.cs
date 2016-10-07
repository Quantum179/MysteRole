using UnityEngine;
using System.Collections;

public class Evenement : MonoBehaviour {

    private string message = "Salut à toi jeune aventurier";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string Talk()
    {
        return message;
    }

}
