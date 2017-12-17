using HelixToolkit.Wpf;
using System.Linq;

namespace PointGenerator
{
    class ProcessPolygon
    {
        public ProcessPolygon(CoordinateBorder border, HelixViewport3D viewPort)
        {
            m_Points = new RandomPoints2D(border);
            m_Viewport = viewPort;
        }

        public RandomPoints2D Points { get => m_Points; }
        public LinesVisual3D Polygon { get => m_Polygon;}

        public void createPolygon()
        {
            m_Polygon = new LinesVisual3D();

            m_Polygon.Points.Add(m_Points.Points.First());

            for(int i = 1; i < m_Points.Points.Count(); i++)
            {
                m_Polygon.Points.Add(m_Points.Points[i]);
                m_Polygon.Points.Add(m_Points.Points[i]);

                if(m_Points.Points[i].Equals(m_Points.Points.Last()))
                    m_Polygon.Points.Add(m_Points.Points.First());

            }

            m_Viewport.Children.Add(m_Polygon);
        }

        #region Private fields

        private RandomPoints2D m_Points;
        private LinesVisual3D m_Polygon;
        private HelixViewport3D m_Viewport;

        #endregion
    }
}
