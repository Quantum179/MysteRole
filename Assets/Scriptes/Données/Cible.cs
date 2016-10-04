using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

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
            CibleUnite = SurPersonne;
            if (DistanceMin > DistanceMax)
                throw new ArgumentOutOfRangeException("Distance minimale doit être inférieure à distance maximale.");
            this.DistanceMin = DistanceMin;
            this.DistanceMax = DistanceMax;
            EnDiag = Diag;
            EnLigne = Croix;
            this.Zone = Zone;
        }
    }
    public class CiblePlein : Cible
    {
        public CiblePlein(bool SurPersonne, int DistanceMin, int DistanceMax, Zone Zone) : base(SurPersonne, DistanceMin, DistanceMax, true, true, Zone)
        {
            
        }
    }
    public class CibleCercle : Cible
    {
        public CibleCercle(bool SurPersonne, Zone Zone) : base(SurPersonne, 2, 2, true, true, Zone)
        {
            
        }
    }
}
