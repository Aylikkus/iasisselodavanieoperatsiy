using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;

namespace GraphMethod
{
    public class SystemEquations
    {
        private DataModel dm;
        public List<LinearEquation> LinearEquations { get; set; }

        public SystemEquations(DataModel dm, List<LinearEquation> linearEquations)
        {
            this.dm = dm;
            LinearEquations = linearEquations;
        }

        public void AddEquation(LinearEquation e)
        {
            LinearEquations.Add(e);
        }

        public void Draw()
        {
            foreach (LinearEquation e in LinearEquations)
                e.Draw();
        }
    }
}
