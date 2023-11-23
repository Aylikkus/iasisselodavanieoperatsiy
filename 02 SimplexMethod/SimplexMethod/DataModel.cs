using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    public class DataModel : INotifyPropertyChanged
    {
        private Constraints constraints;
        private TargetFunction targetFunction;

        public Constraints Constraints
        {
            get
            {
                return constraints;
            }
            set
            {
                constraints = value;
                NotifyPropertyChanged();
            }
        }

        public TargetFunction TargetFunction
        {
            get
            {
                return targetFunction;
            }
            set
            {
                targetFunction = value;
                NotifyPropertyChanged();
            }
        }

        public DataModel(Constraints c, TargetFunction target)
        {
            Constraints = c;
            TargetFunction = target;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    
        public static double[,] GetTableView(Constraints constraints, TargetFunction targetFunction)
        {
            double[,] Table = new double[constraints.Rows + 1, constraints.Columns];

            for (int i = 0; i < Table.GetLength(0); i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < Table.GetLength(1); j++)
                    {
                        Table[i, j] = targetFunction.GetCoefficients(j);
                    }
                    Table[i, targetFunction.GetCountOfCoefficients()] = targetFunction.B;
                }
                else
                {
                    for (int j = 0; j < Table.GetLength(1); j++)
                    {
                        Table[i, j] = constraints.GetExpression(i - 1).ReturnCoefficients()[j];
                    }
                    Table[i, targetFunction.GetCountOfCoefficients()] = constraints.GetExpression(i - 1).B;
                }
            }
            return Table;
        }
    }
}
