using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PointeurMenu : MonoBehaviour {

	private GameObject EventSystem;

	// Use this for initialization
	void Start () {
		EventSystem = GameObject.Find ("EventSystem");
	}
	
	// Update is called once per frame
	void Update () {
		if (EventSystem.GetComponent<EventSystem> ().currentSelectedGameObject != null) {
			var eventPos = EventSystem.GetComponent<EventSystem> ().currentSelectedGameObject.transform.position;
			transform.position = new Vector3 (eventPos.x - 80, eventPos.y - 10, eventPos.z);
		}

	}
}
