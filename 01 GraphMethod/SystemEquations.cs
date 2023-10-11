using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace GraphMethod
{
    public class SystemEquations
    {
        private DataModel dm;
        public List<ObservablePoint> RoavIntersections;
        public List<LinearEquation> LinearEquations { get; set; }

        public SystemEquations(DataModel dm, List<LinearEquation> linearEquations)
        {
            this.dm = dm;
            LinearEquations = linearEquations;
            RoavIntersections = new List<ObservablePoint>();
            foreach (LinearEquation equation in LinearEquations)
                FindIntersections(equation);
        }

        public void AddEquation(LinearEquation e)
        {
            LinearEquations.Add(e);
            FindIntersections(e);
        }

        public List<ObservablePoint> FindIntersections(LinearEquation linearEquation)
        {
            List<ObservablePoint> intersections = new List<ObservablePoint>();
            foreach (LinearEquation e in LinearEquations)
            {
                if (e != linearEquation)
                {
                    ObservablePoint intersection = dm.Intersect(linearEquation, e);
                    if (intersection != null)
                    {
                        intersections.Add(intersection);
                        e.Intersections.Add(intersection);
                        linearEquation.Intersections.Add(intersection);
                    }
                }
            }
            return intersections;
        }

        public bool InRoAV(ObservablePoint p, double e)
        {
            bool valid = true;
            foreach (LinearEquation equation in LinearEquations)
            {
                double literal = equation.C1 * p.Y + equation.C2 * p.X;
                double bpe = equation.B + e;
                double bme = equation.B - e;
                switch (equation.Sign)
                {
                    case Sign.Less:
                        valid = (literal <= bpe) || (literal <= bme);
                        break;
                    case Sign.Equal:
                        valid = (literal == bpe) || (literal == bme);
                        break;
                    case Sign.More:
                        valid = (literal >= bpe) || (literal >= bme);
                        break;
                }
                if (!valid)
                    break;
            }
            return valid;
        }

        public void DrawRoAV(TargetFunction target)
        {
            List<ObservablePoint> roav = new List<ObservablePoint>();
            foreach (LinearEquation equation in LinearEquations)
            {
                roav.Clear();
                foreach (ObservablePoint intersection in equation.Intersections)
                {
                    if (InRoAV(intersection, 0.001))
                    {
                        roav.Add(intersection);
                    }
                }
                LineSeries ls = new LineSeries
                {
                    Fill = Brushes.Transparent,
                    LineSmoothness = 0,
                    StrokeThickness = 3,
                    PointGeometrySize = 15,
                    Stroke = Brushes.Gray,
                    Values = new ChartValues<ObservablePoint>(),
                };
                dm.cartesianChart.Series.Add(ls);
                RoavIntersections.AddRange(roav);
                foreach (ObservablePoint p in roav)
                {
                    ls.Values.Add(p);
                }
            }
            DrawTarget(target);
        }

        public void Draw()
        {
            foreach (LinearEquation e in LinearEquations)
                e.Draw();
        }

        public void DrawTarget(TargetFunction target)
        {
            Dictionary<double, ObservablePoint> distancesFromZero = new Dictionary<double, ObservablePoint>();
            foreach (ObservablePoint p in RoavIntersections)
            {
                double distance = p.X + p.Y;
                distancesFromZero[distance] = p;
            }

            LineSeries ls = new LineSeries
            {
                Fill = Brushes.Transparent,
                LineSmoothness = 0,
                StrokeThickness = 3,
                PointGeometrySize = 17,
                PointForeground = Brushes.Red,
                Stroke = Brushes.Black,
                Values = new ChartValues<ObservablePoint>(),
            };
            dm.cartesianChart.Series.Add(ls);

            if (target.Target == Target.Max)
            {
                ls.Values.Add(distancesFromZero[distancesFromZero.Keys.Max()]);
            }
            else
            if (target.Target == Target.Min)
            {
                ls.Values.Add(distancesFromZero[distancesFromZero.Keys.Min()]);
            }
        }
    }
}
