using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Mysterole;
using System;

public class DonneesJeu : MonoBehaviour
{
    private static DonneesJeu moi;
    private EquipeJoueur _equipe;
    private Equipe _adversaires;
    private Dictionary<int, RoleJoueur> _roles = new Dictionary<int, RoleJoueur>();
    private Dictionary<int, Competence> _competences = new Dictionary<int, Competence>();
    private Dictionary<int, Role> _monstres = new Dictionary<int, Role>();
    private Dictionary<int, Equipe> _equipesMonstres = new Dictionary<int, Equipe>();
    private Dictionary<int, Etat> _etats = new Dictionary<int, Etat>();
    public GameObject Debogueur;
    public static Equipe Adversaires { get { return moi._adversaires; } set { moi._adversaires = value; } }
    public Dictionary<int, RoleJoueur> Roles { get { return _roles; } }
    public Dictionary<int, Competence> Competences { get { return _competences; } }
    public Dictionary<int, Role> Monstres { get { return _monstres; } }
    public Dictionary<int, Equipe> EquipesMonstre { get { return _equipesMonstres; } }
    public Dictionary<int, Etat> Etats { get { return _etats; } }

    // Use this for initialization
    void Start()
    {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de Donneesjeu.");
        moi = this;
        _equipe = CreerEquipe();
        ChargerDonnees();
        //SceneManager.LoadScene(FirstScene, LoadSceneMode.Additive);
	}

    private void ChargerDonnees()
    {
        ChargerEtats();
        ChargerCompetences();
        ChargerRoles();
        ChargerMonstres();
        ChargerEquipeMonstres();
		_equipe.AjoutMembre(new Joueur("Maurice", _roles[0], 5));
    }

    private void ChargerEquipeMonstres()
    {
        Personnage monstre;
        _equipesMonstres.Add(0, new Equipe("Monstres"));
        monstre = new Personnage(_monstres[0].Nom, _monstres[0], 1);
        _equipesMonstres[0].AjoutMembre(monstre);
        monstre = new Personnage(_monstres[1].Nom, _monstres[1], 1);
        _equipesMonstres[0].AjoutMembre(monstre);
    }

    private void ChargerEtats()
    {
        return;
    }

    private void ChargerMonstres()
    {
        _monstres.Add(0, new Role("Orque", 5, 5, 5, _competences[0], _competences[1]));
        _monstres.Add(1, new Role("Squelette", 5, 5, 5, _competences[0], _competences[1]));
        return;
    }

    private void ChargerCompetences()
    {
        Effet[] effets = { new Degats() };
        _competences.Add(0, new Competence("Attaque (Épée)", new Cible(true, 1, 1, true, true, new ZonePoint()), true, true, effets));
        _competences.Add(1, new Competence("Fausse Attaque", new Cible(true, 1, 1, true, true, new ZonePoint()), true, false, effets));
        return;
    }

    private void ChargerRoles()
    {
        _roles.Add(0, new RoleJoueur("Guerrier", Stats.PUI, Stats.DEF, Stats.VIT, _competences[0], _competences[1]));
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left alt"))
        {
            if (Input.GetKeyUp("f12"))
            {
                Debogueur.SetActive(!Debogueur.activeSelf);
            }
        }
        if (Debogueur.activeSelf || Erreurs.Visible())
            Cursor.visible = true;
        else
            Cursor.visible = false;
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
	public static Dictionary<int, Equipe> EquipeMonstre
	{
		get{ return moi._equipesMonstres; }
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
        static public Dictionary<string, bool> RecupererListe()
        {
            Dictionary<string, bool> retrun = new Dictionary<string, bool>();
            Dictionary<string, bool>.Enumerator e = _declencheurs.GetEnumerator();
            while(e.MoveNext())
            {
                retrun.Add(e.Current.Key, e.Current.Value);
            }
            return retrun;
        }
    }
}
