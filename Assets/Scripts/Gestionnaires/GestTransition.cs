using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GestTransition : MonoBehaviour {
    struct TRANSPARENCE
    {
        public static readonly float VISIBLE = 1.0f;
        public static readonly float INVISIBLE = 0.0f;
    }
    public enum VITESSE
    {
        INSTANT,
        TRES_RAPIDE,
        RAPIDE,
        NORMAL,
        LENT,
        TRES_LENT
    }
    private static GestTransition moi;
    private static VITESSE Vitesse = VITESSE.NORMAL;
    private static float TransparenceCible = TRANSPARENCE.VISIBLE;

    public delegate void FinTransition(bool ok);

    public GameObject Attenuation;
    public static bool EnTransition { get { return moi._enTransition; } }

    private bool _enTransition = false;

    private FinTransition fin;

    private static float AvoirVitesse(VITESSE vitesse)
    {
        switch(vitesse)
        {
            case VITESSE.INSTANT:
                return 999.999f;
            case VITESSE.TRES_RAPIDE:
                return 10.0f;
            case VITESSE.RAPIDE:
                return 5.0f;
            case VITESSE.LENT:
                return 1.0f;
            case VITESSE.TRES_LENT:
                return 0.5f;
            case VITESSE.NORMAL:
            default:
                return 2.0f;
        }
    }

    public static void FaireAttenuationNoir(FinTransition rappel)
    {
        FaireAttenuationNoir(rappel, Vitesse);
    }

    public static void FaireAttenuationNoir(FinTransition rappel, VITESSE vitesse)
    {
        Vitesse = vitesse;

        if (EnTransition)
        {
            rappel(false);
        }
        else
        {
            moi.fin = rappel;
            moi._faireAttenuationNoir();
        }
    }

    private void _faireAttenuationNoir()
    {
        Color c = Color.black;
        c.a = TRANSPARENCE.INVISIBLE;
        Attenuation.GetComponent<Image>().color = c;
        _faireAttenuation();
    }

    private void _faireAttenuation()
    {
        _enTransition = true;
        TransparenceCible = TRANSPARENCE.VISIBLE;
        Attenuation.SetActive(true);
    }

    public static void DefaireAttenuationNoir(FinTransition rappel)
    {
        DefaireAttenuationNoir(rappel, Vitesse);
    }

    public static void DefaireAttenuationNoir(FinTransition rappel, VITESSE vitesse)
    {
        Vitesse = vitesse;

        if (EnTransition)
        {
            rappel(false);
        }
        else
        {
            moi.fin = rappel;
            moi._defaireAttenuationNoir();
        }
    }

    private void _defaireAttenuationNoir()
    {
        // TODO : Verifier que couleur est noir.
        _defaireAttenuation();
    }

    private void _defaireAttenuation()
    {
        TransparenceCible = 0;
        _enTransition = true;
    }

    // Use this for initialization
    void Start () {
        if (moi != null)
            throw new Exception("GestTransition existe déjà.");
        moi = this;
	}
	
	// Update is called once per frame
	void Update () {
	    if (_enTransition)
        {
            Color c = Attenuation.GetComponent<Image>().color;
            float diffAlpha = Mathf.Abs(c.a - TransparenceCible);
            if (diffAlpha > 0.001f)
            {
                c.a = Mathf.Max(Mathf.Min(Mathf.Lerp(c.a, TransparenceCible, AvoirVitesse(Vitesse) * Time.deltaTime), TRANSPARENCE.VISIBLE), TRANSPARENCE.INVISIBLE);
                Attenuation.GetComponent<Image>().color = c;
                /*if (fin == null)
                {
                    Debug.Log("Appel de retour nul?");
                }*/
            }
            else if (fin != null)
            {
                _enTransition = false;
                FinTransition tampon = fin;
                fin = null;
                tampon(true);
            }
            else
            {
                _enTransition = false;
                Erreurs.NouvelleErreur("Une transition s'est complétée sans appel de retour.");
            }
        }
        else if (TransparenceCible == 0)
        {
            Attenuation.SetActive(false);
        }

        if(Input.GetKeyUp("f9"))
        {
            TransparenceCible = TRANSPARENCE.INVISIBLE;
        }
	}

    void OnGUI()
    {
        //GUI.Label(new Rect(100, 400, 200, 25), "Transparence : " + Attenuation.GetComponent<Image>().color.a.ToString());
    }
}
