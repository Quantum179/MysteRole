using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mysterole;
using System.Collections.Generic;
using UnityEngine.UI;


public class GestionOptions : MonoBehaviour {


    public Toggle Son;
    public Toggle debogage;
    public Toggle PleinEcran;
    public Dropdown Res;

    // Use this for initialization
    void Start () {
	
	}

    public void Son_toggle()
    {
        if (Son.isOn == true)
        {
            AudioListener.volume = 1f;
            DonneesJeu.Options.Muet = false;
        }
        else
        {
            AudioListener.volume = 0f;
            DonneesJeu.Options.Muet = true;
        }
    }

    public void Debug_toggle()
    {
        if (debogage.isOn == true)
        {
            DonneesJeu.Options.Debogage = true;
        }
        else
        {
            DonneesJeu.Options.Debogage = false;
        }
    }

    public void PleinEcran_toggle()
    {
        if (PleinEcran.isOn == true)
        {
            DonneesJeu.Options.PleinEcran = true;
            ChangerResolution();
        }
        else
        {
            DonneesJeu.Options.PleinEcran = false;;
            ChangerResolution();
        }
    }

    public void ChangerResolution()
    {
        switch (Res.value)
        {
            case 0:
                Screen.SetResolution(640, 480, DonneesJeu.Options.PleinEcran);
                break;
            case 1:
                Screen.SetResolution(1280, 720, DonneesJeu.Options.PleinEcran);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, DonneesJeu.Options.PleinEcran);
                break;
        }
    }
}
