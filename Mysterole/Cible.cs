using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public class Cible
    {
        public bool CibleUnite { get; private set; }
        public int DistanceMax { get; private set; }
        public int DistanceMin { get; private set; }
        public bool EnDiag { get; private set; }
        public bool EnLigne { get; private set; }
        public Zone Zone { get; private set; }
        protected Cible(bool SurPersonne, int DistanceMin, int DistanceMax, bool Diag, bool Croix, Zone Zone)
        {
            // TODO
        }
        public virtual int[][] PrendreCibles(int x, int y)
        {
            return new int[][] { new int[] { 0, 0 } }; // TODO
        }
    }
    /*public class CibleEtoile : Cible
    {
        public CibleEtoile(bool SurPersonne, int DistanceMin, int DistanceMax, Zone Zone) : base(SurPersonne, DistanceMin, DistanceMax, true, true, Zone)
        {
            // TODO
        }
    }
    public class CibleDiag : Cible
    {
        public CibleDiag(bool SurPersonne, int DistanceMin, int DistanceMax, Zone Zone) : base(SurPersonne, DistanceMin, DistanceMax, true, false, Zone)
        {
            // TODO
        }
    }
    public class CibleCroix : Cible
    {
        public CibleCroix(bool SurPersonne, int DistanceMin, int DistanceMax, Zone Zone) : base(SurPersonne, DistanceMin, DistanceMax, false, true, Zone)
        {
            // TODO
        }
    }*/
    public class CiblePlein : Cible
    {
        public CiblePlein(bool SurPersonne, int DistanceMin, int DistanceMax, Zone Zone) : base(SurPersonne, DistanceMin, DistanceMax, false, true, Zone)
        {
            // TODO
        }
        public override int[][] PrendreCibles(int x, int y)
        {
            return new int[][] { new int[] { 0, 0 } }; // TODO
        }
    }
    public class CibleCercle : Cible
    {
        public CibleCercle(Zone Zone) : base(false, 2, 2, false, true, Zone)
        {
            // TODO
        }
        public override int[][] PrendreCibles(int x, int y)
        {
            return new int[][] { new int[] { 0, 0 } }; // TODO
        }
    }
}
