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
    private ListeOptions _options = new ListeOptions();
    public GameObject Debogueur;
    public static Equipe Adversaires { get { return moi._adversaires; } set { moi._adversaires = value; } }
    public static Dictionary<int, RoleJoueur> Roles { get { return moi._roles; } }
    public static Dictionary<int, Competence> Competences { get { return moi._competences; } }
    public static Dictionary<int, Role> Monstres { get { return moi._monstres; } }
    public static Dictionary<int, Equipe> EquipesMonstre { get { return moi._equipesMonstres; } }
    public static Dictionary<int, Etat> Etats { get { return moi._etats; } }
    public static ListeOptions Options { get { return moi._options; } }

    // Use this for initialization

    void Awake()
    {
        if (moi != null)
            throw new Exception("Ce système comporte déjà une instance de Donneesjeu.");
        moi = this;
        _equipe = CreerEquipe();
        //Erreurs.NouvelleErreur("TEST");
        ChargerDonnees();
	}

    public static void Vider()
    {
        moi._equipe = CreerEquipe();
        Declencheurs.Vider();
        Options.Defaut();
        JoueurMonde.Coor = Vector2.zero;
        GestionMonde.Reinitialiser();
    }

    private void ChargerDonnees()
    {
        ChargerEtats();
        ChargerCompetences();
        ChargerObjets();
        ChargerRoles();
        ChargerMonstres();
        ChargerEquipeMonstres();
    }

    private void ChargerObjets()
    {
        Objets.Charger();
    }

    private void ChargerEquipeMonstres()
    {
        /*Personnage monstre;
        _equipesMonstres.Add(0, new Equipe("Monstres"));
        monstre = new Personnage(_monstres[1].Nom, _monstres[1], 1);
        _equipesMonstres[0].AjoutMembre(monstre);
        monstre = new Personnage(_monstres[2].Nom, _monstres[2], 1);
        _equipesMonstres[0].AjoutMembre(monstre);*/
        _equipesMonstres = AccesBD.TrouverEquipesMonstres();
    }

    private void ChargerEtats()
    {
        _etats = AccesBD.TrouverEtats();
        return;
    }

    private void ChargerMonstres()
    {
        /*_monstres.Add(1, new Role(1, "Orque", 5, 5, 5, _competences[0], _competences[1]));
        _monstres.Add(2, new Role(2, "Squelette", 5, 5, 5, _competences[0], _competences[1]));*/
        _monstres = AccesBD.TrouverMonstres();
        return;
    }

    private void ChargerCompetences()
    {
        /*Effet[] effets = { new Degats() };
        _competences.Add(0, new Competence("Attaque (Épée)", new Portee(true, 1, 1, true, true, new ZonePoint()), true, true, effets));
		_competences.Add(1, new Competence("Fausse Attaque", new Portee(true, 1, 1, true, true, new ZoneLigne(3,3)), true, false, effets));
        */
        _competences = AccesBD.TrouverComps();
        return;
    }

    private void ChargerRoles()
    {
        //_roles.Add( new RoleJoueur(0, "Guerrier", Stats.PUI, Stats.DEF, Stats.VIT, _competences[0], _competences[1]));
        _roles = AccesBD.TrouverRoles();
        return;
    }

    public bool Sauvegarde(int ID)
    {
        bool complet = false;
        //bool existe = AccesBD.SauveExiste(ID);

        // Sauvegarde équipe et position

        // Sauvegarde personnages

        // Sauvegarde inventaire

        // Sauvegarde variables

        // Sauvegarde quêtes

        return complet;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Debogage") && Options.Debogage && ((JoueurMonde.PeutAgir && GestScene.SurCarte()) || !GestScene.SurCarte() || Debogueur.activeSelf))
        {
            Debogueur.SetActive(!Debogueur.activeSelf);
        }
        else if (!Options.Debogage && Debogueur.activeSelf)
            Debogueur.SetActive(false);
        if (Debogueur.activeSelf || Erreurs.Visible())
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
        static public void Vider()
        {
            _declencheurs = new Dictionary<string, bool>();
        }
    }
    public class ListeOptions
    {
        public bool Muet = false;
        public bool Debogage = true;
        public bool PleinEcran = false;
        public string Resolution = "1280x720";
        public ListeOptions()
        {

        }

        public void Defaut()
        {
            Muet = false;
            Debogage = true;
            PleinEcran = false;
            Resolution = "1280x720";
        }
    }
}
