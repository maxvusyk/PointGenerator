﻿using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;

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
        }

        private void buttonGenerate_Click(object sender, RoutedEventArgs e)
        {
            var coordBorder = getBorder();
            m_ProcessLines = new ProcessLines(coordBorder, ViewPort1);
            m_ProcessLines.RenderLines();

        }

        private CoordinateBorder getBorder()
        {
            List<int> borders = new List<int>();
            borders.Add(Int32.Parse(textBoxMinX.Text));
            borders.Add(Int32.Parse(textBoxMaxX.Text));
            borders.Add(Int32.Parse(textBoxMinY.Text));
            borders.Add(Int32.Parse(textBoxMaxY.Text));
            borders.Add(Int32.Parse(textBoxMinZ.Text));
            borders.Add(Int32.Parse(textBoxMaxZ.Text));
            borders.Add(Int32.Parse(textBoxCountPoint.Text));

            return
                new CoordinateBorder(borders);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            setDefaultValues();
        }

        private void buttonGeneratePolygon_Click(object sender, RoutedEventArgs e)
        {
            clearScene();
            m_ProcessLines.createPolygon(Int32.Parse(textBoxCount.Text));
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

        private ProcessLines m_ProcessLines;


    }
}
