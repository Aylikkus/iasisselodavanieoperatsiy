using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimplexMethod
{
    public class LinearExpression : INotifyPropertyChanged
    {
        private double[] coefficients;
        private Sign sign;
        private double b;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Sign Sign
        {
            get
            {
                return sign;
            }
            set
            {
                sign = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(SignString));
            }
        }

        public string SignString
        {
            get
            {
                return SignConverter.GetString(sign);
            }
            set
            {
                if (SignConverter.IsValidString(value))
                {
                    sign = SignConverter.GetSign(value);
                }
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Sign));
            }
        }

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

        [IndexerName("Item")]
        public double this[int i]
        {
            get
            {
                return coefficients[i];
            }
            set
            {
                coefficients[i] = value;
                NotifyPropertyChanged("Item[]");
            }
        }
        
        public LinearExpression(int columns)
        {
            sign = Sign.Equal;
            coefficients = new double[columns];
        }

        private LinearExpression() { }

        public int CoefficientsCount()
        {
            return coefficients.Length;
        }

        public void InvertGreaterThan()
        {
            for (int i = 0; i < coefficients.Length; i++)
            {
                this[i] *= (-1);
            }
            B *= (-1);
            Sign = Sign.LessThanEqual;
        }
    }
}
