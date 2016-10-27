using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class Erreurs : MonoBehaviour
{
    static Erreurs Main;
    public Text textZone;
    static Text tz;
    static bool change = false;
        
    void Start()
    {
        Main = this;
        tz = textZone;
        gameObject.SetActive(false);
    }
    void OnGUI()
    {
        if (change)
        {
            change = false;
            GetComponentInChildren<Scrollbar>().value = 0;
        }
    }
    static public void NouvelleErreur(string message)
    {
        tz.text += "--------------" + '\n';
        tz.text += "  * " + DateTime.Now.ToString() + " - " + message + '\n';
        Main.gameObject.SetActive(true);
        change = true;
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
    }
    public static void Afficher()
    {
        Main.gameObject.SetActive(true);
    }
    public static void Cacher()
    {
        Main.gameObject.SetActive(false);
    }
    public static bool Visible()
    {
        return Main.gameObject.activeSelf;
    }
}
