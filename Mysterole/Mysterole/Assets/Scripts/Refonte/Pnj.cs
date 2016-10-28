using System;
using System.Collections.Generic;

namespace Mysterole
{
	public class Pnj
	{
        //Attributs
        protected string _nom;
        protected List<Quete> _quetes;
        //protected List<Objectif> _objectifs;
        protected Dictionary<TypeEvenement , Dictionary<int, Evenement>> _evenements;


        /*
        concepts d'attributs à tester avec UnityEngine

            protected isEvent;
        */

        //Propriétés
        public string Nom
        {
            get { return _nom; }
            private set { }

        }
        public List<Quete> Quetes
        {
            get { return _quetes; }
            private set { }
        }
        /*
        public List<Objectif> Objectifs
        {
            get { return _objectifs;}
            private set { }
        }
        */
		public Dictionary<TypeEvenement, Dictionary<int, Evenement>> Evenements 
		{
			get { return _evenements; }
            private set { _evenements = value; }

		}

        //Constructeurs
		public Pnj ()
		{
            _nom = null;
            _quetes = new List<Quete>();
            _evenements = new Dictionary<TypeEvenement, Dictionary<int, Evenement>>();
		}
        public Pnj(string n)
        {
            _nom = n;
            //Faire la requête pour récupérer les quêtes reliées au pnj
            //En déduire la requête pour récupérer les événements liés aux quêtes du pnj
        }
             
        //public Pnj(string n, List<Quete> lq, Dictionary<string, Dictionary<TypeEvenement, Evenement>>)





        //Méthodes
        private void InitialiserDonnees()
        {
           //requêtes à la BD
        }

        public void VerifierAvanceeQuete()
        {
            List<Quete>.Enumerator enumQuete = _quetes.GetEnumerator();

            while(enumQuete.MoveNext())
            {
                //TODO : Diviser tout ces blocs en switch si possible
                //TODO : rajouter la gestion d'ordre des quêtes (bloquer les quêtes indisponibles)
                
                
                //Démarrage de la quête par le responsable 
                if(enumQuete.Current.ResponsableDebut == _nom && enumQuete.Current.Etat == EtatQuete.Disponible)
                {
                    //Démarrer la quête aussi bien dans la liste de quete que dans la gestion des événements
                    enumQuete.Current.DemarrerQuete();
                    DeclencherEvenements();
                    //_evenements[TypeEvenement.Quete].GetEnumerator().MoveNext(); //TODO : gérer les événements à l'activation
                    break;
                }
                
                //Validation de l'avanceée de la quête 
                if(enumQuete.Current.Etat == EtatQuete.EnCours)
                {
                    List<Objectif>.Enumerator enumObjectif = enumQuete.Current.Objectifs.GetEnumerator();

                    /*if(!enumObjectif.Current)
                    {
                        //On démarre les objectifs de la quête

                    }*/

                     if(enumObjectif.Current.Responsable == _nom)
                     {
                         //On vérifie que tous les pré-requis sont validés
                         if(enumObjectif.Current.ValiderObjectif())
                         {
                             enumObjectif.MoveNext();
                             //Pop-up d'objectif validé
                         }
                         else
                         {
                             //TODO : gérer le pattern du pnj à adopter selon l'avancée des quêtes qui le concerne
                         }
                     }     
                }
            }
        }




        public void DeclencherEvenements()
        {
            Dictionary<int, Evenement>.Enumerator enumEventQuete = _evenements[TypeEvenement.Quete].GetEnumerator();

            enumEventQuete.MoveNext();


            

                
                
                //TODO : gérer les événements à l'activation*


            //ELSE : Evenements par défaut
            


        }

        private void DeclencherEvenement(Evenement e)
        {

        }




    }
}










///////////////////////////////////////////////////////////




//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using Mysterole;
//using System.Text;

//public class Pnj : MonoBehaviour
//{

//    private Rigidbody2D rbody;
//    private Animator anim;
//    private Vector2 offset;
//    private int velocity = 10;
//    public string nomPNJ;

//    private IA _ia;

//    private int index = 0;

//    private bool isMoving = false;

//    // Use this for initialization
//    void Start()
//    {


//        rbody = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();


//        //dataPnj = new Dictionary<string ,Evenement>();

//        //dataPnj.Add("", new Dialogue("Halte là !"));
//        //dataPnj.Add("", new Deplacement(115, -176));
//        //dataPnj.Add("", new Dialogue("Vous n'avez pas le droit d'entrer ici !"));

//        _ia = new IA("Chef Milicien");


//    }

//    // Update is called once per frame
//    void Update()
//    {


//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            StartCoroutine(RunEvent(true));
//        }


//        if (dataPnj[index].GetTypeEvent().ToString() == "Dialogue")
//        {
//            if (Input.GetKeyDown(KeyCode.T))
//            {
//                EcranDialogue.closeDialog();

//                index++;
//            }
//        }


//        if (dataPnj[index].GetTypeEvent().ToString() == "Deplacement")
//        {
//            if (Input.GetKeyDown(KeyCode.E))
//            {
//                isMoving = true;
//            }

//            //if (Input.GetKeyDown(KeyCode.B))
//            //{
//            //    Debug.Log(rbody.position);
//            //    //Debug.Log(((Deplacement)dataPnj[index]).Destination);
//            //    //Debug.Log(rbody.position - ((Deplacement)dataPnj[index]).Destination);

//            //}


//            if (isMoving)
//            {

//                offset = new Vector2(((Deplacement)dataPnj[index]).Destination.x - rbody.position.x,
//                          ((Deplacement)dataPnj[index]).Destination.y - rbody.position.y
//                        );


//                //Debug.Log(rbody.position.x);
//                //Debug.Log(((Deplacement)dataPnj[index]).Destination);
//                anim.SetBool("isWalking", true);
//                anim.SetFloat("input_x", offset.x);
//                anim.SetFloat("input_y", offset.y);

//                //rbody.MovePosition(rbody.position + offset * 10 * Time.deltaTime);

//                transform.position = Vector3.MoveTowards(transform.position, ((Deplacement)dataPnj[index]).Destination, Time.deltaTime * 5);




//            }

//            if (rbody.position == ((Deplacement)dataPnj[index]).Destination)
//            {
//                isMoving = false;
//                anim.SetBool("isWalking", false);
//                index++;

//            }
//        }






//    }

//    public void RunEvent(bool canEvent)
//    {
//        //if(dataPnj[index].GetType() == typeof(Dialogue))
//        //{

//        //}
//        //else if(dataPnj[index])

//        // START EVENT
//        // Player Cannot Move
//        // PlayerMovement.canMove = false;

//        // List<Evenement>.Enumerator e = dataPnj.GetEnumerator();

//        //switch (dataPnj[index].GetTypeEvent().ToString()) // while (e.MoveNext())
//        //{
//        //    // e.Current.RunEvent();
//        //    case "Dialogue":

//        //        EcranDialogue.NewDialog(this.gameObject.name, ((Dialogue)dataPnj[index]).Message);

//        //        // yield return StartCoroutine(FUNCTION EVENT);
//        //        //dataPnj[index].RunEvent()
//        //        break;
//        //    case "Deplacement":
//        //        isMoving = true;
//        //        break;
//        //}

//        // PlayerMovement.canMove = true;
//    }

//}
