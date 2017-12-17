using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Points.Add(new Point3D(0, 0, 0));
                Points.Add(getRandomPoint());

            }
        }

        protected override Point3D getRandomPoint()
        {
            int x = getRandomNumber(m_CoordinateBorder.MinX, m_CoordinateBorder.MaxX);
            int y = getRandomNumber(m_CoordinateBorder.MinY, m_CoordinateBorder.MaxY);
            int z = getRandomNumber(m_CoordinateBorder.MinZ, m_CoordinateBorder.MaxZ);

            return new Point3D(x, y, z);
        }
    }
}
