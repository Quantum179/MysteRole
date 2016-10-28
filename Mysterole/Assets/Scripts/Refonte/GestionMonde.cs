using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysterole;

namespace Mysterole
{
    public class GestionMonde
    {
        //Attributs
        private string _zone;
        private Dictionary<string, Carte> _cartes;
        //private Dictionary<string, Dictionary<>> _quetes;
        private Dictionary<string, List<Quete>> _quetes;



        private Equipe _equipeJoueur;
        //private List<Objet> _objetsEquipe;


        //Propriétés
        public string Zone
        {
            get { return _zone; }
            private set { _zone = value; }
        }
        public Dictionary<string, Carte> Cartes
        {
            get { return _cartes; }
            private set { _cartes = value; }
        }
        public Dictionary<string, List<Quete>> Quetes
        {
            get { return _quetes; }
            private set { _quetes = value; }
        }
        public Equipe EquipeJoueur 
        {
            get { return _equipeJoueur; }
            private set { _equipeJoueur = value; }
        }

        //Constructeur
        public GestionMonde()
        {
            _zone = "Foret";
            _cartes = new Dictionary<string, Carte>();
            _quetes = new Dictionary<string, List<Quete>>();

            _quetes.Add("La visite du Roi", new List<Quete>());





            //Initialisation hardcodé, corriger avec les données de la BD
            //TODO : faire une ébauche de BD avant lundi



            _quetes["La visite du Roi"].Add(new Quete("La demande du milicien"));
            _quetes["La visite du Roi"].Add(new Quete("La veillée mortuaire"));
            _quetes["La visite du Roi"].Add(new Quete("L'arrivée du Roi"));






            //List<Quete> uneListeQuete = new List<Quete>();
            //_quetes.Add("La visite du Roi", uneListeQuete);

            //_quetes.Add("La visite du Roi", new List<Quete>().Add(new Quete("La demande du Chef milicien"))));
            //_quetes.Add("La visite du Roi", new Quete("La veillée mortuaire"));

        }



        
        public static void ValiderQuete(string nomQuete, List<Gain> gains)
        {           
            List<Gain>.Enumerator enumGain = gains.GetEnumerator();
            
            while(enumGain.MoveNext())
            {
                switch(enumGain.Current.Type)
                {
                    case TypeGain.Argent:

                        break;
                    case TypeGain.Objet:

                        break;
                    case TypeGain.Competence:

                        break;
                    case TypeGain.GainQuete:

                        break;
                    default:
                        
                        break;
                }
            }

            //TODO : Notifier le joueur que les gains ont bien été distribués
            //TODO : Gérer le débloquement de la quête suivante si existente

            
        }
    }
}
