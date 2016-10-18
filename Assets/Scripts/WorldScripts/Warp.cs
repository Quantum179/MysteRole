using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class Warp : MonoBehaviour {

    public Transform warpTarget;
    private CameraFollow cf;


    void Start()
    {
        cf = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }
    
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        //ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        yield return StartCoroutine(GestScene.FadeOut());

        Camera.main.transform.position = warpTarget.position;
        //System.Threading.Thread.Sleep(250);
        yield return new WaitForSeconds(3);

		Debug.Log (warpTarget.parent.parent);
        cf.map = warpTarget.parent.parent.GetComponent<TiledMap>();


        other.gameObject.transform.position = warpTarget.position;
        
        yield return StartCoroutine(GestScene.FadeIn());


    }

}
