using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoutonSceneDebogage : MonoBehaviour {
    public GameObject ZoneParams;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initZone(GameObject zone)
    {
        ZoneParams = zone;
    }

    public void SurClique()
    {
        ZoneParams.SendMessage("ChangerScene", GetComponentInChildren<Text>().text);
    }
}
