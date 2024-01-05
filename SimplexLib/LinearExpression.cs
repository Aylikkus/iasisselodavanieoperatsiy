namespace SimplexMethod
{
    public class LinearExpression
    {
        private double[] coefficients;
        private Sign sign;
        private double b;

        public Sign Sign
        {
            get
            {
                return sign;
            }
            set
            {
                sign = value;
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
            }
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
            }
        }

        public LinearExpression(int columns)
        {
            sign = Sign.Equal;
            coefficients = new double[columns];
        }

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

