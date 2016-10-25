using UnityEngine;
using System.Collections;
using Tiled2Unity;
using System.Text;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 4f;
    private Vector3 offset;
    public TiledMap map;
    private Camera cameraCurr;


	// Use this for initialization
	void Start () {

        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        offset = transform.position - target.position;

        string sb = "map" + (int)(target.position.x / 50) + (int)(target.position.y / -50);
        map = GameObject.Find(sb).GetComponent<TiledMap>();
        


        cameraCurr = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {



  //      Vector3 targetCamPos = target.position + offset;

  //      if (target.position.x <= map.transform.position.x + cameraCurr.orthographicSize * cameraCurr.aspect)
  //      {
		//	targetCamPos.x = map.transform.position.x + cameraCurr.orthographicSize * cameraCurr.aspect;
  //      }
		//if (target.position.x >= map.transform.position.x + map.NumTilesWide - cameraCurr.orthographicSize * cameraCurr.aspect)
  //      {
		//	targetCamPos.x = map.transform.position.x + map.NumTilesWide - cameraCurr.orthographicSize * cameraCurr.aspect;
  //      }

  //      if (target.position.y >= map.transform.position.y - cameraCurr.orthographicSize)
  //      {
  //          targetCamPos.y = map.transform.position.y - cameraCurr.orthographicSize;
  //      }

		//if (target.position.y <= map.transform.position.y - map.NumTilesHigh + cameraCurr.orthographicSize)
  //      {
		//	targetCamPos.y = map.transform.position.y - map.NumTilesHigh + cameraCurr.orthographicSize;
  //      }


  //      transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
