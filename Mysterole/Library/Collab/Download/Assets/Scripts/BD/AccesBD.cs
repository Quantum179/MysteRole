using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Mysterole;

public partial class AccesBD : MonoBehaviour {

    public string Serveur;
    public string BaseDonnee;
    public string Utilisateur;
    public string MotDePasse;

    private MySqlConnection Connexion;

    static private AccesBD moi;

    // Use this for initialization
    void Awake () {
        moi = this;
        string chnConn =
              "server=" + Serveur     + ";" +
            "database=" + BaseDonnee  + ";" +
                 "uid=" + Utilisateur + ";" +
            "password=" + MotDePasse;
        try
        {
            Connexion = new MySqlConnection(chnConn);
            //Erreurs.NouvelleErreur("BD crée");
        }
        catch (Exception e)
        {
            Erreurs.NouvelleErreur("Erreur lors de la création de la connexion MySql : (" + e.GetType() + ") " + e.Message);
        }
    }

    bool OuvrirConnexion()
    {
        try
        {
            Connexion.Open();
            return true;
        }
        catch (Exception e)
        {
            Erreurs.NouvelleErreur("Erreur lors de l'ouverture de la BD : (" + e.GetType() + ") " + e.Message);
            return false;
        }
    }

    bool FermerConnexion()
    {
        try
        {
            Connexion.Close();
            return true;
        }
        catch (Exception e)
        {
            Erreurs.NouvelleErreur("Erreur lors de la fermeture de la BD : (" + e.GetType() + ") " + e.Message);
            return false;
        }
    }

    List<Dictionary<string, string>> Selectionner(string Commande)
    {
        List<Dictionary<string, string>> retour = new List<Dictionary<string, string>>();

        if (OuvrirConnexion())
        {
            try
            {
                MySqlCommand commande = new MySqlCommand(Commande, Connexion);
                MySqlDataReader donnees = commande.ExecuteReader();

                while (donnees.Read())
                {
                    Dictionary<string, string> rangee = new Dictionary<string, string>();
                    for (int i = 0; i < donnees.FieldCount; i++)
                    {
                        string donnee;
                        if (donnees.IsDBNull(i))
                            donnee = "";
                        else
                            donnee = donnees.GetString(i);
                        rangee.Add(donnees.GetName(i), donnee);
                    }
                    retour.Add(rangee);
                }
            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Erreur lors du traitement de la commande :\n" + Commande + "\n(" + e.GetType() + ") " + e.Message + 
                        "\n\nNombre de lignes lues avant l'erreur : " + retour.Count);
                retour = new List<Dictionary<string, string>>();
            }
            finally
            {
                FermerConnexion();
            }
        }

