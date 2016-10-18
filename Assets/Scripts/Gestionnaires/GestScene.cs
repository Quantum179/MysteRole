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
    private List<string> ScenesNoms;
    private List<string> ScenesChemins;
    private List<string> ScenesInitiales;
    //public Fader

    // Use this for initialization
    void Start() {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de GestScene.");
        ScenesInitiales = new List<string>();
        ScenesNoms = new List<string>();
        ScenesChemins = new List<string>();
        moi = this;
        for (int i=0; i < SceneManager.sceneCount; i++)
        {
            ScenesChemins.Add(SceneManager.GetSceneAt(i).path);
            ScenesNoms.Add(SceneManager.GetSceneAt(i).name);
            if (SceneManager.GetSceneAt(i).isLoaded)
                ScenesInitiales.Add(SceneManager.GetSceneAt(i).name);
        }
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
        }
        else
            return false;

        return true;
    }
    private bool _prochaineSceneTransition(string scene)
    {
        
        bool ok = _prochaineScene(scene);
        return ok;
    }
    public static IEnumerator FadeOut()
    {
        moi.Transition.SetActive(true);
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
}
