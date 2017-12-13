using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PointGenerator
{
    public class SortedLines
    {
        #region Constants
        
        private const int CountOfGroups = 3;

        #endregion

        public SortedLines(CoordinateBorder border, HelixViewport3D viewPort)
        {
            m_Points = new RandomPoints(border);
            m_Viewport = viewPort;
            processLinesIntoGroups();
        }

        public RandomPoints Points { get => m_Points; }
        public Dictionary<LineGroup_e, LinesVisual3D> Lines { get => m_Lines; }

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

        private void processLinesIntoGroups()
        {
            var pointsWithDistance = Utils.Distance.GetPointsWithDistance(m_Points.Points);
            int elementsInGroup = (int)pointsWithDistance.Count / CountOfGroups;
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

        private RandomPoints m_Points;
        private Dictionary<LineGroup_e, LinesVisual3D> m_Lines = new Dictionary<LineGroup_e, LinesVisual3D>();
        private HelixViewport3D m_Viewport;
    }

}
