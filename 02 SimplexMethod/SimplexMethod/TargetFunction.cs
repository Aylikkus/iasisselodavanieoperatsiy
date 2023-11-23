using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    public enum ValueOfTargetFunction
    {
        min,
        max
    }

    public class TargetFunction : INotifyPropertyChanged
    {
        private double[] coefficients;
        private ValueOfTargetFunction target;
        private double b;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ValueOfTargetFunction ValueOfTargetFunction 
        { 
            get
            {
                return target;
            }
            set
            {
                target = value;
                NotifyPropertyChanged();
            }
        }

        public string Target
        {
            get
            {
                return target.ToString();
            }
            set
            {
                if (value == "min")
                {
                    target = ValueOfTargetFunction.min;
                }
                else 
                if (value == "max")
                {
                    target = ValueOfTargetFunction.max;
                }
                else
                {
                    target = ValueOfTargetFunction.max;
                }
                NotifyPropertyChanged();
            }
        }

        public int CoefficientsCount { get; private set; }

        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
                NotifyPropertyChanged();
            }
        }

        public TargetFunction(int numberOfCoefficients)
        {
            CoefficientsCount = numberOfCoefficients;
            coefficients = new double[numberOfCoefficients];
        }

        public double this[int i]
        {
            get
            {
                return coefficients[i];
            }
            set
            {
                coefficients[i] = value;
                NotifyPropertyChanged();
            }
        }
        public TargetFunction(double[] coefficients, double B, ValueOfTargetFunction value)
        {
            this.coefficients = coefficients;
            this.ValueOfTargetFunction = value;
            this.B = B;
        }

        public TargetFunction CheckFunction(ValueOfTargetFunction value)
        {
            if (value == ValueOfTargetFunction.min)
            {
                for (int i = 0; i < coefficients.Length; i++)
                {
                    coefficients[i] = coefficients[i] * (-1);
                    B = B * (-1);
                }
            }
            return new TargetFunction(coefficients, B, value);
        }
        public double GetCoefficients(int temp)
        {
            return coefficients[temp];
        }

        public int GetCountOfCoefficients()
        {
            return coefficients.Length;
        }
    }
}
