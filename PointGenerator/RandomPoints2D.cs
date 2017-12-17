using System.Windows.Media.Media3D;

namespace PointGenerator
{
    class RandomPoints2D : RandomPoints
    {
        public RandomPoints2D(CoordinateBorder border)
        {
            m_CoordinateBorder = border;
            m_CoordinateBorder.MinX = 0;
            m_CoordinateBorder.MinY = 0;
            GeneratePoints();
        }

        public Point3DCollection Points { get => m_Points; protected set => m_Points = value; }

        protected override void GeneratePoints()
        {
            for (int i = 0; i < m_CoordinateBorder.Count; i++)
            {
                Points.Add(GetRandomPoint());
            }
        }

        protected override Point3D GetRandomPoint()
        {
            int x = GetRandomNumber(m_CoordinateBorder.MinX, m_CoordinateBorder.MaxX);
            int y = GetRandomNumber(m_CoordinateBorder.MinY, m_CoordinateBorder.MaxY);

            return new Point3D(x, y, 0);
        }
    }
}
