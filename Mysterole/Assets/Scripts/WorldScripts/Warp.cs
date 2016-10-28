using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class Warp : MonoBehaviour {

    public Transform warpTarget;
    private CameraFollow cf;
    GameObject player;


    void Start()
    {
        cf = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;

        //player.GetComponent<PlayerMovement>().CanMove = false;



        //ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        GestTransition.FinTransition rappel = new GestTransition.FinTransition(FaireWarp);
        GestTransition.FaireAttenuationNoir(rappel, GestTransition.VITESSE.TRES_RAPIDE);
        
    }


    public void FaireWarp(bool ok)
    {


		//Debug.Log (warpTarget.parent.parent);
        cf.map = warpTarget.parent.parent.GetComponent<TiledMap>();
        GestionWorld.UpdateMap(cf.map);
        
        player.transform.position = warpTarget.position;
        Camera.main.transform.position = warpTarget.position;

        GestTransition.FinTransition rappel = new GestTransition.FinTransition(FinWarp);
        GestTransition.DefaireAttenuationNoir(rappel, GestTransition.VITESSE.TRES_RAPIDE);


    }

    public void FinWarp(bool ok)
    {
        //player.GetComponent<PlayerMovement>().CanMove = true;
    }

}
