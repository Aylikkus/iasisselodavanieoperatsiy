namespace BimatrixGames
{
    public class DataModel
    {
        public TwoOnTwoMatrix A { get; set; }
        public TwoOnTwoMatrix B { get; set; }

        public double C
        {
            get
            {
                return A[1, 1] - A[1, 2] - A[2, 1] + A[2, 2];
            }
        }

        public double Alpha
        {
            get
            {
                return A[2, 2] - A[1, 2];
            }
        }

        public double D
        {
            get
            {
                return B[1, 1] - B[1, 2] - B[2, 1] + B[2, 2];
            }
        }

        public double Delta
        {
            get
            {
                return B[2, 2] - B[2, 1];
            }
        }

        public DataModel()
        {
            // Значения по умолчанию
            A = new TwoOnTwoMatrix(-10, 2, 1, -1);
            B = new TwoOnTwoMatrix(5, -2, -1, 1);
        }

        public DataModel(TwoOnTwoMatrix a, TwoOnTwoMatrix b)
        {
            A = a;
            B = b;
        }

        private double findOptimal(double firstCoefficient, double secondCoefficient, bool q)
        {
            if (firstCoefficient == 0)
            {
                double numerator = q ? A[2, 2] - A[1, 2] : B[2, 2] - B[2, 1];
                double denominator = q ? 
                    A[1, 1] + A[2, 2] - A[1, 2] - A[2, 1] :
                    B[1, 1] + B[2, 2] - B[1, 2] - B[2, 1];
                return numerator / denominator;
            }
            else
            {
                return secondCoefficient / firstCoefficient;
            }
        }

        public (double, double) P
        {
            get
            {
                double q = findOptimal(D, Delta, true);
                return (q, 1 - q);
            }
        }

        public (double, double) Q
        {
            get
            {
                double p = findOptimal(C, Alpha, false);
                return (p, 1 - p);
            }
        }

        public double AverageWin(bool forPlayerB, bool secondStrategy)
        {
            TwoOnTwoMatrix matrix = forPlayerB ? B : A;
            double p = secondStrategy ? P.Item2 : P.Item1;
            double q = secondStrategy ? Q.Item2 : Q.Item1;

            return p * q * (matrix[1, 1] - matrix[1, 2] - matrix[2, 1] + matrix[2, 2])
                + p * (matrix[1, 2] - matrix[2, 2]) + q * (matrix[2, 1] - matrix[2, 2])
                + matrix[2, 2];
        }
    }
}
