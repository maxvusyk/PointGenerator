using HelixToolkit.Wpf;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PointGenerator
{
    public class ProcessLines
    {
        #region Constants
        
        private const int GroupsCount = 3;

        #endregion

        #region Constructors

        public ProcessLines() { }

        public ProcessLines(CoordinateBorder border, HelixViewport3D viewPort)
        {
            m_Points = new RandomPoints3D(border);
            m_Viewport = viewPort;
            ProcessLinesIntoGroups();
        }

        #endregion

        #region Properties

        public RandomPoints3D Points { get => m_Points; }
        public Dictionary<LineGroup_e, LinesVisual3D> Lines { get => m_Lines; }

        #endregion

        public enum LineGroup_e { LOW, MIDDLE, HIGHT }

        #region Public logic

        public void RenderLines()
        {
            if (m_Viewport == null)
                return;

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

        private void ProcessLinesIntoGroups()
        {
            var pointsWithDistance = GetPointsWithDistance(m_Points.Points);
            int elementsInGroup = (int)(pointsWithDistance.Count / GroupsCount);
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
