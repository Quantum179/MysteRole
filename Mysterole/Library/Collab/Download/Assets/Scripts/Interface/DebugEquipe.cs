using UnityEngine;
using System.Collections;
using Mysterole;
using System.Collections.Generic;
using System;

public class DebugEquipe : MonoBehaviour
{
    public GameObject Joueur1;
    public GameObject Joueur2;
    public GameObject Joueur3;
    public GameObject Joueur4;
    private List<GameObject> Joueurs = new List<GameObject>();
    // Use this for initialization
    void Start ()
    {
        Joueurs.Add(Joueur1);
        Joueurs.Add(Joueur2);
        Joueurs.Add(Joueur3);
        Joueurs.Add(Joueur4);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GuerirTous()
    {
        foreach(Joueur j in DonneesJeu.Equipe.Membres)
        {
            j.Guerir();
        }
    }

    public void ModifierTous()
    {
        foreach(GameObject joueur in Joueurs)
        {
            DebugJoueur dj = joueur.GetComponentInChildren<DebugJoueur>();
            if (DonneesJeu.Equipe.Membres.Count <= dj.ID)
            {
                int niveau;
                try
                {
                    niveau = Math.Max(1, int.Parse(dj.Niveau.text));
                }
                catch
                {
                    niveau = 1;
                }
                DonneesJeu.Equipe.Membres[dj.ID - 1].NIV = (uint)niveau;
                dj.ModifierFait();
            }
        }
    }


    public void Rafraichir()
    {
        foreach (GameObject joueur in Joueurs)
        {
            DebugJoueur dj = joueur.GetComponentInChildren<DebugJoueur>();
            dj.VerifierEquipe();
        }
    }
}
