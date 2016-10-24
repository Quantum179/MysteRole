﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Zone
    {
        public int Largeur { get; private set; }
        public int Longueur { get; private set; }
        protected Zone(int Largeur, int Longueur)
        {
            this.Largeur = Largeur;
            this.Longueur = Longueur;
        }
    }
    public class ZonePoint : Zone
    {
        public ZonePoint() : base(0, 0)
        {
            
        }
    }
    public class ZoneLosange : Zone
    {
        public ZoneLosange(int r) : base(r, 0)
        {
            // TODO
        }
        public int Rayon
        {
            get
            {
                return Largeur;
            }
        }
    }
    public class ZoneLigne : Zone
    {
        public ZoneLigne(int Largeur, int Longueur) : base(Largeur, Longueur)
        {
            // TODO
        }
    }
}
