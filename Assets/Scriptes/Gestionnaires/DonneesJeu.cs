using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Mysterole;
using System;

public class DonneesJeu : MonoBehaviour
{
    private static DonneesJeu moi;
    private bool tested = false;
    private bool addTest = false;
    public string FirstScene = "unity";
    public EquipeJoueur _equipe { get; private set; }
	// Use this for initialization
	void Start()
    {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de Donneesjeu.");
        DontDestroyOnLoad(this);
        moi = this;
        _equipe = CreerEquipe();
        SceneManager.LoadScene(FirstScene, LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update()
    {
    }
    void OnGUI()
    {
    }
    public static EquipeJoueur Equipe
    {
        get
        {
            try
            {
                return moi._equipe;
            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Une erreur inatendue s'est produite durant l'accès à l'équipe du joueur : (" + e.GetType().ToString() + ") " + e.Message);
                return CreerEquipe();
            }
        }
    }
    private static EquipeJoueur CreerEquipe()
    {
        EquipeJoueur retrun = new EquipeJoueur();
        return retrun;
    }
}
