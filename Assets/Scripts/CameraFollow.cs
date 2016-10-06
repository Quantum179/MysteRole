using UnityEngine;
using System.Collections;
using Tiled2Unity;

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
        map = GameObject.Find("map0").GetComponent<TiledMap>();

        cameraCurr = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {



        Vector3 targetCamPos = target.position + offset;

        if (target.position.x <= map.transform.position.x + cameraCurr.orthographicSize)
        {
            targetCamPos.x = map.transform.position.x + cameraCurr.orthographicSize;
        }
        if (target.position.x >= map.transform.position.x + 50 - cameraCurr.orthographicSize)
        {
            targetCamPos.x = map.transform.position.x + 50 - cameraCurr.orthographicSize;
        }

        if (target.position.y >= map.transform.position.y - cameraCurr.orthographicSize)
        {
            targetCamPos.y = map.transform.position.y - cameraCurr.orthographicSize;
        }

        if (target.position.y <= map.transform.position.y - 50 + cameraCurr.orthographicSize)
        {
            targetCamPos.y = map.transform.position.y - 50 + cameraCurr.orthographicSize;
        }


        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
