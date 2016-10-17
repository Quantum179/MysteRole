using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Mysterole;
using System;

public class DonneesJeu : MonoBehaviour
{
    private static DonneesJeu moi;
    public EquipeJoueur _equipe { get; private set; }
	// Use this for initialization
	void Start()
    {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de Donneesjeu.");
        moi = this;
        _equipe = CreerEquipe();
        //SceneManager.LoadScene(FirstScene, LoadSceneMode.Additive);
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
                Erreurs.NouvelleErreur("Une erreur inattendue s'est produite durant l'accès à l'équipe du joueur : (" + e.GetType().ToString() + ") " + e.Message);
                return CreerEquipe();
            }
        }
    }
    private static EquipeJoueur CreerEquipe()
    {
        EquipeJoueur retrun = new EquipeJoueur();
        return retrun;
    }
	static public class Declencheurs
	{
		static private Dictionary<string, bool> _declencheurs = new Dictionary<string, bool>();
		static private void Verifier(string nom)
		{
			if (!_declencheurs.ContainsKey (nom))
			{
				_declencheurs.Add (nom, false);
			}
		}
		static public bool EstActif(string nom)
		{
			Verifier (nom);
			return _declencheurs [nom];
		}
		static public bool EstInactif(string nom)
		{
			Verifier (nom);
			return !_declencheurs [nom];
        }
        static public bool RendreActif(string nom)
        {
            Verifier(nom);
            _declencheurs[nom] = true;
            return _declencheurs[nom];
        }
        static public bool RendreInactif(string nom)
        {
            Verifier(nom);
            _declencheurs[nom] = false;
            return _declencheurs[nom];
        }
        static public bool Inverser(string nom)
        {
            Verifier(nom);
            _declencheurs[nom] = !_declencheurs[nom];
            return _declencheurs[nom];
        }
    }
}
