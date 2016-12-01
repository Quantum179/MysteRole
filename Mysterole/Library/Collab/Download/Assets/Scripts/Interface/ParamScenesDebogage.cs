using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Mysterole;
using System.Collections.Generic;

public class ParamScenesDebogage : MonoBehaviour {
    public GameObject BoutonsCharger;
    public GameObject ParamsCarte;
    public GameObject ParamsCombat;
    private string sceneChoisie;

	// Use this for initialization
	void Awake () {
        BoutonsCharger.SetActive(false);
        ParamsCarte.SetActive(false);
        ParamsCombat.SetActive(false);
        ChangerScene("Monde");
	}
	
	// Update is called once per frame
	void Update () {
	    if (GestTransition.EnTransition || (GestScene.ScenesNoms[sceneChoisie] == GestScene.TypeScene.Combat && DonneesJeu.Equipe.Membres.Count <= 0))
        {
            IEnumerator btn = BoutonsCharger.GetComponentsInChildren<Button>().GetEnumerator();
            while(btn.MoveNext()) {
                (btn.Current as Button).interactable = false;
            }
        }
        else
        {
            IEnumerator btn = BoutonsCharger.GetComponentsInChildren<Button>().GetEnumerator();
            while (btn.MoveNext())
            {
                (btn.Current as Button).interactable = true;
            }
        }
	}

    public void SurClique(bool transition)
    {
        if (GestScene.ScenesNoms[sceneChoisie] == GestScene.TypeScene.Combat)
        {
            Dropdown dd = DebugLstMonstres.Liste;
            if (dd.value != 0)
                DonneesJeu.Adversaires = DonneesJeu.EquipesMonstre[dd.value - 1];
            else
            {
                DonneesJeu.Adversaires = null;
                Equipe monstres = new Equipe("Debug Manuel");
                Role r = DonneesJeu.Monstres[DebugLstMonstres.Monstre1];
                monstres.AjoutMembre(new Personnage(r.Nom, r, DebugLstMonstres.Niveau1));
                if (DebugLstMonstres.Monstre2 >= 0)
                {
                    r = DonneesJeu.Monstres[DebugLstMonstres.Monstre2];
                    monstres.AjoutMembre(new Personnage(r.Nom, r, DebugLstMonstres.Niveau2));
                }
                if (DebugLstMonstres.Monstre3 >= 0)
                {
                    r = DonneesJeu.Monstres[DebugLstMonstres.Monstre3];
                    monstres.AjoutMembre(new Personnage(r.Nom, r, DebugLstMonstres.Niveau3));
                }
                if (DebugLstMonstres.Monstre4 >= 0)
                {
                    r = DonneesJeu.Monstres[DebugLstMonstres.Monstre4];
                    monstres.AjoutMembre(new Personnage(r.Nom, r, DebugLstMonstres.Niveau4));
                }
                DonneesJeu.Adversaires = monstres;
            }
        }
        if (transition)
        {
            GestScene.ProchaineSceneTransition(sceneChoisie);
        }
        else
        {
            GestScene.ProchaineScene(sceneChoisie);
        }
    }

    public void ChangerScene(string scene)
    {
        ParamsCarte.SetActive(false);
        ParamsCombat.SetActive(false);
        BoutonsCharger.SetActive(true);

        GestScene.TypeScene type = GestScene.ScenesNoms[scene];

        if (type == GestScene.TypeScene.Carte)
        {
            ParamsCarte.SetActive(true);
        }
        else if (type == GestScene.TypeScene.Combat)
        {
            ParamsCombat.SetActive(true);
        }

        sceneChoisie = scene;
    }
}
