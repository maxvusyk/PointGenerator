using System.Windows.Media.Media3D;

namespace PointGenerator
{
    public class RandomPoints3D : RandomPoints
    {
        public RandomPoints3D(CoordinateBorder border)
        {
            m_CoordinateBorder = border;
            GeneratePoints();
        }

        public Point3DCollection Points { get => m_Points; protected set => m_Points = value; }

        protected override void GeneratePoints()
        {
            for (int i = 0; i < m_CoordinateBorder.Count; i++)
            {
                m_Points.Add(new Point3D(0, 0, 0));
                m_Points.Add(GetRandomPoint());

            }
        }

        protected override Point3D GetRandomPoint()
        {
            int x = GetRandomNumber(m_CoordinateBorder.MinX, m_CoordinateBorder.MaxX);
            int y = GetRandomNumber(m_CoordinateBorder.MinY, m_CoordinateBorder.MaxY);
            int z = GetRandomNumber(m_CoordinateBorder.MinZ, m_CoordinateBorder.MaxZ);

            return new Point3D(x, y, z);
        }
    }
}
