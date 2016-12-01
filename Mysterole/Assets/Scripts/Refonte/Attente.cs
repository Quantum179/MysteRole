using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysterole
{
    public class Attente : Evenement
    {
        private float _temps;
        public float Temps
        {
            get { return _temps; }
            private set { _temps = value; }
        }

        private string _direction;
        public string Direction
        {
            get { return _direction; }
            private set { _direction = value; }
        }


        public Attente()
        {
            _type = TypeEvenement.Attente;
            _etat = EtatEvenement.EnAttente;
            _decompte = 0;

        }

        public Attente(float t, TypeEvenement ty)
        {
            _decompte = t;
            _type = ty;
        }
        public Attente(int id, TypeEvenement te, int p, string param)
            : base(id, null, te, false)
        {
            string[] lst = param.Split(';');
            _direction = lst[0];
            _temps = float.Parse(lst[1]);
            _decompte = float.Parse(lst[1]);

        }
        public Attente(int id, Etape e, TypeEvenement te, bool pc, string param)
            : base(id, e, te, pc)
        {
            string[] lst = param.Split(';');

            _direction = lst[0];
            _decompte = float.Parse(lst[1]);
        }



        public override void DeclencherEvenement(Pnj pnj)
        {
            _etat = EtatEvenement.EnCours;
            pnj.EvenementActuel = this;
            switch (_direction)
            {
                case "haut":
                    pnj.Anim.SetFloat("input_y", 1);
                    break;
                case "bas":
                    pnj.Anim.SetFloat("input_y", -1);
                    break;
                case "droite":
                    pnj.Anim.SetFloat("input_x", 1);
                    break;
                case "gauche":
                    pnj.Anim.SetFloat("input_x", -1);
                    break;
            }
            pnj.FaitPause = true;
        }

        public override void DeclencherEvenement(JoueurMonde j)
        {
            _etat = EtatEvenement.EnCours;
            JoueurMonde.EvenementActuel = this;
            j.FaitPause = true;


        }
    }
}
