using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mysterole
{
	public abstract class Evenement
	{
        protected int _id;
        public int ID
        {
            get { return _id; }
        }

        protected Etape _etapeReliee;
        public Etape EtapeReliee
        {
            get { return _etapeReliee; }
        }

        protected int _idQuete;
        public int IDQuete
        {
            get { return _idQuete; }
        }

        protected int _idObjectif;
        public int IDObjectif
        {
            get { return _idObjectif; }
        }

        protected int _indexEvenement;
        public int IndexEvenement
        {
            get { return _indexEvenement; }
        }

		protected TypeEvenement _type;
        public TypeEvenement Type
        {
            get { return _type; }
            private set { }
        }

        protected EtatEvenement _etat;
        public EtatEvenement Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }

        protected bool _peutContinuer;
        public bool PeutContinuer
        {
            get { return _peutContinuer; }
        }

        //protected int _etape;


        //protected int _position;
        //public int

        protected float _decompte;
        public float Decompte
        {
            get { return _decompte; }
            set { _decompte = value; }
        }


		public Evenement ()
		{
            _type = TypeEvenement.NULL ;
            _etat = EtatEvenement.EnAttente;
            
		}

        public Evenement(TypeEvenement te)
        {
            _type = te;
            _etat = EtatEvenement.EnAttente;
        }

        public Evenement(int id, Etape e, TypeEvenement te, bool pc)
        {
            _id = id;
            _etapeReliee = e;
            _type = te;
            _peutContinuer = pc;
        }

        public abstract void DeclencherEvenement(Pnj pnj);

        public abstract void DeclencherEvenement(JoueurMonde j);





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
