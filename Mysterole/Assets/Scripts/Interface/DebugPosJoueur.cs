using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mysterole;

public class DebugPosJoueur : MonoBehaviour {

    public InputField PX;
    public InputField PY;
    public GameObject NonDispo;
    private double posX;
    private double posY;
    private string carte;

	// Use this for initialization
	void Awake ()
    {
        PX.interactable = false;
        PY.interactable = false;
        ActualiserPosition();
    }

    void ActualiserPosition()
    {
        try
        {
            //Debug.Log("Coordonnées : " + JoueurMonde.Coor.ToString());
            posX = JoueurMonde.Coor.x;
            posY = JoueurMonde.Coor.y;
            PX.text = posX.ToString();
            PY.text = posY.ToString();
            NonDispo.SetActive(false);
        }
        catch
        {
            NonDispo.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        ActualiserPosition();
    }
}
