using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Mysterole;

partial class AccesBD
{
    //Requêtes pour récupérer toutes les quêtes
    private Dictionary<QueteParente, List<Quete>> _trouverToutesQuetes()
    {
        string Commande = "SELECT * FROM QuetesParentes;";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        Dictionary<QueteParente, List<Quete>> tq = new Dictionary<QueteParente, List<Quete>>();

        foreach (Dictionary<string, string> lst in result)
        {
            QueteParente qp = new QueteParente(int.Parse(lst["idQueteParente"]), lst["nom"]);
            List<Quete> lq = moi._trouverQuetes(qp);

            tq.Add(qp, lq);
        }

        return tq;
    }
    public static Dictionary<QueteParente, List<Quete>> TrouverToutesQuetes()
    {
        return moi._trouverToutesQuetes();
    }

    //Requêtes pour récupérer les quêtes d'une quête parente
    private List<Quete> _trouverQuetes(QueteParente qp)
    {
        string Commande = "SELECT * FROM Quetes WHERE idQueteParente = " + qp.ID.ToString() + ";";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<Quete> lq = new List<Quete>();

        foreach (Dictionary<string, string> lst in result)
        {
            Quete q = new Quete(int.Parse(lst["idQuete"]),
                int.Parse(lst["idEtatQuete"]),
                qp,
                lst["nom"],
                (lst["responsablePnj"] == "" ? 0 : int.Parse(lst["responsablePnj"])),
                lst["description"]
            );

            lq.Add(q);
        }

        return lq;
    }
    public static List<Quete> TrouverQuetes(QueteParente qp)
    {
        return moi._trouverQuetes(qp);
    }

    //Requêtes pour récupérer les objectifs d'une quête 
    private List<Objectif> _trouverObjectifs(Quete q)
    {
        string Commande = "SELECT * FROM Objectifs WHERE idQuete = " + q.ID.ToString() + ";";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<Objectif> lo = new List<Objectif>();

        foreach (Dictionary<string, string> lst in result)
        {
            TypeObjectif to = MethodesEnum.TrouverTypeObjectif(int.Parse(lst["idTypeObjectif"]));
            Objectif o;


            switch (to)
            {
                case TypeObjectif.Parler:
                    o = new ObjectifParler(int.Parse(lst["idObjectif"]), q, to, (lst["responsable"] == "" ? 0 : int.Parse(lst["responsable"])), lst["parametres"], bool.Parse(lst["estValide"]));
                    break;
                case TypeObjectif.Trouver:
                    o = new ObjectifTrouver(int.Parse(lst["idObjectif"]), q, to, (lst["responsable"] == "" ? 0 : int.Parse(lst["responsable"])), lst["parametres"], bool.Parse(lst["estValide"]));
                    break;
                case TypeObjectif.Battre:
                    o = new ObjectifBattre(int.Parse(lst["idObjectif"]), q, to, (lst["responsable"] == "" ? 0 : int.Parse(lst["responsable"])), lst["parametres"], bool.Parse(lst["estValide"]));
                    break;
                case TypeObjectif.Explorer:
                    o = new ObjectifExplorer(int.Parse(lst["idObjectif"]), q, to, (lst["responsable"] == "" ? 0 : int.Parse(lst["responsable"])), lst["parametres"], bool.Parse(lst["estValide"]));
                    break;
                case TypeObjectif.NULL:
                    o = new ObjectifParler();
                    break;
                default:
                    o = new ObjectifParler();
                    break;
            }

            lo.Add(o);
        }

        return lo;
    }
    public static List<Objectif> TrouverObjectifs(Quete q)
    {
        return moi._trouverObjectifs(q);
    }

    //Requêtes pour récupérer les gains d'une quête
    private List<Gain> _trouverGains(Quete q)
    {
        return new List<Gain>();
    }
    public static List<Gain> TrouverGains(Quete q)
    {
        return moi._trouverGains(q);
    }

    //Requêtes pour récupérer les cartes 
    private List<GameObject> _trouverCartes()
    {
        string Commande = "SELECT nom, position_x, position_y FROM Cartes;";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<GameObject> lc = new List<GameObject>();

        foreach (Dictionary<string, string> lst in result)
        {

            GameObject c = Instantiate(Resources.Load("Prefab/cartes/" + lst["nom"]), new Vector2(int.Parse(lst["position_x"]), int.Parse(lst["position_y"])), Quaternion.identity) as GameObject;

            if (c == null)
            {
                Debug.Log("carte " + lst["nom"] + " manquante");
                continue;
            }
            c.name = c.name.Replace("(Clone)", "");

            lc.Add(c);
        }

        return lc;
    }
    public static List<GameObject> TrouverCartes()
    {
        return moi._trouverCartes();
    }

