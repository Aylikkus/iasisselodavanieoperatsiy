namespace OptimalStrategyInUncertainity
{
    public class LaplasCriterion : ICriterion
    {
        public int GetOptimalStrategy(PaymentMatrix matrix)
        {
            int n = matrix.Size;
            int maxValueIndex = 0;
            double maxValue = double.MinValue;

            for (int i = 1; i <= matrix.Size; i++)
            {
                double sum = 0;
                for (int j = 1; j <= matrix.Size; j++)
                {
                    sum += matrix[i, j];
                }

                double result = 1.0 / n * sum;
                if (result > maxValue)
                {
                    maxValueIndex = i;
                    maxValue = result;
                }
            }

            return maxValueIndex;
        }
    }
}