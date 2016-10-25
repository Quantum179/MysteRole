using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
    public class IA : MonoBehaviour
    {
        //private Dictionary<bool, Deplacement> _deplacements;
        //public Dictionary<bool, Deplacement> Deplacements { get; private set; }

        //private List<Quete> _quetes;
        //public List<Quete> Quetes { get; private set; }

        private List<Dialogue> _dialogues;
        public List<Dialogue> Dialogues { get { return _dialogues; } private set { } }

        private List<Deplacement> _deplacements;
        public List<Deplacement> Deplacements { get { return _deplacements; } private set { } }

        private List<Evenement> _evenements;
        public List<Evenement> Evenements { get { return _evenements; } private set { } }
        

        private string _nomPnj;
        public string NomPnj { get; private set; }




        //A optimiser
        private Rigidbody2D rbody;
        private Animator anim;
        private Vector2 offset;
        private GameObject player;
        //private int velocity = 10;
        private int index = 0;
        private int indexAuto = 0;

        private bool _canEvent = false;
        public bool CanEvent { get; set; }
        private bool _isEvent = false;
        public bool IsEvent { get; set; }

        private bool _isWalking = false;
        private float tempsIdle = 5.0f;


        ////Constructeur
        //public IA(string nomPnj)
        //{
            
        //}


        //public IA()
        void Start()
        {
            //Initialisation des données avec des requêtes SQL
            //Hardcode

            //Connexion au Game Object du pnj
            //TODO : générer les dessins (sprites et collisions) automatiquement à partir des informations de la BD

            rbody = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            _evenements = new List<Evenement>();
            _dialogues = new List<Dialogue>();
            _deplacements = new List<Deplacement>();
            _nomPnj = gameObject.name;



            switch (_nomPnj)
            {
                case "Chef milicien":
                    _evenements.Add(new Dialogue("Halte là"));
                    _evenements.Add(new Deplacement(131,-175));
                    _evenements.Add(new Dialogue("Vous ne pouvez pas entrer dans la cité !"));
                    _evenements.Add(new Dialogue("Revenez avec une autorisation"));
                    _evenements.Add(new Deplacement(135, -175));


                    _dialogues.Add(new Dialogue("Vous ne passerez pas tant que je ne verrai pas d'autorisation"));

                    //ajouter quêtes
                    break;
                case "Sage du village":
                    _evenements.Add(new Dialogue("Bonjour jeune aventurier."));
                    _evenements.Add(new Dialogue("Voici le papier t'autorisant à entrer dans la cité"));

                    _deplacements.Add(new Deplacement(34, -10));
                    _deplacements.Add(new Deplacement(38, -10));
                    break;
                case "Milicien":

                    break;
            }
        }







        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    StartCoroutine(TriggerEvent(true));
            //}

            if(index < _evenements.Count)
                RunEvent();

            if(index < _evenements.Count)
            {
                switch (_evenements[index].GetTypeEvent())
                {
                    case "Dialogue":

                        if (Input.GetKeyDown(KeyCode.H) && _isEvent)
                        {
                            EcranDialogue.closeDialog();
                            _isEvent = false;
                            _canEvent = true;
                            index++;
                        }
                        break;
                    case "Deplacement":
                        Deplacement unDeplacement = (Deplacement)_evenements[index];

                        if (_isEvent)
                        {
                            offset = new Vector2(unDeplacement.Destination.x - rbody.position.x, unDeplacement.Destination.y - rbody.position.y);

                            anim.SetBool("isWalking", true);
                            anim.SetFloat("input_x", offset.x);
                            anim.SetFloat("input_y", offset.y);

                            //rbody.MovePosition(rbody.position + offset * 10 * Time.deltaTime);

                            transform.position = Vector3.MoveTowards(transform.position, unDeplacement.Destination, Time.deltaTime * 5);
                        }

                        if (rbody.position == unDeplacement.Destination)
                        {
                            _isEvent = false;
                            _canEvent = true;
                            anim.SetBool("isWalking", false);
                            anim.SetFloat("input_x", -1);
                            index++;
                        }
                        break;
                    default:
                        break;
                }

                if (index == _evenements.Count)
                    GameObject.Find("Player").GetComponent<PlayerMovement>().CanMove = true;     
            }


            tempsIdle -= Time.deltaTime;
            if (tempsIdle < 1)
            {
                _isWalking = true;
            }




            if(_nomPnj == "Sage du village")
            {
                if (_isWalking)
                {
                    offset = new Vector2(_deplacements[indexAuto].Destination.x - rbody.position.x, _deplacements[indexAuto].Destination.y - rbody.position.y);

                    anim.SetBool("isWalking", true);
                    anim.SetFloat("input_x", offset.x);
                    anim.SetFloat("input_y", offset.y);
                    _canEvent = false;

                    //rbody.MovePosition(rbody.position + offset * 10 * Time.deltaTime);

                    transform.position = Vector3.MoveTowards(transform.position, _deplacements[indexAuto].Destination, Time.deltaTime * 5);
                    Debug.Log(_deplacements[indexAuto].ToString() + indexAuto.ToString());
                    
                }

                if (rbody.position == _deplacements[indexAuto].Destination && _canEvent)
                {
                    _isWalking = false;
                    _canEvent = true;
                    anim.SetBool("isWalking", false);
                    anim.SetFloat("input_x", -1);

                    if (indexAuto == 0)
                        indexAuto = 1;
                    else
                        indexAuto = 0;
                }

            }
        }


        //Event
        private void RunEvent()
        {
            if (!_isEvent && _canEvent && index < _evenements.Count())
            {

                _isEvent = true;

                switch (_evenements[index].GetTypeEvent())
                {
                    case "Dialogue":
                        
                        Dialogue unDialogue = (Dialogue)_evenements[index];
                        EcranDialogue.NewDialog(_nomPnj, unDialogue.Message);
                        _canEvent = false;
                        break;
                    case "Deplacement":
                        break;
                    default:
                        Debug.Log("erreur event, " + index.ToString());
                        break;
                }
            }
        }

        public bool StartEvent()
        {

            _canEvent = true;
            return true;

        }

        public static void SetEvent()
        {

        }


    }
}
