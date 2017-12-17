using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PointGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewPort1.CalculateCursorPosition = true;
            orthographicCamera = new OrthographicCamera(new Point3D(0, 0, 20), new Vector3D(0, 0, 0), new Vector3D(0, 0, 1), 3);
            perspectiveCamera = new PerspectiveCamera(new Point3D(0, 0, -3), new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), 50);
        }

        private void buttonGenerateLines_Click(object sender, RoutedEventArgs e)
        {
            //setPerspectiveCamera();
            clearScene();
            var coordBorder = getBorder(Int32.Parse(textBoxCountPoint.Text));
            var m_ProcessLines = new ProcessLines(coordBorder, ViewPort1);
            m_ProcessLines.RenderLines();

        }

        private void buttonGeneratePolygon_Click(object sender, RoutedEventArgs e)
        {
            //setOrthographicCamera();
            clearScene();
            var coordBorder = getBorder(Int32.Parse(textBoxCountPolygonPoints.Text));
            m_ProcessPolygon = new ProcessPolygon(coordBorder, ViewPort1);
            m_ProcessPolygon.createPolygon();
        }

        private CoordinateBorder getBorder(int countPoints)
        {
            List<int> borders = new List<int>();
            borders.Add(Int32.Parse(textBoxMinX.Text));
            borders.Add(Int32.Parse(textBoxMaxX.Text));
            borders.Add(Int32.Parse(textBoxMinY.Text));
            borders.Add(Int32.Parse(textBoxMaxY.Text));
            borders.Add(Int32.Parse(textBoxMinZ.Text));
            borders.Add(Int32.Parse(textBoxMaxZ.Text));

            return
                new CoordinateBorder(borders, countPoints);
        }

        private void buttonDefault_Click(object sender, RoutedEventArgs e)
        {
            setDefaultValues();
        }

        private void setDefaultValues()
        {
            textBoxMinX.Text = "-20";
            textBoxMaxX.Text = "20";
            textBoxMinY.Text = "-30";
            textBoxMaxY.Text = "30";
            textBoxMinZ.Text = "-10";
            textBoxMaxZ.Text = "10";
            textBoxCountPoint.Text = "50";
        }

        private void clearScene()
        {
            if (ViewPort1.Children.Any())
                ViewPort1.Children.Clear();
        }

        private void setOrthographicCamera()
        {
            ViewPort1.Camera = orthographicCamera;
        }

        private void setPerspectiveCamera()
        {
            ViewPort1.Camera = perspectiveCamera;
        }

        private void ViewPort1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (m_ProcessPolygon == null)
                return;

            var destinationPoint = ViewPort1.CursorPosition;
            SphereVisual3D sphere = new SphereVisual3D();
            sphere.Center = new Point3D(destinationPoint.Value.X, destinationPoint.Value.Y, destinationPoint.Value.Z);
            sphere.Radius = 0.25;
            bool isPointInPolygon = Utils.Math.IsPointInPolygon(destinationPoint.Value, m_ProcessPolygon.Polygon.Points);
            if (isPointInPolygon)
                sphere.Material = new EmissiveMaterial(new SolidColorBrush(Colors.Yellow));
            else
                sphere.Material = new EmissiveMaterial(new SolidColorBrush(Colors.Red));

            ViewPort1.Children.Add(sphere);
        }

        private OrthographicCamera orthographicCamera;
        private PerspectiveCamera perspectiveCamera;
        ProcessPolygon m_ProcessPolygon;
    }
}
