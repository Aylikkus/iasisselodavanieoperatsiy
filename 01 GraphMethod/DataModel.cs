using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts.Wpf.Converters;
using System.Windows.Controls;

namespace GraphMethod
{
    public enum ChartAxis
    {
        X1,
        X2,
        Y1,
        Y2
    }

    public class DataModel
    {
        private LinearEquation x1;
        private LinearEquation y1;
        private LinearEquation x2;
        private LinearEquation y2;
        public CartesianChart cartesianChart { get; set; }

        private bool isValidDouble(double n)
        {
            return !(double.IsInfinity(n) || double.IsNaN(n));
        }

        public LinearEquation X1
        {
            get
            {
                return x1;
            }
        }
        public LinearEquation Y1 
        {
            get
            {
                return y1;
            }
        }
        public LinearEquation X2 {
            get
            {
                return x2;
            }
        }
        public LinearEquation Y2 
        {
            get
            {
                return y2;
            }
        }

        public TargetFunction TargetFunction { get; set; }
        public SystemEquations SystemEquations { get; set; }

        public DataModel(CartesianChart chart)
        {
            cartesianChart = chart;
            chart.Series = new SeriesCollection();

            x1 = new LinearEquation(this, 0, 1,
                    0, Sign.Equal);
            y1 = new LinearEquation(this, 1, 0,
                    0, Sign.Equal);
            x2 = new LinearEquation(this, 0, 1,
                    100, Sign.Equal);
            y2 = new LinearEquation(this, 1, 0,
                    100, Sign.Equal);
        }

        public void UpdateAxis()
        {
            double b1 = cartesianChart.AxisX[0].MaxValue;
            double b2 = cartesianChart.AxisY[0].MaxValue;

            if (isValidDouble(b1) && 
                isValidDouble(b2))
            {
                x2.B = b1;
                y2.B = b2;
            }
        }

        public ObservablePoint Intersect(ChartAxis axis, LinearEquation e2)
        {
            LinearEquation e1;
            switch (axis)
            {
                case ChartAxis.X1:
                    e1 = X1;
                    break;
                case ChartAxis.X2:
                    e1 = X2;
                    break;
                case ChartAxis.Y1:
                    e1 = Y1;
                    break;
                case ChartAxis.Y2:
                    e1 = Y2;
                    break;
                default:
                    e1 = X1;
                    break;
            }

            decimal d = (decimal)(e1.C1 * e2.C2 - e1.C2 * e2.C1);
            decimal d1 = (decimal)(e1.B * e2.C2 - e2.B * e1.C2);
            decimal d2 = (decimal)(e1.C1 * e2.B - e1.B * e2.C1);

            try
            {
                decimal x = d1 / d;
                decimal y = d2 / d;

                ObservablePoint p = new ObservablePoint(
                    (double)y,
                    (double)x
                );
                return p;
            }
            catch(DivideByZeroException)
            {
                return null;
            }
        }

        public ObservablePoint Intersect(ChartAxis axis, TargetFunction target)
        {
            LinearEquation e = new LinearEquation(this, target.C1, target.C2, 0, Sign.Equal);
            return Intersect(axis, e);
        }

        public ObservablePoint Intersect(LinearEquation e1, LinearEquation e2)
        {
            decimal d = (decimal)(e1.C1 * e2.C2 - e1.C2 * e2.C1);
            decimal d1 = (decimal)(e1.B * e2.C2 - e2.B * e1.C2);
            decimal d2 = (decimal)(e1.C1 * e2.B - e1.B * e2.C1);

            try
            {
                decimal x = d1 / d;
                decimal y = d2 / d;

                ObservablePoint p = new ObservablePoint(
                    (double)y,
                    (double)x
                );
                return p;
            }
            catch (DivideByZeroException)
            {
                return null;
            }
        }

        public void Draw()
        {
            UpdateAxis();
            SystemEquations.Draw();
            //TargetFunction.Draw();
        }

        public void DrawRoAV()
        {
            SystemEquations.DrawRoAV(TargetFunction);
        }
    }
}
