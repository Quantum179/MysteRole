// Programme : Menu de débogage : Liste des Scènes
// Auteur : Jean-Michel Beauvais
// Actualise la liste des « scènes » selon la liste dans GestScene.cs

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ListeScenesDebogueur : MonoBehaviour {
    public GameObject PrefabScene;
    public GameObject ZoneListe;
    public GameObject ZoneParam;

	// Use this for initialization
	void Start () {
        //Debug.Log("NBR scenes : " + GestScene.ScenesNoms.Count);
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
    }
	
	// Update is called once per frame
	void Update () {
    }
}
