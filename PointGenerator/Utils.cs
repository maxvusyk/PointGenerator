using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PointGenerator
{
    public static class Utils
    {
        public static class Distance
        {
            public static SortedDictionary<double, List<Point3D>> GetPointsWithDistance(Point3DCollection points)
            {
                var sortedPoints = new SortedDictionary<double, List<Point3D>>();
                foreach(var point in points)
                {
                    if ((point.X == 0) && (point.Y == 0) && (point.Z == 0))
                        continue;

                    double distance = getDistance(point);

                    if (!sortedPoints.ContainsKey(distance))
                        sortedPoints.Add(distance, new List<Point3D> { point });
                    else
                        sortedPoints[distance].Add(point);
                }

                return sortedPoints;
            }

            private static double getDistance(Point3D point)
            {
                return Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2) + Math.Pow(point.Z, 2));
            }
        }
    }
}
