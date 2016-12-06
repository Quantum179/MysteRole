// Programme : Menu de débogage : Position du joueur
// Auteur : Jean-Michel Beauvais
// Actualise la position affichée dans le menu de débogage et permet la téléportation.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mysterole;

public class DebugPosJoueur : MonoBehaviour {

    public InputField PX;
    public InputField PY;
    public Button Teleport;
    public GameObject NonDispo;
    private double posX;
    private double posY;
    private string carte;
    public static bool modif = false;

	// Use this for initialization
	void Awake ()
    {

        /*PX.interactable = false;
        PY.interactable = false;*/
        ActualiserPosition();
    }

    void ActualiserPosition()
    {
        //Debug.Log("Coordonnées : " + JoueurMonde.Coor.ToString());
        posX = JoueurMonde.Coor.x;
        posY = JoueurMonde.Coor.y;
        PX.text = posX.ToString();
        PY.text = posY.ToString();
        modif = false;
        Teleport.interactable = false;
        NonDispo.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        try
        {
            if (posX != JoueurMonde.Coor.x || posY != JoueurMonde.Coor.y)
                ActualiserPosition();
        }
        catch
        {
            NonDispo.SetActive(true);
        }

    }

    public void SelectionPosition()
    {
        modif = true;
        Teleport.interactable = true;
    }

    public void Teleporter()
    {
        Vector2 cible = new Vector2(float.Parse(PX.text), float.Parse(PY.text));
        JoueurMonde.Coor = cible;
    }
}
