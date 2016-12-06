// Programme : Gestionnaire des Scènes
// Auteur : Jean-Michel Beauvais
// Système gérant le changement de « scène » avec Unity.
// « scène » : Ensemble d'éléments actifs pour l'application. Plus d'une « scène » peut être active.
//             Les éléments d'une « scène » inactive sont détruits.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using Mysterole;

public class GestScene : MonoBehaviour
{
    private static GestScene moi;
    public string PremiereScene;
    private string SceneActuelle;
    private bool PremiereFois = true;
    //private Vector2 PlacerJoueur = Vector2.zero;
    private string TransitionScene;
    private Dictionary<string, TypeScene> _scenesNoms;

    private void _prochaineSceneTransition(string scene)
    {
        TransitionScene = scene;
        GestTransition.FinTransition rappel = new GestTransition.FinTransition(AttenuationFait);
        GestTransition.FaireAttenuationNoir(rappel);
    }
    private void AttenuationFait(bool ok)
    {
        //Debug.Log("Mi-transition");
        if (ok)
        {
            _prochaineScene(TransitionScene);
            TransitionScene = null;
            GestTransition.FinTransition rappel = new GestTransition.FinTransition(AttenuationDefait);
            GestTransition.DefaireAttenuationNoir(rappel);
        }
        else
            Erreurs.NouvelleErreur("La scène \"" + TransitionScene + "\" ne fut pas chargée : La transition a échoué.");
    }

    private void AttenuationDefait(bool ok)
    {
        //Debug.Log("Fin");
        if (!ok)
            Erreurs.NouvelleErreur("Une erreur est survenue lors de la fin de la transition vers \"" + TransitionScene + "\".");
    }

    public enum TypeScene
    {
        Carte,
        Combat,
        Initiale,
        Menu
    }

    // Use this for initialization
    void Awake() {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de GestScene.");

        moi = this;

        _scenesNoms = new Dictionary<string, TypeScene>();
        _scenesNoms.Add("Monde", TypeScene.Carte);
        _scenesNoms.Add("Combat", TypeScene.Combat);
        _scenesNoms.Add("Init", TypeScene.Initiale);
        _scenesNoms.Add("Menu_Principal", TypeScene.Menu);
        TransitionScene = PremiereScene;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        checkActive();
        if (PremiereFois)
        {
            AttenuationFait(true);
            PremiereFois = false;
            /*else
                FadeIn();*/
        }
    }

    void checkActive()
    {
        Scene actu = SceneManager.GetSceneByName(SceneActuelle);
        //Debug.Log("Scene actuelle : " + (actu.name == null ? "[null]" : actu.name) + " // Scène active : " + SceneManager.GetActiveScene().name);
        if (actu.name != null && SceneManager.GetActiveScene() != actu)
        {
            //Debug.Log("PROBLÈME : La scène actuelle \"" + actu.name + "\" est différente de la scène active \"" + SceneManager.GetActiveScene().name + "\"... Activation de la bonne scène.");
            SceneManager.SetActiveScene(actu);
        }
    }

    private bool _prochaineScene(string scene)
    {
        if (!_scenesNoms.ContainsKey(scene))
            throw new ArgumentException("Erreur load : Scène manquante... (" + scene + ")");
        else if (_scenesNoms[scene] == TypeScene.Initiale)
            throw new ArgumentException("Ne peut charger une scène initiale.");

        if (SceneActuelle == null || scene != SceneActuelle)
        {
            GUI.FocusControl("");
            if (SceneActuelle != null)
            {
                /*bool ReplaceJoueur = false;
                if (SceneActuelle == "Monde" && scene == "Combat")
                    ReplaceJoueur = true;*/
                if (!SceneManager.UnloadScene(SceneActuelle))
                    return false;
                /*if (ReplaceJoueur)
                    PlacerJoueur = new Vector2(JoueurMonde.Coor.x, JoueurMonde.Coor.y);*/
            }

            Dictionary<string, TypeScene>.Enumerator e = _scenesNoms.GetEnumerator();

            while(e.MoveNext())
            {
                if (e.Current.Key != scene && e.Current.Value != TypeScene.Initiale)
                {
                    if (SceneManager.GetSceneByName(e.Current.Key).isLoaded)
                        SceneManager.UnloadScene(e.Current.Key);
                }
            }

            if (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                /*if (scene == "Monde" && SceneActuelle == "Combat")
                {
                    if (PlacerJoueur != Vector2.zero)
                    {
                        JoueurMonde.Coor = PlacerJoueur;
                        PlacerJoueur = Vector2.zero;
                    }
                }*/
            }

            SceneActuelle = scene;
            checkActive();
        }
        else
            return false;

        return true;
    }
    public static void ProchaineScene(string scene)
    {
        moi._prochaineScene(scene);
    }
    public static void ProchaineSceneTransition(string scene)
    {
        moi._prochaineSceneTransition(scene);
    }
    public static Dictionary<string, TypeScene> ScenesNoms { get { return moi._scenesNoms; } }
    public static bool SurCarte()
    {
        return moi._scenesNoms[moi.SceneActuelle] == TypeScene.Carte;
    }
    public static bool EnCombat()
    {
        return moi._scenesNoms[moi.SceneActuelle] == TypeScene.Combat;
    }
}
