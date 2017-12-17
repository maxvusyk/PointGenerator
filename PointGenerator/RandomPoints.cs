using System;
using System.Windows.Media.Media3D;

namespace PointGenerator
{
    public abstract class RandomPoints
    {
        protected abstract void GeneratePoints();
        protected abstract Point3D GetRandomPoint();

        protected virtual int GetRandomNumber(int min, int max)
        {
            return m_Random.Next(min, max);
        }

        protected Point3DCollection m_Points = new Point3DCollection();
        protected CoordinateBorder m_CoordinateBorder;
        protected Random m_Random = new Random();
    }
}
