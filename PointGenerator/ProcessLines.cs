using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PointGenerator
{
    public class ProcessLines
    {
        #region Constants
        
        private const int CountOfGroups = 3;

        #endregion

        #region Constructors

        public ProcessLines() { }

        public ProcessLines(CoordinateBorder border, HelixViewport3D viewPort)
        {
            m_Points = new RandomPoints3D(border);
            m_Viewport = viewPort;
            processLinesIntoGroups();
        }

        #endregion

        #region Properties

        public RandomPoints Points { get => m_Points; }
        public Dictionary<LineGroup_e, LinesVisual3D> Lines { get => m_Lines; }

        #endregion

        #region Public logic

        public void RenderLines()
        {
            if (m_Viewport == null)
                return;

            if (m_Viewport.Children.Any())
                m_Viewport.Children.Clear();

            LinesVisual3D lines1 = m_Lines[LineGroup_e.LOW];
            LinesVisual3D lines2 = m_Lines[LineGroup_e.MIDDLE];
            LinesVisual3D lines3 = m_Lines[LineGroup_e.HIGHT];

            lines1.Color = Colors.Green;
            lines2.Color = Colors.Yellow;
            lines3.Color = Colors.Red;

            lines1.Thickness = 3;
            lines2.Thickness = 2;
            lines3.Thickness = 1;

            m_Viewport.Children.Add(lines1);
            m_Viewport.Children.Add(lines2);
            m_Viewport.Children.Add(lines3);
        }

        public void createPolygon(int pointCount)
        {
            LinesVisual3D polygon = new LinesVisual3D();
            Point3D prev = new Point3D();
            Random rand = new Random();
            Point3D nullablePoint = new Point3D();

            int randNumber = rand.Next(0, m_Points.Points.Count());
            while (m_Points.Points[randNumber].Equals(nullablePoint))
                randNumber = rand.Next(0, m_Points.Points.Count());

            polygon.Points.Add(m_Points.Points[randNumber]);

            for (int i = 1; i < pointCount; i++)
            {
                randNumber = rand.Next(0, m_Points.Points.Count());
                while (m_Points.Points[randNumber].Equals(nullablePoint))
                    randNumber = rand.Next(0, m_Points.Points.Count());

                if (!prev.Equals(nullablePoint))
                    polygon.Points.Add(prev);

                prev = m_Points.Points[rand.Next(0, m_Points.Points.Count())];
                polygon.Points.Add(prev);
            }

            polygon.Points.Add(polygon.Points.First());
            //Visual3DCollection col = new Visual3DCollection();
            //col.Add();
        }

        private void processLinesIntoGroups()
        {
            var pointsWithDistance = GetPointsWithDistance(m_Points.Points);
            int elementsInGroup = (int)(pointsWithDistance.Count / CountOfGroups);
            LineGroup_e typeOfGroup = LineGroup_e.LOW;
            int elementCount = 0;
            
            foreach(var elements in pointsWithDistance)
            {
                if ((elementCount > elementsInGroup) && (typeOfGroup != LineGroup_e.HIGHT))
                {
                    ++typeOfGroup;
                    elementCount = 0;
                }

                foreach (var element in elements.Value)
                {
                    if (!m_Lines.ContainsKey(typeOfGroup))
                    {
                        LinesVisual3D line = new LinesVisual3D();
                        line.Points.Add(new Point3D(0,0,0));
                        line.Points.Add(element);
                        m_Lines.Add(typeOfGroup, line);
                    }
                    else
                    {
                        m_Lines[typeOfGroup].Points.Add(new Point3D(0, 0, 0));
                        m_Lines[typeOfGroup].Points.Add(element);
                    }
                }
                ++elementCount;
            }
        }

        private SortedDictionary<double, List<Point3D>> GetPointsWithDistance(Point3DCollection points)
        {
            var sortedPoints = new SortedDictionary<double, List<Point3D>>();
            var centrePoint = new Point3D();

            foreach (var point in points)
            {
                if (point.Equals(centrePoint))
                    continue;

                double distance = centrePoint.DistanceTo(point);

                if (!sortedPoints.ContainsKey(distance))
                    sortedPoints.Add(distance, new List<Point3D> { point });
                else
                    sortedPoints[distance].Add(point);
            }

            return sortedPoints;
        }

        #endregion

        #region Private fields

        private RandomPoints3D m_Points;
        private Dictionary<LineGroup_e, LinesVisual3D> m_Lines = new Dictionary<LineGroup_e, LinesVisual3D>();
        private HelixViewport3D m_Viewport;

        #endregion
    }
}
