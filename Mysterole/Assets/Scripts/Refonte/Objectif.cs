using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mysterole
{
	public abstract class Objectif
	{
        //Attributs
        protected int _id;
        public int ID
        {
            get { return _id; }
        }

        protected Quete _queteReliee;
        public Quete QueteReliee
        {
            get { return _queteReliee; }
        }

        protected TypeObjectif _type;
        public TypeObjectif Type
        {
            get { return _type; }
        }

        protected int _responsable;
        public int Responsable
        {
            get { return _responsable; }
        }

        protected string _description;
        public string Description
        {
            get { return _description; }
        }


        //private Dictionary<string, bool> _prerequis;
        //public Dictionary<string, bool> Prerequis
        //{
        //    get { return _prerequis; }
        //    private set { _prerequis = value; }
        //}

        protected bool _estValide;
        public bool EstValide
        {
            get { return _estValide; }
            set { _estValide = value; }
        }

        protected List<GainQuete> _gainsQuete;
        public List<GainQuete> GainsQuete
        {
            get { return _gainsQuete; }
        }
        //Propriétes


        //Constructeurs
		public Objectif ()
		{
		}

		public Objectif(string d, bool e, string r)
		{
			//_description = d;
   //         _estValide = e;
   //         _responsable = r;

			////Aller récupérer les pré-requis dans la BD seulement si l'objectif n'a pas encore été rempli
			//if(!_estValide)
			//{
   //             //Requête BD pour les pré-requis
   //             //Hardcode
   //             _prerequis = null;
			//}
   //         else
   //         {
   //             _prerequis = null;
   //         }

			////Initialiser les gains de quetes avec l'id de l'objectif
		}

        public Objectif(int id, Quete q, TypeObjectif to, int res, bool ev)
        {
            _id = id;
            _queteReliee = q;
            _type = to;
            _responsable = res;
            _estValide = ev;

            //string[] lstp = param.Split(';');

            //switch (_type)
            //{
            //    case TypeObjectif.Parler:
                    
            //        break;
            //    case TypeObjectif.Trouver:
            //        break;
            //    case TypeObjectif.Battre:
            //        break;
            //    case TypeObjectif.Explorer:
            //        break;
            //    case TypeObjectif.NULL:
            //        break;
            //    default:
            //        break;
            //}

        }

        //Méthodes
        public virtual bool ValiderObjectif()
		{

            return true;



		//	//Faire la vérification des pré-requis à l'aide d'une série de requêtes BD
		//	//if(_prerequis == null)
		//	//{
  // //             _estValide = true;
  // //             return true;
		//	//}


  // //         Dictionary<string, bool>.Enumerator enumPrerequis = _prerequis.GetEnumerator();

		//	//while(enumPrerequis.MoveNext())
		//	//	if(!enumPrerequis.Current.Value)
		//	//	return false;

		//	_estValide = true;
		//	return true;
		}





	}
}
