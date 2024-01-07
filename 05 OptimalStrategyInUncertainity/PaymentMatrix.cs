using System.Reflection;

namespace OptimalStrategyInUncertainity
{
    public class PaymentMatrix
    {
        private double[,] values;
        private int size;

        public PaymentMatrix(int size)
        {
            values = new double[size, size];
            this.size = size;
        }

        public PaymentMatrix(double[,] values)
        {
            int n = values.GetLength(0);
            int m = values.GetLength(1);

            if (n != m) throw new ArgumentException("Matrix dimensions should be same");
            this.values = (double[,])values.Clone();
            size = n;
        }

        public double this[int i, int j]
        {
            get
            {
                return values[i - 1, j - 1];
            }
            set
            {
                values[i - 1, j - 1] = value;
            }
        }

        public int Size 
        {
            get
            {
                return size;
            }
        }

        public int GetOptimalStrategy(ICriterion criterion)
        {
            return criterion.GetOptimalStrategy(this);
        }
    }
}