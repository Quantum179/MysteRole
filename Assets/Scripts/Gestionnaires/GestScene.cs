using UnityEngine;
using System.Collections;
using System;

public class GestScene : MonoBehaviour
{
    private static GestScene moi;
    public string FirstScene;
    public GameObject Transition;
    //public Fader

    // Use this for initialization
    void Start () {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de GestScene.");
        moi = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
