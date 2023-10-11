using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace GraphMethod
{
    public enum Target
    {
        Min,
        Max
    }

    public class TargetFunction
    {
        private CartesianChart chart;
        private DataModel dm;
        private LineSeries lineSeries;
        public double C1 { get; set; }
        public double C2 { get; set; }

        public Target Target { get; set; }

        public TargetFunction(DataModel dm, CartesianChart chart, double c1, double c2, Target target)
        {
            this.chart = chart;
            C1 = c1;
            C2 = c2;
            Target = target;
            lineSeries = new LineSeries
            {
                Fill = Brushes.Transparent,
                LineSmoothness = 0,
                StrokeThickness = 1,
                Stroke = Brushes.Red,
                Values = new ChartValues<ObservablePoint>()
            };
            dm.cartesianChart.Series.Add(lineSeries);
        }

        public void Draw()
        {
            lineSeries.Values.Clear();
            ObservablePoint p1 = dm.Intersect(ChartAxis.X1, this);
            if (p1 != null)
                lineSeries.Values.Add(p1);

            ObservablePoint p2 = dm.Intersect(ChartAxis.X2, this);
            if (p2 != null)
                lineSeries.Values.Add(p2);

            ObservablePoint p3 = dm.Intersect(ChartAxis.Y1, this);
            if (p3 != null)
                lineSeries.Values.Add(p3);

            ObservablePoint p4 = dm.Intersect(ChartAxis.Y2, this);
            if (p4 != null)
                lineSeries.Values.Add(p4);
        }
    }
}