    //Requêtes pour récupérer une carte
    private void _trouverCarte(Carte c)
    {
        string Commande = "SELECT * FROM Cartes WHERE nom = " + c.Nom + ";";
        List<Dictionary<string, string>> result = Selectionner(Commande);

        foreach (Dictionary<string, string> lst in result)
        {
            c.ID = int.Parse(lst["idCarte"]);
            c.Zone = MethodesEnum.TrouverZoneCarte(int.Parse(lst["idZoneCarte"]));
            c.EstHostile = (int.Parse(lst["estHostile"]) == 1 ? true : false);
        }

    }
    public static void TrouverCarte(Carte c)
    {
        moi._trouverCarte(c);
    }

    //Méthode pour trouver l'id de la carte
    private List<string> _trouverIdCarte(string n)
    {
        string Commande = "SELECT idCarte, estHostile FROM Cartes WHERE nom = '" + n + "'";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<string> li = new List<string>();

        li.Add(result[0]["idCarte"]);
        li.Add(result[0]["estHostile"]);



        return li;
    }
    public static List<string> TrouverIdCarte(string n)
    {
        return moi._trouverIdCarte(n);
    }

    //Requêtes pour récupérer les pnjs
    private List<GameObject> _trouverPnjs()
    {
        string Commande = "SELECT Pnjs.idPnj, nom, position_x, position_y, idQuete, idEtatQuete, idCinematique FROM Pnjs, PositionsPnjs WHERE Pnjs.idPnj = PositionsPnjs.idPnj ORDER BY idPnj, indexPosition DESC;";


        List<Dictionary<string, string>> result = Selectionner(Commande);



        List<GameObject> lp = new List<GameObject>();
        string nomActuel = "";
        bool estCharge = false;


        foreach (Dictionary<string, string> lst in result)
        {
            GameObject p;

            if (nomActuel == "")
                nomActuel = lst["nom"];

            if (estCharge && lst["nom"] == nomActuel)
                continue;

            if (lst["nom"] != nomActuel)
            {
                nomActuel = lst["nom"];
                estCharge = false;
            }

            if (lst["idCinematique"] != "")
            {
                if (GestionMonde.TrouverEtatCinematique(int.Parse(lst["idCinematique"])) == EtatCinematique.Terminee)
                {
                    p = Instantiate(Resources.Load("Prefab/Pnjs/" + lst["nom"]), new Vector2(float.Parse(lst["position_x"]), float.Parse(lst["position_y"])), Quaternion.identity) as GameObject;
                    p.GetComponent<Pnj>().ID = int.Parse(lst["idPnj"]);
                    lp.Add(p);
                    estCharge = true;
                    continue;
                }
            }
            else if (lst["idQuete"] != "" && lst["idEtatQuete"] != "")
            {
                if (GestionMonde.TrouverEtatQuete(int.Parse(lst["idQuete"])) >= MethodesEnum.TrouverEtatQuete(int.Parse(lst["idEtatQuete"])))
                {
                    p = Instantiate(Resources.Load("Prefab/Pnjs/" + lst["nom"]), new Vector2(float.Parse(lst["position_x"]), float.Parse(lst["position_y"])), Quaternion.identity) as GameObject;
                    p.GetComponent<Pnj>().ID = int.Parse(lst["idPnj"]);
                    lp.Add(p);
                    estCharge = true;
                    continue;
                }
            }
            else
            {
                p = Instantiate(Resources.Load("Prefab/Pnjs/" + lst["nom"]), new Vector2(float.Parse(lst["position_x"]), float.Parse(lst["position_y"])), Quaternion.identity) as GameObject;
                p.GetComponent<Pnj>().ID = int.Parse(lst["idPnj"]);
                lp.Add(p);
                estCharge = true;
                continue;
            }
        }

        return lp;
    }
    public static List<GameObject> TrouverPnjs()
    {
        return moi._trouverPnjs();
    }

    //Requête pour récupérer un pnj
    private void _trouverPnj(int id)
    {
        //string Commande = "SELECT * FROM Pnjs WHERE idPnj = " + id.ToString() + ";";
        //List<Dictionary<string, string>> result = Selectionner(Commande);
        ////GameObject p;

        //foreach (Dictionary<string, string> lst in result)
        //{

        //}

    }
    public static void TrouverPnj(int id)
    {
        moi._trouverPnj(id);
    }

    //Méthodes pour récupérer l'id d'un pnj
    private int _trouverIdPnj(string n)
    {
        string Commande = "SELECT idPnj FROM Pnjs WHERE nom = '" + n + "';";
        List<Dictionary<string, string>> result = Selectionner(Commande);

        return int.Parse(result[0]["idPnj"]);
    }
    public static int TrouverIdPnj(string n)
    {
        return moi._trouverIdPnj(n);
    }

