using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointGenerator
{
    public class CoordinateBorder
    {
        
        public CoordinateBorder(List<int> borders)
        {
            this.minX = borders[0];
            this.maxX = borders[1];
            this.minY = borders[2];
            this.maxY = borders[3];
            this.minZ = borders[4];
            this.maxZ = borders[5];
            this.Count = borders[6];
        }

        public int MinX { get => minX; set => minX = value; }
        public int MaxX { get => maxX; set => maxX = value; }
        public int MinY { get => minY; set => minY = value; }
        public int MaxY { get => maxY; set => maxY = value; }
        public int MinZ { get => minZ; set => minZ = value; }
        public int MaxZ { get => maxZ; set => maxZ = value; }
        public int Count { get => count; set => count = value; }

        private int minX;
        private int maxX;
        private int minY;
        private int maxY;
        private int minZ;
        private int maxZ;
        private int count;
    }
}
