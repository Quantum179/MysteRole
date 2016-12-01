using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysterole
{
    public class MethodesEnum
    {
        //Méthode pour trouver l'état de quête selon un index
        public static EtatQuete TrouverEtatQuete(int eq)
        {
            switch (eq)
            {
                case 1:
                    return EtatQuete.Bloquee;
                case 2:
                    return EtatQuete.Disponible;
                case 3:
                    return EtatQuete.EnCours;
                case 4:
                    return EtatQuete.Terminee;
                default:
                    return EtatQuete.NULL;
            }
        }

        //Méthode pour trouver le type d'objectif selon un index
        public static TypeObjectif TrouverTypeObjectif(int to)
        {
            switch (to)
            {
                case 1:
                    return TypeObjectif.Parler;
                case 2:
                    return TypeObjectif.Trouver;
                case 3:
                    return TypeObjectif.Battre;
                case 4:
                    return TypeObjectif.Explorer;
                default:
                    return TypeObjectif.NULL;
            }
        }

        //Méthode pour trouver la zone de carte
        public static ZoneCarte TrouverZoneCarte(int zc)
        {
            switch (zc)
            {
                case 1:
                    return ZoneCarte.Foret;
                case 2:
                    return ZoneCarte.Cite;
                case 3:
                    return ZoneCarte.Caverne;
                default:
                    return ZoneCarte.NULL;
            }
        }

        //Méthode pour trouver l'état d'une étape
        public static EtatEtape TrouverEtatEtape(int ee)
        {
            switch (ee)
            {
                case 1:
                    return EtatEtape.EnAttente;
                case 2:
                    return EtatEtape.EnCours;
                case 3:
                    return EtatEtape.Finie;
                default:
                    return EtatEtape.NULL;
            }
        }

        //Méthode pour trouver le type d'un événement
        public static TypeEvenement TrouverTypeEvenement(int te)
        {
            switch (te)
            {
                case 1:
                    return TypeEvenement.Dialogue;
                case 2:
                    return TypeEvenement.Deplacement;
                case 3:
                    return TypeEvenement.Attente;
                default:
                    return TypeEvenement.NULL;
            }
        }

        //Méthode pour trouver l'état d'une cinématique
        public static EtatCinematique TrouverEtatCinematique(int ec)
        {
            switch (ec)
            {
                case 1:
                    return EtatCinematique.Bloquee;
                case 2:
                    return EtatCinematique.Disponible;
                case 3:
                    return EtatCinematique.EnCours;
                case 4:
                    return EtatCinematique.Terminee;
                default:
                    return EtatCinematique.NULL;
            }
        }



        public static EtatQuete ActualiserEtatQuete(EtatQuete eq)
        {
            switch (eq)
            {
                case EtatQuete.Bloquee:
                    return EtatQuete.Disponible;
                case EtatQuete.Disponible:
                    return EtatQuete.EnCours;
                case EtatQuete.EnCours:
                    return EtatQuete.Terminee;
                case EtatQuete.Terminee:
                    return EtatQuete.Terminee;
                case EtatQuete.NULL:
                    return EtatQuete.NULL;
                default:
                    return EtatQuete.NULL;
            }
        }
    }
}
