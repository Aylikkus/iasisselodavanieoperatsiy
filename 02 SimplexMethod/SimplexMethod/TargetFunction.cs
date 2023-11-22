using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    public enum Target
    {
        Min,
        Max
    }

    public class TargetFunction : INotifyPropertyChanged
    {
        private double[] coefficients;
        private double b;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Target Target { get; set; }

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
    }
}
