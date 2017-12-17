using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

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

        public void createPolygon()
        {
            LinesVisual3D polygon = new LinesVisual3D();
            
            polygon.Points.Add(m_Points.Points.First());

            for(int i = 1; i < m_Points.Points.Count(); i++)
            {
                polygon.Points.Add(m_Points.Points[i]);
                polygon.Points.Add(m_Points.Points[i]);

                if(m_Points.Points[i].Equals(m_Points.Points.Last()))
                    polygon.Points.Add(m_Points.Points.First());

            }

            m_Viewport.Children.Add(polygon);
        }

        #region Private fields

        private RandomPoints2D m_Points;
        private HelixViewport3D m_Viewport;

        #endregion
    }
}
