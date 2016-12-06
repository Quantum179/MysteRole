using UnityEngine;
using System.Collections;
using Tiled2Unity;
using Mysterole;


public class Warp : MonoBehaviour {

    public Transform warpTarget;
    public JoueurMonde perso;


    void Start()
    {
        //cf = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        perso = other.gameObject.GetComponent<JoueurMonde>();
        //Debug.Log(gameObject.transform.parent.parent.name);
        JoueurMonde.PeutAgir = false;

        GestTransition.FinTransition rappel = new GestTransition.FinTransition(FaireWarp);
        GestTransition.FaireAttenuationNoir(rappel, GestTransition.VITESSE.RAPIDE);

    }


    public void FaireWarp(bool ok)
    {

        JoueurMonde.PeutAgir = false;
        //Debug.Log (warpTarget.parent.parent);

        if (GestionMonde.CarteActive.GetComponent<Carte>().Zone != warpTarget.parent.parent.gameObject.GetComponent<Carte>().Zone)
        {
            Bandeau.NouveauBandeau(warpTarget.parent.parent.gameObject.GetComponent<Carte>().Zone);

            if(warpTarget.parent.parent.gameObject.name == "" && DonneesJeu.Declencheurs.EstActif("première fois" + warpTarget.parent.parent.gameObject.name))
            {
            }
        }



        GestionMonde.Timer = UnityEngine.Random.Range(0, 15);
        GestionMonde.CarteActive = warpTarget.parent.parent.gameObject;
        GestionMonde.VerifierExplorations(GestionMonde.CarteActive);
        perso.transform.position = warpTarget.position;
        GestTransition.FinTransition rappel = new GestTransition.FinTransition(FinWarp);
        GestTransition.DefaireAttenuationNoir(rappel, GestTransition.VITESSE.RAPIDE);




    }

    public void FinWarp(bool ok)
    {
        JoueurMonde.PeutAgir = true;


    }

}
