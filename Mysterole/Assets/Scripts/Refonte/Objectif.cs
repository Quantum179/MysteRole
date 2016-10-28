using System;
using System.Collections.Generic;

namespace Mysterole
{
	public class Objectif
	{
		//Attributs
		private string _description;
        private Dictionary<string, bool> _prerequis; 
		private bool _estValide;
        private string _responsable;
		private List<GainQuete> _gainsQuete;

        //Propriétes
		public string Description {
			get { return _description; }
			private set { _description = value;}
		}
		public Dictionary<string, bool> Prerequis
		{
			get {return _prerequis;}
			private set { _prerequis = value;}
		}

		public string Responsable 
		{
			get { return _responsable;}
			private set { _responsable = value;}
		}

        //Constructeurs
		public Objectif ()
		{
		}

		public Objectif(string d, bool e, string r)
		{
			_description = d;
            _estValide = e;
            _responsable = r;

			//Aller récupérer les pré-requis dans la BD seulement si l'objectif n'a pas encore été rempli
			if(!_estValide)
			{
				//Requête BD
			}

			//Initialiser les gains de quetes avec 
		}


        //Méthodes
		public bool ValiderObjectif()
		{
			//Faire la vérification des pré-requis à l'aide d'une série de requêtes BD
			if(_prerequis == null)
			{
                _estValide = true;
                return true;
			}


            Dictionary<string, bool>.Enumerator enumPrerequis = _prerequis.GetEnumerator();

			while(enumPrerequis.MoveNext())
				if(!enumPrerequis.Current.Value)
				return false;
			
			_estValide = true;
			return true;
		}





	}
}

