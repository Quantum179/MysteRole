using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mysterole
{
    public abstract class Zone
    {
        public int Largeur { get; private set; }
        public int Longueur { get; private set; }
        protected Zone(int Largeur, int Longueur)
        {
            // TODO
        }
        public virtual int[][] PrendreZone(int x, int y)
        {
            return new int[][] { new int[] { 0, 0 } }; // TODO
        }
    }
    public class ZonePoint : Zone
    {
        public ZonePoint() : base(0, 0)
        {
            // TODO
        }
        /*public override int[][] PrendreZone(int x, int y)
        {
            return new int[][] { new int[] { 0, 0 } }; // TODO
        }*/
    }
    public class ZoneLosange : Zone
    {
        public ZoneLosange(int r) : base(r, 0)
        {
            // TODO
        }
        public override int[][] PrendreZone(int x, int y)
        {
            return new int[][] { new int[] { 0, 0 } };// TODO
        }
    }
    public class ZoneLigne : Zone
    {
        public ZoneLigne(int Largeur, int Longueur) : base(Largeur, Longueur)
        {
            // TODO
        }
        /*public override int[][] PrendreZone(int x, int y)
        {
            return new int[][] { new int[] { 0, 0 } };// TODO
        }*/
    }
}
