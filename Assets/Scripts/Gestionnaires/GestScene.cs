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
    public GameObject Transition;
    private string SceneActuelle;
    private Dictionary<string, TypeScene> _scenesNoms;

    public enum TypeScene
    {
        Carte,
        Combat
    }

    // Use this for initialization
    void Start() {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de GestScene.");

        moi = this;

        _scenesNoms = new Dictionary<string, TypeScene>();
        _scenesNoms.Add("World", TypeScene.Carte);
        _scenesNoms.Add("Combat", TypeScene.Combat);
        _prochaineScene(PremiereScene);
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneActuelle == null)
            if (!_prochaineScene(PremiereScene))
                Erreurs.NouvelleErreur("Pas loadé");
    }

    private bool _prochaineScene(string scene)
    {
        /*if (!ScenesNoms.Contains(scene)/* || ScenesInitiales.Contains(scene)/)
        {
            throw new ArgumentException("Erreur load");
            return false;
        }*/

        if (SceneActuelle == null || scene != SceneActuelle)
        {
            if (SceneActuelle != null)
                if (!SceneManager.UnloadScene(SceneActuelle))
                    return false;
            if (!SceneManager.GetSceneByName(scene).isLoaded)
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            }
            SceneActuelle = scene;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneActuelle));
        }
        else
            return false;

        return true;
    }
    private bool _prochaineSceneTransition(string scene)
    {
        FadeOut();
        bool ok = _prochaineScene(scene);
        FadeIn();
        return ok;
    }
    public static IEnumerator FadeOut()
    {
        moi.Transition.SetActive(true);
        yield return null;
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        yield return moi.StartCoroutine(sf.FadeToBlack());
    }
    public static IEnumerator FadeIn()
    {
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        yield return moi.StartCoroutine(sf.FadeToClear());
        moi.Transition.SetActive(false);
    }
    public static bool ProchaineScene(string scene)
    {
        return moi._prochaineScene(scene);
    }
    public static bool ProchaineSceneTransition(string scene)
    {
        return moi._prochaineSceneTransition(scene);
    }
    public static Dictionary<string, TypeScene> ScenesNoms { get { return moi._scenesNoms; } }
}