    //Requêtes pour récupérer les cinématiques
    private List<Cinematique> _trouverCinematiques()
    {
        string Commande = "SELECT * FROM Cinematiques;";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<Cinematique> lc = new List<Cinematique>();

        foreach (Dictionary<string, string> lst in result)
        {
            Cinematique c = new Cinematique(
                int.Parse(lst["idCinematique"]),
                MethodesEnum.TrouverEtatCinematique(int.Parse(lst["idEtatCinematique"])),
                lst["nom"],
                (lst["idObjectif"] != "" ? int.Parse(lst["idObjectif"]) : 0),
                (lst["idQuete"] != "" ? int.Parse(lst["idQuete"]) : 0)
            );

            lc.Add(c);
        }

        return lc;
    }
    public static List<Cinematique> TrouverCinematiques()
    {
        return moi._trouverCinematiques();
    }

    //Requêtes pour récupérer les étapes d'une cinématique
    private List<Etape> _trouverEtapes(Cinematique c)
    {
        string Commande = "SELECT * FROM EtapesCinematiques WHERE idCinematique = " + c.ID.ToString() + " ORDER BY position;";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<Etape> le = new List<Etape>();

        foreach (Dictionary<string, string> lst in result)
        {
            Etape e = new Etape(
                int.Parse(lst["idEtapeCinematique"]),
                int.Parse(lst["idCinematique"]),
                int.Parse(lst["idEtatEtape"]),
                GestionMonde.TrouverCarte(int.Parse(lst["idCarte"])),
                int.Parse(lst["position"]),
                new Vector2(int.Parse(lst["positionJoueur_x"]), int.Parse(lst["positionJoueur_y"]))
            );

            le.Add(e);
        }

        return le;
    }
    public static List<Etape> TrouverEtapes(Cinematique c)
    {
        return moi._trouverEtapes(c);
    }

    //Requêtes pour récupérer les pnjs d'une étape et ses événements 
    private Dictionary<int, Evenement> _trouverEvenementsCinematiques(Etape e)
    {
        string Commande = "SELECT * FROM EvenementsCinematiques WHERE idEtapeCinematique = " + e.ID + " ORDER BY position;";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        //List<Evenement> le = new List<Evenement>();
        Dictionary<int, Evenement> lpe = new Dictionary<int, Evenement>();

        

        foreach (Dictionary<string, string> lst in result)
        {
            int a;

            if (lst["idPnj"] == "")
                a = 0;
            else
                a = int.Parse(lst["idPnj"]);


            TypeEvenement te = MethodesEnum.TrouverTypeEvenement(int.Parse(lst["idTypeEvenement"]));

            switch (te)
            {
                case TypeEvenement.Dialogue:
                    Debug.Log(lst["idPnj"]);
                    lpe[a] = new Dialogue(int.Parse(lst["idEvenementCinematique"]), e, te, bool.Parse(lst["peutContinuer"]), lst["contenu"]);
                    break;
                case TypeEvenement.Deplacement:
                    lpe[a] = new Deplacement(int.Parse(lst["idEvenementCinematique"]), e, te, bool.Parse(lst["peutContinuer"]), lst["contenu"]);
                    break;
                case TypeEvenement.Attente:
                    lpe[a] = new Attente(int.Parse(lst["idEvenementCinematique"]), e, te, bool.Parse(lst["peutContinuer"]), lst["contenu"]);
                    break;
            }
        }

        return lpe;
    }
    public static Dictionary<int, Evenement> TrouverEvenementsCinematiques(Etape e)
    {
        return moi._trouverEvenementsCinematiques(e);
    }

    //Requêtes pour récupérer un acteur temporaire
    private GameObject _trouverActeur(int id)
    {
        string Commande = "SELECT * FROM Pnjs WHERE idPnj = " + id;
        List<Dictionary<string, string>> result = Selectionner(Commande);
        GameObject a;


        a = Instantiate(Resources.Load("Prefab/Pnjs/" + result[0]["nom"]), new Vector2(), Quaternion.identity) as GameObject;

        return a;
    }
    public static GameObject TrouverActeur(int id)
    {
        return moi._trouverActeur(id);
    }
    private Vector2 _trouverPositionActeur(int idPnj, int idEtape)
    {
        string Commande = "SELECT * FROM PositionsActeurs WHERE idPnj = " + idPnj + " AND idEtapeCinematique = " + idEtape;
        List<Dictionary<string, string>> result = Selectionner(Commande);

        
        return new Vector2(float.Parse(result[0]["position_x"]), float.Parse(result[0]["position_y"]));

    }
    public static Vector2 TrouverPositionActeur(int idPnj, int idEtape)
    {
        return moi._trouverPositionActeur(idPnj, idEtape);
    }


