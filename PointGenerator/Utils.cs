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
        public static class Math
        {
            public static bool IsPointInTriangle(Point3D destinationPoint, Point3D a, Point3D b, Point3D c)
            {
                return ((Classify(destinationPoint, a, b) != Location_e.LEFT) &&
                        (Classify(destinationPoint, b, c) != Location_e.LEFT) &&
                        (Classify(destinationPoint, c, a) != Location_e.LEFT));
            }

            private static Location_e Classify(Point3D destinationPoint, Point3D trianglePoint0, Point3D trianglePoint1)
            {
                Point3D a = (Point3D)(trianglePoint1 - trianglePoint0);
                Point3D b = (Point3D)(destinationPoint - trianglePoint0);

                double sa = (a.X * b.Y) - (b.X * a.Y);
                if (sa > 0.0)
                    return Location_e.LEFT;
                //if (sa < 0.0)
                //    return Location_e.RIGHT;
                if ((a.X * b.X < 0.0) || (a.Y * b.Y < 0.0))
                    return Location_e.BEHIND;
                if (VectorLength(a) < VectorLength(b))
                    return Location_e.BEYOND;
                if (trianglePoint0.Equals(destinationPoint))
                    return Location_e.ORIGIN;
                if (trianglePoint1.Equals(destinationPoint))
                    return Location_e.DESTINATION;
                return Location_e.BETWEEN;
            }

            private static double VectorLength(Point3D point)
            {
                return System.Math.Sqrt((point.X * point.X) + (point.Y * point.Y));
            }

            private enum Location_e
            {
                LEFT, RIGHT, BEYOND, BEHIND, BETWEEN, ORIGIN, DESTINATION
            }
        }
    }
}
