namespace OptimalStrategyInUncertainity
{
    public class MaxCriterion : ICriterion
    {
        public int GetOptimalStrategy(PaymentMatrix matrix)
        {
            double maxValue = double.MinValue;
            int maxValueIndex = 0;
            for (int i = 1; i <= matrix.Size; i++)
            {
                for (int j = 1; j <= matrix.Size; j++)
                {
                    if (maxValue < matrix[i, j])
                    {
                        maxValue = matrix[i, j];
                        maxValueIndex = i;
                    }
                }
            }

            return maxValueIndex;
        }
    }
}