using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mysterole
{
    public class Deplacement : Evenement
    {
        private Vector2 _destination;

        public Vector2 Destination
        {
            get { return _destination; }
            private set { _destination = value; }
        }

        public Deplacement() : base()
        {

        }

        public Deplacement(float x, float y, TypeEvenement t)
        {
            _destination = new Vector2(x, y);
            _type = t;
        }
        public Deplacement(int id, TypeEvenement te, int p, string param)
            : base(id, null, te, false)
        {
            string[] lst = param.Split(';');
            string[] pos = lst[0].Split(',');
            _destination = new Vector2(float.Parse(pos[0]), float.Parse(pos[1]));
            _idQuete = int.Parse(lst[2]);
            _idObjectif = int.Parse(lst[3]);
            _indexEvenement = p;
        }
        public Deplacement(int id, Etape e, TypeEvenement te, bool pc, string param)
            : base(id, e, te, pc)
        {
            string[] lst = param.Split(';');
            string[] pos = lst[0].Split(',');
            _destination = new Vector2(float.Parse(pos[0]), float.Parse(pos[1]));

            if(lst[1] != "")
                _decompte = float.Parse(lst[1]);
        }

        public override void DeclencherEvenement(Pnj pnj)
        {
            _etat = EtatEvenement.EnCours;
            pnj.EvenementActuel = this;
            pnj.SeDeplace = true;
        }

        public override void DeclencherEvenement(JoueurMonde j)
        {
            _etat = EtatEvenement.EnCours;
            JoueurMonde.EvenementActuel = this;
            j.SeDeplace = true;
        }



    }
}












////////////////////



/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
public class Deplacement : Evenement
{
    public Vector2 Destination;


    public Deplacement(float x, float y) : base()
    {
        Destination = new Vector2(x, y);
    }

    public override string GetTypeEvent()
    {
        return "Deplacement";
    }


}
}
*/