        return retour;
    }

    private void MettreAJour(string Commande)
    {
        if (OuvrirConnexion())
        {
            try
            {
                
            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Erreur lors du traitement de la commande :\n" + Commande + "\n(" + e.GetType() + ") " + e.Message);
            }
            finally
            {
                FermerConnexion();
            }
        }
    }

    private void Ajouter(string Commande)
    {
        if (OuvrirConnexion())
        {
            try
            {

            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Erreur lors du traitement de la commande :\n" + Commande + "\n(" + e.GetType() + ") " + e.Message);
            }
            finally
            {
                FermerConnexion();
            }
        }
    }

    private void Effacer(string Commande)
    {
        if (OuvrirConnexion())
        {
            try
            {

            }
            catch (Exception e)
            {
                Erreurs.NouvelleErreur("Erreur lors du traitement de la commande :\n" + Commande + "\n(" + e.GetType() + ") " + e.Message);
            }
            finally
            {
                FermerConnexion();
            }
        }
    }

    public static Dictionary<int, Equipe> RecupererSauvegardes()
    {
        return moi._recupererSauvegardes();
    }

    private Dictionary<int, Equipe> _recupererSauvegardes()
    {
        Dictionary<int, Equipe> retour = new Dictionary<int, Equipe>();

        string Commande = "SELECT equipes.*, personnages.* " +
                          "FROM equipes LEFT JOIN personnages ON personnages.idEquipe = equipes.idEquipe " +
                                       "LEFT JOIN inventaires ON inventaires.idEquipe = equipes.idEquipe";

        return retour;
    }

    public static Dictionary<int, Objets.Objet> TrouverObjets()
    {
        return moi._trouverObjets();
    }

    public Dictionary<int, Objets.Objet> _trouverObjets()
    {
        Dictionary<int, Objets.Objet> objets = new Dictionary<int, Objets.Objet>();

        string Commande = "SELECT * FROM objets";
        List<Dictionary<string, string>> lstObjets = Selectionner(Commande);
        foreach(Dictionary<string, string> objet in lstObjets)
        {
            Objets.Objet o = null;
            switch (objet["idTypeObjet"])
            {
                case "0":
                    Commande = "SELECT * FROM consommables WHERE idConsomable=" + objet["idObjet"];
                    List<Dictionary<string, string>> consommable = Selectionner(Commande);
                    try
                    {
                        o = Objets.CreerConsommable(int.Parse(objet["idObjet"]), objet["nom"], int.Parse(consommable[0]["puissance"]), DonneesJeu.Competences[int.Parse(consommable[0]["idCompetence"])]);
                    }
                    catch (Objets.DoublonException e)
                    {
                        Erreurs.NouvelleErreur("L'objet (consommable) avec l'ID #" + objet["idObjet"] + " est créé directement par quelqu'un. Impossible de le récupérer dans la BD.");
                    }
                    catch (Exception e)
                    {
                        Erreurs.NouvelleErreur("Erreur inconnue lors de la tentative de création du consommable #" + objet["idObjet"] + ". (" + e.GetType().ToString() + ") : " + e.Message);
                    }
                    break;
                case "1":
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
            }
            if (o != null)
                objets.Add(o.ID, o);
        }

        return objets;
    }

    public static bool SauveExiste(int iD)
    {
        string Commande = "SELECT * FROM equipes WHERE idEquipe=" + iD.ToString();
        return moi.Selectionner(Commande).Count > 0;
    }

    // ************************************************************************
    public List<Dictionary<string, string>> _trouverStats()
    {
        string Commande =
            "SELECT * " +
            "FROM stats";
        return Selectionner(Commande);
    }
    static public List<Dictionary<string, string>> TrouverStats()
    {
        return moi._trouverStats();
    }
    // ************************************************************************
    public Dictionary<int, Etat> _trouverEtats()
    {
        Dictionary<int, Etat> etats = new Dictionary<int, Etat>();
        string Commande =
            "SELECT * " +
            "FROM etats";
        List<Dictionary<string, string>> liste =  Selectionner(Commande);

        foreach(Dictionary<string, string> etat in liste)
        {
            Commande = "SELECT * " +
                       "FROM effetsetatsperiodiques " +
                       "WHERE idEtat=" + etat["idEtat"];
            List<Dictionary<string, string>> lEtats = Selectionner(Commande);
            Commande = "SELECT * " +
                       "FROM effetsetats " +
                       "WHERE idEtat=" + etat["idEtat"];
            List<Dictionary<string, string>> lEffets = Selectionner(Commande);
            Effet[] effets = new Effet[lEffets.Count];
            for (int i = 0; i < lEffets.Count; i++)
            {
                if (lEffets[i]["idStat"] != "8")
                {
                    bool neg = int.Parse(lEffets[i]["montant"]) < 0;
                    effets[i] = new EffetStat(neg, (Stats)int.Parse(lEffets[i]["idStat"]), uint.Parse(lEffets[i]["montant"]));
                }
                else
                {
                    effets[i] = new EffetScenarise(int.Parse(lEffets[i]["montant"]));
                }
            }
            Etat e = null;
            if (lEtats.Count > 0)
            {
                EffetStat[] effetsP = new EffetStat[lEtats.Count];
                for (int i = 0; i < lEtats.Count; i++) {
                    bool neg = int.Parse(lEffets[i]["montant"]) < 0;
                    effetsP[i] = new EffetStat(neg, (Stats)int.Parse(lEtats[i]["idStat"]), uint.Parse(lEtats[i]["montant"]));
                }
                e = new EtatPeriodique(int.Parse(etat["idEtat"]), etat["nom"], int.Parse(etat["duree"]), effetsP, effets);
            }
            else
            {
                e = new Etat(int.Parse(etat["idEtat"]), etat["nom"], int.Parse(etat["duree"]), effets);
            }


            etats.Add(int.Parse(etat["idEtat"]), e);
        }

        return etats;
    }
    static public Dictionary<int, Etat> TrouverEtats()
    {
        return moi._trouverEtats();
    }

    public Dictionary<int, Competence> _trouverComps()
    {
        Dictionary<int, Competence> Competences = new Dictionary<int, Competence>();
        string Commande =
            "SELECT * " +
            "FROM competences";
        List<Dictionary<string, string>> comps = Selectionner(Commande);

        foreach(Dictionary<string, string> comp in comps)
        {
            // ZONE
            Commande =
                "SELECT nom " +
                "FROM zones " +
                "WHERE idZone=" + comp["idZone"];
            Dictionary<string, string> zone = Selectionner(Commande)[0];
            Zone z;
            if (zone["nom"] == "Losange")
            {
                z = new ZoneLosange(int.Parse(comp["zoneLargeurRayon"]));
            }
            else if (zone["nom"] == "Ligne")
            {
                z = new ZoneLigne(int.Parse(comp["zoneLargeurRayon"]), int.Parse(comp["zoneLongueur"]));
            }
            else
            {
                z = new ZonePoint();
            }

            // CIBLE
            Commande =
                "SELECT nom " +
                "FROM portees " +
                "WHERE idPortee=" + comp["idPortee"];
            Dictionary<string, string> portee = Selectionner(Commande)[0];
            Portee c;
            if (portee["nom"] == "Cercle")
            {
                c = new PorteeCercle(!bool.Parse(comp["CaseCiblee"]), z);
            }
            else if (portee["nom"] == "Plein")
            {
                c = new PorteePleine(!bool.Parse(comp["CaseCiblee"]), int.Parse(comp["distanceMin"]), int.Parse(comp["distanceMax"]), z);
            }
            else
            {
                c = new Portee(!bool.Parse(comp["CaseCiblee"]), int.Parse(comp["distanceMin"]), int.Parse(comp["distanceMax"]), bool.Parse(comp["diagonale"]), bool.Parse(comp["longueur"]), z);
            }

            // EFFETS
            Commande =
                "SELECT idStat, idTypeEffet, valeur " +
                "FROM effetscompetences " +
                "WHERE idCompetence=" + comp["idCompetence"];
            List<Dictionary<string, string>> effets = Selectionner(Commande);
            List<Effet> Effets = new List<Effet>();
            foreach(Dictionary<string, string> effet in effets)
            {
                Commande =
                    "SELECT * " +
                    "FROM typeseffets " +
                    "WHERE idTypeEffet=" + effet["idTypeEffet"];
                Dictionary<string, string> teffets = Selectionner(Commande)[0];
                Effet e = null;
                if (teffets["nomTypeEffet"] == "EffetScénarisé")
                {
                    e = new EffetScenarise(int.Parse(effet["valeur"]));
                }
                else if (teffets["nomTypeEffet"] == "Dégât")
                {
                    e = new Degats((Stats)int.Parse(effet["idStat"]));
                }
                else if (teffets["nomTypeEffet"] == "Soin")
                {
                    e = new Soins((Stats)int.Parse(effet["idStat"]));
                }
                else
                {
                    e = new EffetStat(int.Parse(effet["valeur"]) >= 0, (Stats)int.Parse(effet["idStat"]), uint.Parse(effet["valeur"]));
                }
                Effets.Add(e);
            }
            Commande =
                "SELECT idCompetence, idEtat " +
                "FROM competences_etats " +
                "WHERE idCompetence=" + comp["idCompetence"];
            List<Dictionary<string, string>> etats = Selectionner(Commande);
            Competence attaque;
            if (etats.Count > 0)
            {
                attaque = new CompEtat(int.Parse(comp["idCompetence"]), comp["nom"],c, bool.Parse(comp["negatif"]), Effets.ToArray(), DonneesJeu.Etats[int.Parse(etats[0]["idEtat"])]);
            }
            else
            {
                attaque = new Competence(int.Parse(comp["idCompetence"]), comp["nom"], c, bool.Parse(comp["negatif"]), bool.Parse(comp["basique"]), Effets.ToArray());
            }
            Competences.Add(int.Parse(comp["idCompetence"]), attaque);
        }

        return Competences;
    }
    static public Dictionary<int, Competence> TrouverComps()
    {
        return moi._trouverComps();
    }

    public Dictionary<int, RoleJoueur> _trouverRoles()
    {
        Dictionary<int, RoleJoueur> Roles = new Dictionary<int, RoleJoueur>();
        string Commande =
            "SELECT idRole, nom " +
            "FROM roles " +
            "WHERE estJoueur = 1";
        List<Dictionary<string, string>> _roles = Selectionner(Commande);
        foreach(Dictionary<string, string> role in _roles)
        {
            Commande =
                "SELECT montant " +
                "FROM stats_roles " +
                "WHERE idRole=" + role["idRole"] +
                    " AND idStat=" + (int)Stats.PUI;
            List<Dictionary<string, string>> cmdRes = Selectionner(Commande);
            uint pui = uint.Parse(cmdRes[0]["montant"]);
            Commande =
                "SELECT montant " +
                "FROM stats_roles " +
                "WHERE idRole=" + role["idRole"] +
                    " AND idStat=" + (int)Stats.DEF;
            uint def = uint.Parse(Selectionner(Commande)[0]["montant"]);
            Commande =
                "SELECT montant " +
                "FROM stats_roles " +
                "WHERE idRole=" + role["idRole"] +
                    " AND idStat=" + (int)Stats.VIT;
            uint vit = uint.Parse(Selectionner(Commande)[0]["montant"]);

            Stats s1, s2, s3;

            if (pui > def && pui > vit)
            {
                s1 = Stats.PUI;
                if (def > vit)
                {
                    s2 = Stats.DEF;
                    s3 = Stats.VIT;
                }
                else
                {
                    s2 = Stats.VIT;
                    s3 = Stats.DEF;
                }
            }
            else if (def > pui && def > vit)
            {
                s1 = Stats.DEF;
                if (pui > vit)
                {
                    s2 = Stats.PUI;
                    s3 = Stats.VIT;
                }
                else
                {
                    s2 = Stats.VIT;
                    s3 = Stats.PUI;
                }
            }
            else
            {
                s1 = Stats.VIT;
                if (def > pui)
                {
                    s2 = Stats.DEF;
                    s3 = Stats.PUI;
                }
                else
                {
                    s2 = Stats.PUI;
                    s3 = Stats.DEF;
                }
            }

            Commande =
                "SELECT idCompetence " +
                "FROM competences_roles " +
                "WHERE idRole = " + role["idRole"];
            List<Dictionary<string, string>> comps = Selectionner(Commande);
            Competence CompBase = null;
            Competence CompSpec = null;

            foreach(Dictionary<string, string> comp in comps)
            {
                Competence competence = DonneesJeu.Competences[int.Parse(comp["idCompetence"])];

                if (competence.AttaqueBasique)
                {
                    CompBase = competence;
                }
                else
                {
                    CompSpec = competence;
                }
            }

            Roles.Add(int.Parse(role["idRole"]), new RoleJoueur(int.Parse(role["idRole"]), role["nom"], s1, s2, s3, CompBase, CompSpec));
        }

        return Roles;
    }
    static public Dictionary<int, RoleJoueur> TrouverRoles()
    {
        return moi._trouverRoles();
    }

    public Dictionary<int, Role> _trouverMonstres()
    {
        Dictionary<int, Role> Roles = new Dictionary<int, Role>();
        string Commande =
            "SELECT idRole, nom " +
            "FROM roles " +
            "WHERE estJoueur = 0";
        List<Dictionary<string, string>> _roles = Selectionner(Commande);
        foreach (Dictionary<string, string> role in _roles)
        {
            Commande =
                "SELECT montant " +
                "FROM stats_roles " +
                "WHERE idRole=" + role["idRole"] +
                    " AND idStat=" + (int)Stats.PUI;
            List<Dictionary<string, string>> cmdRes = Selectionner(Commande);
            uint pui = uint.Parse(cmdRes[0]["montant"]);
            Commande =
                "SELECT montant " +
                "FROM stats_roles " +
                "WHERE idRole=" + role["idRole"] +
                    " AND idStat=" + (int)Stats.DEF;
            uint def = uint.Parse(Selectionner(Commande)[0]["montant"]);
            Commande =
                "SELECT montant " +
                "FROM stats_roles " +
                "WHERE idRole=" + role["idRole"] +
                    " AND idStat=" + (int)Stats.VIT;
            uint vit = uint.Parse(Selectionner(Commande)[0]["montant"]);

            Commande =
                "SELECT idCompetence " +
                "FROM competences_roles " +
                "WHERE idRole = " + role["idRole"];
            List<Dictionary<string, string>> comps = Selectionner(Commande);
            Competence CompBase = null;
            Competence CompSpec = null;

            foreach (Dictionary<string, string> comp in comps)
            {
                Competence competence = DonneesJeu.Competences[int.Parse(comp["idCompetence"])];

                if (competence.AttaqueBasique)
                {
                    CompBase = competence;
                }
                else
                {
                    CompSpec = competence;
                }
            }

            Roles.Add(int.Parse(role["idRole"]), new Role(int.Parse(role["idRole"]), role["nom"], pui, def, vit, CompBase, CompSpec));
        }

        return Roles;
    }
    static public Dictionary<int, Role> TrouverMonstres()
    {
        return moi._trouverMonstres();
    }

    private Dictionary<int, Equipe> _trouverEquipesMonstre()
    {
        Dictionary<int, Equipe> EquipesMonstres = new Dictionary<int, Equipe>();
        string Commande = "SELECT * FROM equipesmonstre";
        List<Dictionary<string, string>> EqMo = Selectionner(Commande);
        foreach(Dictionary<string, string> equipe in EqMo)
        {
            EquipesMonstres.Add(int.Parse(equipe["idEquipeMonstre"]), new Equipe(equipe["nom"]));
            //Debug.Log("Ajout Équipe #" + equipe["idEquipeMonstre"]);
        }
        Commande = "SELECT * FROM equipes_monstres";
        EqMo = Selectionner(Commande);
        foreach (Dictionary<string, string> equipe in EqMo)
        {
            //Debug.Log("Ajout Monstre # " + equipe["idMonstre"] + " au niveau " + equipe["niveau"] + " pour Équipe #" + equipe["idEquipe"]);
            Role monstre = DonneesJeu.Monstres[int.Parse(equipe["idMonstre"])];
            EquipesMonstres[int.Parse(equipe["idEquipe"])].AjoutMembre(new Personnage(monstre.Nom, monstre, uint.Parse(equipe["niveau"])));
        }
        return EquipesMonstres;
    }
    static public Dictionary<int, Equipe> TrouverEquipesMonstres()
    {
        return moi._trouverEquipesMonstre();
    }
}
