using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Mysterole
{
    public class EcranQuete : MonoBehaviour
    {
        static EcranQuete Main;



        private static bool estActif = false;
        public static bool EstActif { get { return estActif; } }
        private static ColorBlock colors;
        private Button firstFocus;

        private Image lblPrincipales;
        private Image lblAnnexes;
        private Image lblToutes;
        private Image lblDisponibles;
        private Image lblEnCours;
        private Image lblTerminees;

        private static GameObject lstQuetesParentes;
        private static GameObject lstQuetesEnfants;





        private Text txtNomQuete;
        private Text txtResponsable;
        private Text txtEtatQuete;
        private Text txtDescription;

        private static GameObject gains;
        private static GameObject lstObjectifs;
        private static GameObject lstPrerequis;




        void Start()
        {
            Main = this;


            //AccueilQueteGauche
            //lblPrincipales = GameObject.Find("lblPrincipales").GetComponent<Image>();
            //lblAnnexes = GameObject.Find("lblAnnexes").GetComponent<Image>();
            //lblToutes = GameObject.Find("lblToutes").GetComponent<Image>();
            //lblDisponibles = GameObject.Find("lblDisponibles").GetComponent<Image>();
            //lblEnCours = GameObject.Find("lblEnCours").GetComponent<Image>();
            //lblTerminees = GameObject.Find("lblTerminees").GetComponent<Image>();

            lstQuetesParentes = GameObject.Find("lstQuetesParentes");
            lstQuetesEnfants = GameObject.Find("lstQuetesEnfants");


            //lblPrincipales.color = Color.yellow;


            //AccueilQueteDroite

            txtNomQuete = GameObject.Find("txtNomQuete").GetComponent<Text>();
            txtResponsable = GameObject.Find("txtResponsable").GetComponent<Text>();
            txtEtatQuete = GameObject.Find("txtEtatQuete").GetComponent<Text>();
            txtDescription = GameObject.Find("txtDescription").GetComponent<Text>();

            lstObjectifs = GameObject.Find("lstObjectifs");
            lstPrerequis = GameObject.Find("lstPrerequis");


            //lblToutes.color = Color.yellow;
            gameObject.SetActive(false);

        }


        public static void OuvrirEcranQuete(string onglet)
        {
            Reset();

            //Faire le mappage
            //GameObject menu = 
            Main.gameObject.SetActive(true);
            estActif = true;

            foreach (Transform t in Main.gameObject.transform)
            {
                if (t.name == "Accueil")
                    t.gameObject.SetActive(true);
                if (t.name == "DetailsQuete")
                    t.gameObject.SetActive(false);
            }

            JoueurMonde.PeutAgir = false;

            Button oldBtn = null;
            bool isFirstKey = true;
            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();



            //On ajoute les quêtes parentes à la liste de l'interface
            foreach (QueteParente key in GestionMonde.Quetes.Keys)
            {
                GameObject btnQueteParente = Instantiate(Resources.Load("Prefab/btnQueteParente")) as GameObject;
                btnQueteParente.transform.SetParent(lstQuetesParentes.transform);
                btnQueteParente.name = key.Nom;
                btnQueteParente.GetComponentInChildren<Text>().text = key.Nom;

                //
                if (isFirstKey)
                {
                    //Main.firstFocus = btnQueteParente.GetComponent<Button>();
                    es.SetSelectedGameObject(btnQueteParente);
                    isFirstKey = false;
                }

                //On initialise la navigation du bouton
                Navigation nav = btnQueteParente.GetComponent<Button>().navigation;
                if (oldBtn != null)
                {
                    nav.selectOnUp = oldBtn;
                    Navigation oldNav = oldBtn.navigation;
                    oldNav.selectOnDown = btnQueteParente.GetComponent<Button>();
                    btnQueteParente.GetComponent<Button>().navigation = nav;
                    oldBtn.navigation = oldNav;
                }
                oldBtn = btnQueteParente.GetComponent<Button>();



                btnQueteParente.GetComponent<Button>().onClick.AddListener( delegate { Main.ChargerQuetesEnfants(btnQueteParente.gameObject.name); });
                //btnQueteParente.GetComponent<Button>().onClick.AddListener(delegate { Main.ChargerQuetesEnfants(key); });
                //btnQueteParente.GetComponent<Button>().onClick.AddListener( () => { Main.ChargerQuetesEnfants(key); });
            }

            //Faire une validation pour savoir quel est l'onglet à afficher et ainsi réinitialiser l'écran 

        }


        private void ChargerQuetesEnfants(string key)
        {
            bool isFirstKey = true;
            Button oldBtn = null;
            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            int index = 0;

            QueteParente qp = GestionMonde.TrouverQueteParente(key);

            foreach (Quete quete in GestionMonde.Quetes[qp])
            {
                GameObject btnQueteEnfant = Instantiate(Resources.Load("Prefab/btnQueteEnfant")) as GameObject;
                btnQueteEnfant.transform.SetParent(lstQuetesEnfants.transform);
                //Debug.Log(lstQuetesEnfants.transform.childCount);
                btnQueteEnfant.name = quete.Nom;
                btnQueteEnfant.GetComponentInChildren<Text>().text = quete.Nom;

                if (isFirstKey)
                {
                    //Main.firstFocus = btnQueteParente.GetComponent<Button>();
                    es.SetSelectedGameObject(btnQueteEnfant);
                    isFirstKey = false;

                }

                //On initialise la navigation du bouton
                Navigation nav = btnQueteEnfant.GetComponent<Button>().navigation;
                if (oldBtn != null)
                {
                    nav.selectOnUp = oldBtn;
                    Navigation oldNav = oldBtn.navigation;
                    oldNav.selectOnDown = btnQueteEnfant.GetComponent<Button>();
                    btnQueteEnfant.GetComponent<Button>().navigation = nav;
                    oldBtn.navigation = oldNav;
                }
                oldBtn = btnQueteEnfant.GetComponent<Button>();

                btnQueteEnfant.GetComponent<Button>().onClick.AddListener(delegate { EcranQuete.MontrerDetailsQuete(es.currentSelectedGameObject.name); });
                index++;
            }

        }




        public static void FermerEcranQuete()
        {
            estActif = false;
            Main.gameObject.SetActive(false);
            JoueurMonde.PeutAgir = true;
        }

        private static void Reset()
        {
            foreach (Button child in lstQuetesParentes.GetComponentsInChildren<Button>())
                Destroy(child.gameObject);
            foreach (Button child in lstQuetesEnfants.GetComponentsInChildren<Button>())
                Destroy(child.gameObject);
            foreach (Button child in lstObjectifs.GetComponentsInChildren<Button>())
                Destroy(child.gameObject);
            foreach (Image child in lstPrerequis.GetComponentsInChildren<Image>())
                Destroy(child.gameObject);
        }

        public static void MontrerDetailsQuete(string nomQuete)
        {

            Reset();
            Main.gameObject.SetActive(true);
            bool isFirstKey = true;
            Button oldBtn = null;
            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();


            //On ferme le panel d'accueil pour ouvrir celui des détails d'une quête
            foreach (Transform t in Main.gameObject.transform)
            {
                if (t.name == "DetailsQuete")
                    t.gameObject.SetActive(true);
                if (t.name == "Accueil")
                    t.gameObject.SetActive(false);
            }


            //List<Quete>.Enumerator enumQuete = GestionMonde.Quetes[GestionMonde.TrouverQueteParente("L'embuscade fatidique")].GetEnumerator();
            //int compteur = 0;
            //while (enumQuete.MoveNext())
            //{
            //    if(nomQuete == enumQuete.Current.Nom)
            //    {
            //        laQuete = enumQuete.Current;
            //        compteur++;
            //    }
            //}

            Quete laQuete = GestionMonde.TrouverQuete(nomQuete);


            Main.txtNomQuete.text = laQuete.Nom;
            Main.txtResponsable.text = (laQuete.ResponsablePnj == 0 ? "Automatique" : GestionMonde.TrouverPnj(laQuete.ResponsablePnj).name);
            Main.txtEtatQuete.text = laQuete.Etat.ToString();
            Main.txtDescription.text = laQuete.Description;



            foreach (Objectif obj in laQuete.Objectifs)
            {
                GameObject btnObjectif = Instantiate(Resources.Load("Prefab/btnObjectif"), lstObjectifs.transform) as GameObject;
                btnObjectif.name = obj.Description;
                btnObjectif.GetComponentInChildren<Text>().text = obj.Description;

                if (isFirstKey)
                {
                    //Main.firstFocus = btnQueteParente.GetComponent<Button>();
                    es.SetSelectedGameObject(btnObjectif);
                    isFirstKey = false;
                }

                //On initialise la navigation du bouton
                Navigation nav = btnObjectif.GetComponent<Button>().navigation;
                if (oldBtn != null)
                {
                    nav.selectOnUp = oldBtn;
                    Navigation oldNav = oldBtn.navigation;
                    oldNav.selectOnDown = btnObjectif.GetComponent<Button>();
                    btnObjectif.GetComponent<Button>().navigation = nav;
                    oldBtn.navigation = oldNav;
                }
                oldBtn = btnObjectif.GetComponent<Button>();


                //btnObjectif.GetComponent<Button>().onClick.AddListener(delegate { Main.ChargerPrerequis(obj.Prerequis); });

                if(obj.EstValide)
                {
                    colors = btnObjectif.GetComponent<Button>().colors;
                    colors.normalColor = Color.green;
                    btnObjectif.GetComponent<Button>().colors = colors;
                }
                



            }

            JoueurMonde.PeutAgir = false;
            estActif = true;
        }
        public static void MontrerDetailsQuete(Quete q)
        {
            Reset();
            bool isFirstKey = true;
            Button oldBtn = null;
            EventSystem es = GameObject.Find("EventSystem").GetComponent<EventSystem>();


            //On ferme le panel d'accueil pour ouvrir celui des détails d'une quête
            Main.gameObject.SetActive(true);

            //Debug.Log(Main.gameObject.transform.childCount);


            foreach(Transform t in Main.gameObject.transform)
            {
                if (t.name == "DetailsQuete")
                    t.gameObject.SetActive(true);
                if (t.name == "Accueil")
                    t.gameObject.SetActive(false);
            }


            //GameObject.Find("Accueil").SetActive(false);
            //GameObject.Find("DetailsQuete").SetActive(true);


            Main.txtNomQuete.text = q.Nom;
            Main.txtResponsable.text = GestionMonde.TrouverPnj(q.ResponsablePnj).name;
            Main.txtEtatQuete.text = q.Etat.ToString();
            Main.txtDescription.text = q.Description;



            foreach(Objectif obj in q.Objectifs)
            {
                GameObject btnObjectif = Instantiate(Resources.Load("Prefab/btnObjectif")) as GameObject;
                btnObjectif.transform.SetParent(lstObjectifs.transform);
                btnObjectif.name = obj.Description;
                btnObjectif.GetComponentInChildren<Text>().text = obj.Description;

                if (isFirstKey)
                {
                    //Main.firstFocus = btnQueteParente.GetComponent<Button>();
                    es.SetSelectedGameObject(btnObjectif);
                    isFirstKey = false;
                }

                //On initialise la navigation du bouton
                Navigation nav = btnObjectif.GetComponent<Button>().navigation;
                if (oldBtn != null)
                {
                    nav.selectOnUp = oldBtn;
                    Navigation oldNav = oldBtn.navigation;
                    oldNav.selectOnDown = btnObjectif.GetComponent<Button>();
                    btnObjectif.GetComponent<Button>().navigation = nav;
                    oldBtn.navigation = oldNav;
                }
                oldBtn = btnObjectif.GetComponent<Button>();


                //btnObjectif.GetComponent<Button>().onClick.AddListener(delegate { Main.ChargerPrerequis(obj.Prerequis); });

            }



            JoueurMonde.PeutAgir = false;

            //nomQuete.text = q.Nom;
            //responsable.text = q.ResponsableDebut;
            //etat.text = q.Etat.ToString();
            //foreach (Gain item in q.Gains)
            //    gains.text += item.Description + "\n";
            //description.text = q.Description;
            //foreach (Objectif item in q.Objectifs)
            //{
            //    objectifs.text += item.Description + " " + item.EstValide +"\n\n";
            //    if(item.Prerequis != null)
            //    {
            //        Dictionary<string, bool>.Enumerator enumPr = item.Prerequis.GetEnumerator();
            //        while (enumPr.MoveNext())
            //            prerequis.text += enumPr.Current.Key + " " + enumPr.Current.Value.ToString() + "\n";

            //    }

            //}

            //foreach (Gain item in q.Gains)
            //{
            //    Debug.Log(gains.text);
            //    gains.text += (item.Description + "\n");

            //}

            estActif = true;
            //Main.gameObject.SetActive(true);
            //GUI.FocusControl(nomQuete.name);
        }


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
        }



        private void ChargerPrerequis(Dictionary<string, bool> pr)
        {
            if (pr.Count == 0)
                return;
            foreach(var key in pr.Keys)
            {
                GameObject lblPrerequis = Instantiate(Resources.Load("Prefab/lblPrerequis")) as GameObject;
                lblPrerequis.transform.SetParent(lstPrerequis.transform);
                lblPrerequis.name = lblPrerequis.name.Replace("(Clone)", "");
                lblPrerequis.GetComponentInChildren<Text>().text = key;
            }


        }
    }
}
