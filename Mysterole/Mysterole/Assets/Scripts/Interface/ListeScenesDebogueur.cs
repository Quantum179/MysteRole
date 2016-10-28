using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ListeScenesDebogueur : MonoBehaviour {
    public GameObject PrefabScene;
    public GameObject ZoneListe;
    public GameObject ZoneParam;
    private bool firstTime = true;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (firstTime)
        {
            Dictionary<string, GestScene.TypeScene>.Enumerator e = GestScene.ScenesNoms.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value != GestScene.TypeScene.Initiale)
                {
                    GameObject btn = Instantiate(PrefabScene, ZoneListe.transform) as GameObject;
                    btn.GetComponentInChildren<Text>().text = e.Current.Key;
                    btn.SetActive(true);
                    btn.SendMessage("initZone", ZoneParam);
                }
            }
            firstTime = false;
        }
    }
}
