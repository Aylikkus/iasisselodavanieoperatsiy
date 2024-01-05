namespace SimplexMethod
{
    public enum Target
    {
        Min,
        Max
    }

    public class TargetFunction
    {
        private double[] coefficients;
        private Target target;
        private double b;

        public Target Target 
        { 
            get
            {
                return target;
            }
            set
            {
                target = value;
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
            }
        }

        public TargetFunction(int numberOfCoefficients)
        {
            CoefficientsCount = numberOfCoefficients;
            coefficients = new double[numberOfCoefficients];
            Target = Target.Max;
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
        public TargetFunction(double[] coefficients, double B, Target target)
        {
            this.coefficients = coefficients;
            this.target = target;
            this.B = B;
        }

        public TargetFunction GetCanonicalForm()
        {
            TargetFunction canonical = new TargetFunction(CofficientsCount());
            canonical.Target = Target;
            canonical.coefficients = coefficients.Clone() as double[];
            canonical.B = B;
            if (canonical.Target == Target.Max)
            {
                for (int i = 0; i < canonical.coefficients.Length; i++)
                {
                    canonical[i] *= -1;
                    canonical.B *= -1;
                }
                canonical.Target = Target.Min;
            }
            return canonical;
        }

        public int CofficientsCount()
        {
            return coefficients.Length;
        }
    }
}