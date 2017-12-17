using System.Windows.Media.Media3D;

namespace PointGenerator
{
    public static class Utils
    {
        public static class Math
        {
            private enum PointInPolygon_e { INSIDE, OUTSIDE, BOUNDARY }
            private enum EdgeType_e { TOUCHING, CROSSING, INESSENTIAL }
            private enum PointOverEdge_e { LEFT, RIGHT, BETWEEN, OUTSIDE }

            public static bool IsPointInPolygon(Point3D destinationPoint, Point3DCollection points)
            {
                bool parity = true;

                for (int i = 0; i < points.Count; i++)
                {
                    Point3D startEdge = points[i];
                    Point3D endEdge = points[(++i)];

                    switch (EdgeType(destinationPoint, startEdge, endEdge))
                    {
                        case EdgeType_e.TOUCHING:
                            return true;
                        case EdgeType_e.CROSSING:
                            parity = !parity;
                            break;
                    }
                }

                return parity ? false : true;
            }

            private static EdgeType_e EdgeType(Point3D destinationPoint, Point3D startEdge, Point3D endEdge)
            {
                switch (Classify(destinationPoint, startEdge, endEdge))
                {
                    case PointOverEdge_e.LEFT:
                        return ((startEdge.Y < destinationPoint.Y) && (destinationPoint.Y <= endEdge.Y)) ? EdgeType_e.CROSSING : EdgeType_e.INESSENTIAL;
                    case PointOverEdge_e.RIGHT:
                        return ((endEdge.Y < destinationPoint.Y) && (destinationPoint.Y <= startEdge.Y)) ? EdgeType_e.CROSSING : EdgeType_e.INESSENTIAL;
                    case PointOverEdge_e.BETWEEN:
                        return EdgeType_e.TOUCHING;
                    default:
                        return EdgeType_e.INESSENTIAL;
                }
            }

            private static PointOverEdge_e Classify(Point3D destinationPoint, Point3D startEdge, Point3D endEdge)
            {
                double a = startEdge.Y - endEdge.Y;
                double b = endEdge.X - startEdge.X;
                double c = startEdge.X * endEdge.Y - endEdge.X * startEdge.Y;

                double f = a * destinationPoint.X + b * destinationPoint.Y + c;

                if (f > 0)
                    return PointOverEdge_e.RIGHT;
                if (f < 0)
                    return PointOverEdge_e.LEFT;

                double minX = System.Math.Min(startEdge.X, endEdge.X);
                double maxX = System.Math.Max(startEdge.X, endEdge.X);
                double minY = System.Math.Min(startEdge.Y, endEdge.Y);
                double maxY = System.Math.Max(startEdge.Y, endEdge.Y);

                if (minX <= destinationPoint.X && destinationPoint.X <= maxX && minY <= destinationPoint.Y && destinationPoint.Y <= maxY)
                    return PointOverEdge_e.BETWEEN;

                return PointOverEdge_e.OUTSIDE;
            }
        }
    }
}
