using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace PointGenerator
{
    public class RandomPoints
    {
        public RandomPoints(CoordinateBorder border)
        {
            m_CoordinateBorder = border;
            GeneratePoints();
        }

        public Point3DCollection Points { get => m_Points; private set => m_Points = value; }

        public void GeneratePoints()
        {
            for (int i = 0; i < m_CoordinateBorder.Count; i++)
            {
                m_Points.Add(new Point3D(0, 0, 0));
                m_Points.Add(getRandomPoint());
            }
        }

        private Point3D getRandomPoint()
        {
            int x = getRandomNumber(m_CoordinateBorder.MinX, m_CoordinateBorder.MaxX);
            int y = getRandomNumber(m_CoordinateBorder.MinY, m_CoordinateBorder.MaxY);
            int z = getRandomNumber(m_CoordinateBorder.MinZ, m_CoordinateBorder.MaxZ);

            return new Point3D(x, y, z);
        }

        private int getRandomNumber(int min, int max)
        {
            return m_Random.Next(min, max);
        }

        private Point3DCollection m_Points = new Point3DCollection();
        private CoordinateBorder m_CoordinateBorder;
        private Random m_Random = new Random();
    }
}
