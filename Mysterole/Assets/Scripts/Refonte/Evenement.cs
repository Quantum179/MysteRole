using System;

namespace Mysterole
{
	public class Evenement
	{
		
		//Attributs
		protected TypeEvenement _type;
		//protected int _idQuete;
        protected bool _estFinie;


		//Propriétés
		public TypeEvenement Type
		{
			get { return _type;}
			private set { }
		}
		public bool EstFinie
		{
			get { return _estFinie;}
			private set { }
		}


		public Evenement ()
		{
            _type = TypeEvenement.NonAssigné ;
            _estFinie = false;
		}


        public virtual void DeclencherEvenement()
        {
            throw new Exception();
        }
	}
}












//////////////////////////////////////////////////////////////////////////////







//    using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//namespace Mysterole
//{
//    public abstract class Evenement
//    {

//        public Evenement()
//        {

//        }

//        public abstract string GetTypeEvent();
//        //public abstract IEnumerator RunEvent();







//    }
//}
