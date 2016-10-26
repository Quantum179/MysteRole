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
    private string TransitionScene;
    private Dictionary<string, TypeScene> _scenesNoms;

    private static InfosWorld Infos;

    private void AttenuationFait(bool ok)
    {
        //Debug.Log("Mi-transition");
        if (ok)
        {
            /*if (*/
            _prochaineScene(TransitionScene);//)
            TransitionScene = null;
            //{
                GestTransition.FinTransition rappel = new GestTransition.FinTransition(AttenuationDefait);
                GestTransition.DefaireAttenuationNoir(rappel, GestTransition.VITESSE.NORMAL);
            //}
            /*else
            {
                Erreurs.NouvelleErreur("La scène \"" + TransitionScene + "\" ne fut pas chargée : Une erreure est survenue.");
            }*/
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
    void Start() {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de GestScene.");

        moi = this;

        _scenesNoms = new Dictionary<string, TypeScene>();
        _scenesNoms.Add("World", TypeScene.Carte);
        _scenesNoms.Add("Combat", TypeScene.Combat);
        _scenesNoms.Add("Init", TypeScene.Initiale);
        _scenesNoms.Add("Menu_Principal", TypeScene.Menu);
        _scenesNoms.Add("Creation_personnage", TypeScene.Menu);
        //_prochaineSceneTransition(PremiereScene);
        TransitionScene = PremiereScene;
    }
	
	// Update is called once per frame
	void Update () {
        if (PremiereFois)
        {
            AttenuationFait(true);
            PremiereFois = false;
            /*else
                FadeIn();*/
        }
    }

    private bool _prochaineScene(string scene)
    {
        if (!_scenesNoms.ContainsKey(scene))
            throw new ArgumentException("Erreur load");
        else if (_scenesNoms[scene] == TypeScene.Initiale)
            throw new ArgumentException("Ne peut charger une scène initiale.");

        if (SceneActuelle == null || scene != SceneActuelle)
        {
            GUI.FocusControl("");
            if (SceneActuelle != null)
            {
                if (_scenesNoms[scene] == TypeScene.Combat && _scenesNoms[SceneActuelle] == TypeScene.Carte)
                {
                    // TODO : Trouver le Transform de l'avatar de l'équipe.
                    Infos = GestionWorld.GetInfos();
                }
                if (!SceneManager.UnloadScene(SceneActuelle))
                    return false;
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
            }

            SceneActuelle = scene;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneActuelle));

            if (_scenesNoms[scene] == TypeScene.Carte && Infos != null)
            {
                GameObject.Find("Player").transform.position = Infos.player.transform.position;
                // TODO : Carte nom
                Infos = null;
            }
        }
        else
            return false;

        return true;
    }
    private void _prochaineSceneTransition(string scene)
    {
        //Debug.Log("Début transition");
        TransitionScene = scene;
        GestTransition.FinTransition rappel = new GestTransition.FinTransition(AttenuationFait);
        TransitionScene = scene;
        GestTransition.FaireAttenuationNoir(rappel);
    }
    /*public static IEnumerator FadeOut()
    {
        ScreenFader sf = moi.Transition.GetComponent<ScreenFader>();
        sf.gameObject.SetActive(true);
        yield return null;
        yield return moi.StartCoroutine(sf.FadeToBlack());
    }
    public static IEnumerator FadeIn()
    {
        ScreenFader sf = moi.Transition.GetComponent<ScreenFader>();
        yield return moi.StartCoroutine(sf.FadeToClear());
        sf.gameObject.SetActive(false);
    }*/
    public static void ProchaineScene(string scene)
    {
        moi._prochaineScene(scene);
    }
    public static void ProchaineSceneTransition(string scene)
    {
        moi._prochaineSceneTransition(scene);
    }
    public static Dictionary<string, TypeScene> ScenesNoms { get { return moi._scenesNoms; } }
}
