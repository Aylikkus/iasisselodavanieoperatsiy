using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace GraphMethod
{
    public enum Sign
    {
        Less,
        More,
        Equal
    }

    public class LinearEquation
    {
        private DataModel dm;
        private LineSeries lineSeries;
        public double C1 { get; set; }
        public double C2 { get; set; }

        public double B { get; set; }

        public Sign Sign { get; set; }

        public LinearEquation(DataModel dm, double c1, double c2, double b, Sign sign)
        {
            this.dm = dm;
            C1 = c1;
            C2 = c2;
            B = b;
            Sign = sign;
            lineSeries = new LineSeries
            {
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
