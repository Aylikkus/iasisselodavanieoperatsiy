using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
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
        public double C1 { get; set; }
        public double C2 { get; set; }

        public Target Target { get; set; }

        public TargetFunction(CartesianChart chart, double c1, double c2, Target target)
        {
            this.chart = chart;
            C1 = c1;
            C2 = c2;
            Target = target;
        }
    }
}
