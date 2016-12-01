using UnityEngine;
using System.Collections;
using Mysterole;

public class ChargerMenu : MonoBehaviour {
    public GameObject PrefabMenu;
    private GameObject Menu = null;

	// Use this for initialization
	void Start () {
        FaireMenu();
    }
	
	// Update is called once per frame
	void Update () {
        if (Menu == null)
            FaireMenu();
        if (Input.GetButtonUp("Menu"))
        {
            //Debug.Log("MENU!");
            if (Menu.activeSelf)
            {
                Menu.SetActive(false);
                JoueurMonde.PeutAgir = true;
            }
            else if (!Menu.activeSelf && JoueurMonde.PeutAgir)
            {
                Menu.SetActive(true);
                JoueurMonde.PeutAgir = false;
            }
        }
	}

    void FaireMenu()
    {
        Menu = Instantiate(PrefabMenu, gameObject.transform.parent) as GameObject;
        Menu.transform.position = new Vector2(0,0);
    }
}
