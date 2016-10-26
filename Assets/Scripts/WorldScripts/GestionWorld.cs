using UnityEngine;
using System.Collections;
using Tiled2Unity;
using Mysterole;
using System.Collections.Generic;


public class GestionWorld : MonoBehaviour {


    private Transform player;
    private static TiledMap _map;
    private int coefCombat = 10;
    public System.Random ran;

    private List<Equipe> _monstres = new List<Equipe>();


    public static bool testQuete = false;


    private static bool _isActive = false;
    private float tempsCombat = 1.0f;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();

        string sb = "map" + (int)(player.position.x / 50) + (int)(player.position.y / -50);
        _map = GameObject.Find(sb).GetComponent<TiledMap>();
        
        _monstres.Add(DonneesJeu.EquipesMonstre[0]);

        ran = new System.Random();



    }
	
	// Update is called once per frame
	void Update () {



            

        //int x = ran.Next(0, coefCombat);
        //int y = ran.Next(0, 1);

        //Debug.Log(_isActive);
        //if (x == y && _isActive)
        //    Debug.Log("combat déclenché");

        //activeRan = ran.Next(0, 101);
        //if (activeRan < coefCombat)
        //    Debug.Log("combat déclenché : " + activeRan);

        //UpdateCoef();
        

        //Uniformiser les données de map pour qu'elles permettent de gérer le déclenchement des combats et des quêtes
        switch(_map.gameObject.name)
        {
            case "map00":
            case "map23":
                _isActive = false;
                break;
            default:

                if (_isActive)
                    tempsCombat -= Time.deltaTime;


                if (tempsCombat < 1 && !GestTransition.EnTransition)
                {
                    Equipe unGroupe = _monstres[Random.Range(0, _monstres.Count)];
                    DonneesJeu.Adversaires = unGroupe;
                    GameObject.Find("Player").GetComponent<PlayerMovement>().CanMove = false;
                    GestScene.ProchaineSceneTransition("Combat");
                }

                break;
        }
	}


    public static void UpdateCoef(bool isActive)
    {
        //Debug.Log(tempsCombat -= Time.deltaTime);

        _isActive = isActive;

    }

    public static InfosWorld GetInfos()
    {
        return new InfosWorld("combat" + Random.Range(0,1), PlayerMovement.GetPlayer());
    }

    public static void UpdateMap(TiledMap map)
    {
        _map = map;
    }

}
