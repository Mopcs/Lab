using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

public class Graph : Form
{
    public Graph()
    {
        Func<double, double> leftEquation = x => Math.Pow(x, 3) - 8 * x + 1;
        Func<double, double> rightEquation = x => -5 * Math.Sin(x) + 12 * Math.Cos(x);

        List<double> intersectionPoints = new List<double> { -3.468, -1.210, 1.798 };

        var plotView = CreateChart(leftEquation, rightEquation, intersectionPoints);

        Text = "График";
        Size = new System.Drawing.Size(800, 600);
        Controls.Add(plotView);
    }

    private double ComputeArea(Func<double, double> equation, double x1, double x2, double step = 0.1)
    {
        double area = 0;

        for (double x = x1; x <= x2; x += step)
        {
            double y = equation(x);
            area += y * step;
        }

        return area;
    }

    private PlotView CreateChart(Func<double, double> leftEquation, Func<double, double> rightEquation, List<double> intersectionPoints)
    {
        var plotView = new PlotView
        {
            Dock = DockStyle.Fill
        };

        var model = new PlotModel { Title = "Графики уравнений" };

        var leftSeries = new LineSeries { Title = "Левое уравнение" };
        for (double x = -5; x <= 5; x += 0.1)
        {
            leftSeries.Points.Add(new DataPoint(x, leftEquation(x)));
        }
        model.Series.Add(leftSeries);

        var rightSeries = new LineSeries { Title = "Правое уравнение" };
        for (double x = -5; x <= 5; x += 0.1)
        {
            rightSeries.Points.Add(new DataPoint(x, rightEquation(x)));
        }
        model.Series.Add(rightSeries);

        for (int i = 0; i < intersectionPoints.Count - 1; i++)
        {
            double x1 = intersectionPoints[i];
            double x2 = intersectionPoints[i + 1];

            double y1_left = leftEquation(x1);
            double y2_left = leftEquation(x2);

            var areaPolygon = new AreaSeries
            {
                Color = OxyColor.FromArgb(50, 255, 0, 0)
            };

            areaPolygon.Points.Add(new DataPoint(x1, y1_left));
            areaPolygon.Points.Add(new DataPoint(x2, y2_left));

            for (double x = x2; x >= x1; x -= 0.1)
            {
                double y = rightEquation(x);
                areaPolygon.Points.Add(new DataPoint(x, y));
            }

            for (double x = x1; x <= x2; x += 0.1)
            {
                double y = leftEquation(x);
                areaPolygon.Points.Add(new DataPoint(x, y));
            }

            areaPolygon.Points.Add(new DataPoint(x1, y1_left));

            model.Series.Add(areaPolygon);

            var intersectionPointMarker = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerFill = OxyColors.Green,
                Title = "Intersection Point"
            };

            double intersectionY = leftEquation(x1);
            double intersectionX = rightEquation(x2);
            intersectionPointMarker.Points.Add(new ScatterPoint(x1, intersectionY));
            intersectionPointMarker.Points.Add(new ScatterPoint(x2, intersectionX));

            model.Series.Add(intersectionPointMarker);

            double areaLeft = ComputeArea(leftEquation, x1, x2);
            double areaRight = ComputeArea(rightEquation, x1, x2);
            double areaDifference = Math.Abs(areaLeft - areaRight);

            var annotation = new TextAnnotation
            {
                Text = $"Площадь: {areaDifference:F2}",
                TextPosition = new DataPoint((x1 + x2) / 2, Math.Max(y1_left, y2_left)),
                TextColor = OxyColors.White,
                Background = OxyColor.FromArgb(150, 0, 0, 0),
                StrokeThickness = 0
            };

            model.Annotations.Add(annotation);
        }

        plotView.Model = model;

        return plotView;
    }

    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Application.Run(new Graph());
    }
}