    //Requêtes pour récupérer les événements passifs du pnj
    private List<Evenement> _trouverEvenementsPassifs(Pnj p)
    {
        string Commande = "SELECT * FROM EvenementsPassifs WHERE idPnj = " + p.ID.ToString() + " ORDER BY position;";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<Evenement> le = new List<Evenement>();
    
        foreach(Dictionary<string, string> lst in result)
        {
            TypeEvenement te = MethodesEnum.TrouverTypeEvenement(int.Parse(lst["idTypeEvenement"]));
            Evenement e;
            //int id, Etape e, TypeEvenement te, bool pc, string param
            switch (te)
            {
                case TypeEvenement.Dialogue:
                    e = new Dialogue(int.Parse(lst["idEvenementPassif"]), null, te, false, lst["contenu"]);
                    break;
                case TypeEvenement.Deplacement:
                    e = new Deplacement(int.Parse(lst["idEvenementPassif"]), null, te, false, lst["contenu"]);
                    break;
                case TypeEvenement.Attente:
                    e = new Attente(int.Parse(lst["idEvenementPassif"]), null, te, false, lst["contenu"]);
                    break;
                case TypeEvenement.NULL:
                    e = new Attente(0, te);
                    break;
                default:
                    e = new Attente(0, te);
                    break;
            }

            le.Add(e);
        }

        return le;
    }
    public static List<Evenement> TrouverEvenementsPassifs(Pnj p)
    {
        return moi._trouverEvenementsPassifs(p);
    }

    //Requêtes pour récupérer les événements actifs du pnj
    private List<Evenement> _trouverEvenementsActifs(Pnj p)
    {
        string Commande = "SELECT * FROM EvenementsActifs WHERE idPnj = " + p.ID.ToString() + " ORDER BY position;";
        List<Dictionary<string, string>> result = Selectionner(Commande);
        List<Evenement> le = new List<Evenement>();

        foreach (Dictionary<string, string> lst in result)
        {
            TypeEvenement te = MethodesEnum.TrouverTypeEvenement(int.Parse(lst["idTypeEvenement"]));
            Evenement e;
            //int id, Etape e, TypeEvenement te, bool pc, string param
            switch (te)
            {
                case TypeEvenement.Dialogue:
                    e = new Dialogue(int.Parse(lst["idEvenementActif"]), te, int.Parse(lst["position"]), lst["contenu"]);
                    break;
                case TypeEvenement.Deplacement:
                    e = new Deplacement(int.Parse(lst["idEvenementActif"]), te, int.Parse(lst["position"]), lst["contenu"]);
                    break;
                case TypeEvenement.Attente:
                    e = new Attente(int.Parse(lst["idEvenementActif"]), te, int.Parse(lst["position"]), lst["contenu"]);
                    break;
                case TypeEvenement.NULL:
                    e = new Attente(0, te);
                    break;
                default:
                    e = new Attente(0, te);
                    break;
            }

            le.Add(e);
        }

        return le;
    }
    public static List<Evenement> TrouverEvenementsActifs(Pnj p)
    {
        return moi._trouverEvenementsActifs(p);
    }





}









//Dictionary<string, string>.Enumerator enumlol = lst.GetEnumerator();
//GameObject p;

//string Commande2 = "SELECT * FROM PositionsPnjs WHERE idPnj = " + lst["idPnj"] + " ORDER BY indexPosition DESC;";
//List<Dictionary<string, string>> result2 = Selectionner(Commande2);

//Debug.Log(result2);

//foreach (Dictionary<string, string> lst2 in result2)
//{

//    if (lst["idCinematique"] != null)
//    {
//        if (GestionMonde.TrouverEtatCinematique(int.Parse(lst["idCinematique"])) == EtatCinematique.Terminee)
//        {
//            p = Instantiate(Resources.Load("Prefab/Pnjs/" + lst["nom"]), new Vector2(float.Parse(lst2["position_x"]), float.Parse(lst2["position_y"])), Quaternion.identity) as GameObject;
//            lp.Add(p);
//            break;
//        }
//    }
//    else if (lst["idQuete"] != null && lst["idEtatQuete"] != null)
//    {
//        if (GestionMonde.TrouverEtatQuete(int.Parse(lst["idQuete"])) >= MethodesEnum.TrouverEtatQuete(int.Parse(lst["idEtatQuete"])))
//        {
//            p = Instantiate(Resources.Load("Prefab/Pnjs/" + lst["nom"]), new Vector2(float.Parse(lst2["position_x"]), float.Parse(lst2["position_y"])), Quaternion.identity) as GameObject;
//            lp.Add(p);
//            break;
//        }
//    }
//    else
//    {
//        p = Instantiate(Resources.Load("Prefab/Pnjs/" + lst["nom"]), new Vector2(float.Parse(lst2["position_x"]), float.Parse(lst2["position_y"])), Quaternion.identity) as GameObject;
//        lp.Add(p);
//        break;
//    }
//}