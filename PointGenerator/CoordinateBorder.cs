using System.Collections.Generic;

namespace PointGenerator
{
    public class CoordinateBorder
    {
        public CoordinateBorder() { }

        public CoordinateBorder(List<int> borders)
        {
            m_MinX = borders[0];
            m_MaxX = borders[1];
            m_MinY = borders[2];
            m_MaxY = borders[3];
            m_MinZ = borders[4];
            m_MaxZ = borders[5];
            m_Count = borders[6];
        }

        public int MinX { get => m_MinX; }
        public int MaxX { get => m_MaxX; }
        public int MinY { get => m_MinY; }
        public int MaxY { get => m_MaxY; }
        public int MinZ { get => m_MinZ; }
        public int MaxZ { get => m_MaxZ; }
        public int Count { get => m_Count; }

        private int m_MinX;
        private int m_MaxX;
        private int m_MinY;
        private int m_MaxY;
        private int m_MinZ;
        private int m_MaxZ;
        private int m_Count;
    }
}
