using System;
using UnityEngine;

namespace Mysterole
{
	public class Dialogue : Evenement
	{
        //Attributs
        private string _message;
        public string Message
        {
            get { return _message; }
            private set { }
        }


        public Dialogue () : base()
		{
            _message = null;
		}
        public Dialogue(string m)
        {
            _message = m;
            _type = TypeEvenement.Dialogue;
        }
        public Dialogue(string m, TypeEvenement t)
        {
            _message = m;
            _type = t;
        }
        public Dialogue(int id, TypeEvenement te, int p, string param)
            : base(id, null, te, false)
        {
            string[] lst = param.Split(';');
            _message = lst[0];
            _idQuete = int.Parse(lst[1]);
            _idObjectif = int.Parse(lst[2]);
            _indexEvenement = p;
        }
        public Dialogue(int id, Etape e, TypeEvenement te, bool pc, string param)
            : base(id, e, te, pc)
        {
            string[] lst = param.Split(';');

            _message = lst[0];
            _decompte = float.Parse(lst[1]);
            _etat = EtatEvenement.EnAttente;
        }


        public override void DeclencherEvenement(Pnj pnj)
        {
            _etat = EtatEvenement.EnCours;
            pnj.Discute = true;
            pnj.EvenementActuel = this;
            JoueurMonde.PeutAgir = false;
            EcranDialogue.NouveauDialogue(pnj.Nom, _message);

            //Empêcher le joueur d'agir
        }


        public override void DeclencherEvenement(JoueurMonde j)
        {
            _etat = EtatEvenement.EnCours;
            j.Discute = true;
            JoueurMonde.EvenementActuel = this;
            EcranDialogue.NouveauDialogue(JoueurMonde.Moi.gameObject.name, _message);
        }



    }
}













/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mysterole
{
    public class Dialogue : Evenement 
    {
        private string _message;
        public string Message { get; private set; }


        public Dialogue(string message) : base()
        {
            Message = message;
        }

        public override string GetTypeEvent()
        {
            return "Dialogue";
        }



    }
}
*/
