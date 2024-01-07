namespace OptimalStrategyInUncertainity
{
    public class GurvicCriterion : ICriterion
    {
        private readonly double alpha;
        public int GetOptimalStrategy(PaymentMatrix matrix)
        {
            double maxValue = double.MinValue;
            int maxValueIndex = 0;
            for (int i = 1; i <= matrix.Size; i++)
            {
                double minRowValue = double.MaxValue;
                double maxRowValue = double.MinValue;
                for (int j = 1; j <= matrix.Size; j++)
                {
                    if (minRowValue > matrix[i, j])
                    {
                        minRowValue = matrix[i, j];
                    }
                    if (maxRowValue < matrix[i, j])
                    {
                        maxRowValue = matrix[i, j];
                    }
                }

                double result = alpha * maxRowValue + (1 - alpha) * minRowValue;
                if (result > maxValue)
                {
                    maxValue = result;
                    maxValueIndex = i;
                }
            }

            return maxValueIndex;
        }

        public GurvicCriterion(double alpha)
        {
            if (alpha < 0 && alpha > 1)
                throw new ArgumentException("Alpha is from 0 to 1");
            
            this.alpha = alpha;
        }
    }
}